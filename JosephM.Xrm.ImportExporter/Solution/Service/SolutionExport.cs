﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JosephM.Core.Attributes;
using JosephM.Core.FieldType;
using JosephM.Prism.XrmModule.SavedXrmConnections;

namespace JosephM.Xrm.ImportExporter.Service
{
    public class SolutionExport
    {
        [DisplayOrder(20)]
        [RequiredProperty]
        [GridWidth(400)]
        [SettingsLookup(typeof(ISavedXrmConnections), "Connections")]
        [ConnectionFor("Solution")]
        [ConnectionFor("DataToExport.RecordType")]
        [ReadOnlyWhenSet]
        public SavedXrmRecordConfiguration Connection { get; set; }

        [DisplayOrder(30)]
        [RequiredProperty]
        [GridWidth(400)]
        [ReferencedType("solution")]
        [LookupCondition("ismanaged", false)]
        [LookupCondition("isvisible", true)]
        [PropertyInContextByPropertyNotNull("Connection")]
        public Lookup Solution { get; set; }

        [DisplayOrder(40)]
        [RequiredProperty]
        public bool Managed { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyNotNull("Connection")]
        public bool IncludeNotes { get; set; }

        [RequiredProperty]
        [PropertyInContextByPropertyNotNull("Connection")]
        public bool IncludeNNRelationshipsBetweenEntities { get; set; }

        [DisplayOrder(200)]
        [RequiredProperty]
        [GridWidth(400)]
        [PropertyInContextByPropertyNotNull("Connection")]
        public IEnumerable<ImportExportRecordType> DataToExport { get; set; }
    }
}
