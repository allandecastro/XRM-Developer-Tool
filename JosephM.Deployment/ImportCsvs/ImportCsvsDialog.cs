﻿using JosephM.Application.Desktop.Module.ServiceRequest;
using JosephM.Application.ViewModel.Attributes;
using JosephM.Application.ViewModel.Dialog;
using JosephM.Record.Xrm.XrmRecord;
using System;
using System.Linq;

namespace JosephM.Deployment.ImportCsvs
{
    [RequiresConnection]
    public class ImportCsvsDialog :
        ServiceRequestDialog
            <ImportCsvsService, ImportCsvsRequest,
                ImportCsvsResponse, ImportCsvsResponseItem>
    {
        public ImportCsvsDialog(ImportCsvsService service,
            IDialogController dialogController, XrmRecordService lookupService)
            : base(service, dialogController, lookupService)
        {
            var validationDialog = new ImportCsvsValidationDialog(this, Request);
            SubDialogs = SubDialogs.Union(new[] { validationDialog }).ToArray();
        }

        protected override void CompleteDialogExtention()
        {
            base.CompleteDialogExtention();
            CompletionMessage = "The Import Process Has Completed";
            AddCompletionOption($"Open {Service?.XrmRecordService?.XrmRecordConfiguration?.ToString()}", () =>
            {
                try
                {
                    ApplicationController.StartProcess(Service?.XrmRecordService?.WebUrl);
                }
                catch (Exception ex)
                {
                    ApplicationController.ThrowException(ex);
                }
            });
        }
    }
}