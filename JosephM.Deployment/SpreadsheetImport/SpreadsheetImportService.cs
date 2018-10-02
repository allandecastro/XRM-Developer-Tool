﻿using JosephM.Application.Application;
using JosephM.Core.Extentions;
using JosephM.Core.Log;
using JosephM.Core.Service;
using JosephM.Deployment.DataImport;
using JosephM.Record.IService;
using JosephM.Record.Xrm.XrmRecord;
using JosephM.Xrm;
using JosephM.Xrm.Schema;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JosephM.Deployment.SpreadsheetImport
{
    public class SpreadsheetImportService
    {
        public SpreadsheetImportService(XrmRecordService xrmRecordService)
        {
            XrmRecordService = xrmRecordService;
        }

        public XrmRecordService XrmRecordService { get; }
        public IApplicationController ApplicationController { get; }

        public SpreadsheetImportResponse DoImport(Dictionary<IMapSpreadsheetImport, IEnumerable<IRecord>> mappings, bool maskEmails, bool matchByName, ServiceRequestController controller, bool useAmericanDates = false)
        {
            var response = new SpreadsheetImportResponse();
            var parseResponse = ParseIntoEntities(mappings, useAmericanDates: useAmericanDates);
            response.LoadParseResponse(parseResponse);
            var dataImportService = new DataImportService(XrmRecordService);
            response.LoadDataImport(dataImportService.DoImport(parseResponse.GetParsedEntities(), controller, maskEmails, matchOption: matchByName ? DataImportService.MatchOption.PrimaryKeyThenName : DataImportService.MatchOption.PrimaryKeyOnly));
            return response;
        }

        public ParseIntoEntitiesResponse ParseIntoEntities(Dictionary<IMapSpreadsheetImport, IEnumerable<IRecord>> mappings, bool useAmericanDates = false)
        {
            var response = new ParseIntoEntitiesResponse();
            foreach (var mapping in mappings)
            {
                response.AddEntities(MapToEntities(mapping.Value, mapping.Key, response, useAmericanDates));
            }
            PopulateEmptyNameFields(response.GetParsedEntities());
            return response;
        }

        private IEnumerable<Entity> MapToEntities(IEnumerable<IRecord> queryRows, IMapSpreadsheetImport mapping, ParseIntoEntitiesResponse response, bool useAmericanDates)
        {
            var result = new List<Entity>();

            var nNRelationshipEntityNames = XrmRecordService
                .GetManyToManyRelationships()
                .Select(m => m.IntersectEntityName)
                .ToArray();

            var duplicateLogged = false;

            var rowNumber = 0;
            foreach (var row in queryRows)
            {
                rowNumber++;
                var targetType = mapping.TargetType;
                try
                {
                    var isNnRelation = nNRelationshipEntityNames.Contains(targetType);
                    var entity = new Entity(targetType);
                    var keyColumns = new string[0];

                    foreach (var fieldMapping in mapping.FieldMappings)
                    {
                        var targetField = fieldMapping.TargetField;
                        if (fieldMapping.TargetField != null)
                        {
                            var stringValue = row.GetStringField(fieldMapping.SourceField);
                            if (stringValue != null)
                                stringValue = stringValue.Trim();
                            if (isNnRelation)
                            {
                                //bit of hack
                                //for csv relationships just set to a string and map it later
                                //as the referenced record may not be created yet
                                entity.SetField(targetField, stringValue);
                            }
                            else if (XrmRecordService.XrmService.IsLookup(targetField, targetType))
                            {
                                //for lookups am going to set to a empty guid and allow the import part to replace with a correct guid
                                if (!stringValue.IsNullOrWhiteSpace())
                                    entity.SetField(targetField,
                                        new EntityReference(XrmRecordService.XrmService.GetLookupTargetEntity(targetField, targetType),
                                            Guid.Empty)
                                        {
                                            Name = stringValue
                                        });
                            }
                            else
                            {
                                try
                                {
                                    entity.SetField(targetField, XrmRecordService.XrmService.ParseField(targetField, targetType, stringValue, useAmericanDates));
                                }
                                catch(Exception ex)
                                {
                                    response.AddResponseItem(new ParseIntoEntitiesResponse.ParseIntoEntitiesError(rowNumber, targetType, targetField, null, stringValue, "Error Parsing Field - " + ex.Message, ex));
                                }
                            }
                        }
                    }
                    //okay any which are exact duplicates to previous ones lets ignore
                    if (result.Any(r => r.GetFieldsInEntity().All(f => XrmRecordService.FieldsEqual(r.GetField(f), entity.GetField(f)))))
                    {
                        if(!duplicateLogged)
                        {
                            response.AddResponseItem(new ParseIntoEntitiesResponse.ParseIntoEntitiesError(rowNumber, targetType, null, null, null, "At Least One Duplicate For The Import Map Was Ignored", null));
                            duplicateLogged = true;
                        }
                        continue;
                    }

                    result.Add(entity);
                }
                catch (Exception ex)
                {
                    //todo perhaps could add row number and source details etc.
                    response.AddResponseItem(new ParseIntoEntitiesResponse.ParseIntoEntitiesError("Mapping Error", ex));
                }
            }
            return result;
        }

        private void PopulateEmptyNameFields(IEnumerable<Entity> entities)
        {
            foreach (var contact in entities.Where(e => e.LogicalName == Entities.contact))
            {
                if (contact.Contains(Fields.contact_.fullname)
                    && !contact.Contains(Fields.contact_.firstname)
                    && !contact.Contains(Fields.contact_.lastname))
                {
                    //okay for these dudes lets split their name into first and last name somehow
                    var name = contact.GetStringField(Fields.contact_.fullname);
                    if (name != null)
                    {
                        name = name.Trim();
                        var lastSpaceIndex = name.LastIndexOf(" ");
                        if (lastSpaceIndex == -1)
                        {
                            contact.SetField(Fields.contact_.firstname, name);
                        }
                        else
                        {
                            contact.SetField(Fields.contact_.firstname, name.Substring(0, lastSpaceIndex));
                            contact.SetField(Fields.contact_.lastname, name.Substring(lastSpaceIndex + 1));
                        }
                    }
                }
                if (!contact.Contains(Fields.contact_.fullname)
                    && (contact.Contains(Fields.contact_.firstname)
                        || contact.Contains(Fields.contact_.lastname)))
                {
                    //okay for these dudes lets split their name into first and last name somehow
                    var name = contact.GetStringField(Fields.contact_.firstname) + " " + contact.GetStringField(Fields.contact_.lastname);
                    if (name != null)
                    {
                        name = name.Trim();
                        contact.SetField(Fields.contact_.fullname, name);
                    }
                }
            }
        }
    }
}
