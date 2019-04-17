﻿using JosephM.Application.Desktop.Module.ServiceRequest;
using JosephM.Application.ViewModel.Extentions;
using JosephM.Application.ViewModel.RecordEntry.Form;
using JosephM.Core.Attributes;
using System;
using System.Collections.Generic;

namespace JosephM.TestModule.TestDialog
{
    [MyDescription("Just a fake dialog for testing")]
    public class TestDialogModule :
        ServiceRequestModule
            <TestDialog, TestDialogService, TestDialogRequest, TestDialogResponse, TestDialogResponseItem>
    {
        public override void RegisterTypes()
        {
            base.RegisterTypes();

            AddAutocompletes();
        }

        private void AddAutocompletes()
        {
            var props = new[]
            {
                new KeyValuePair<Type,string>(typeof(TestDialogRequest), nameof(TestDialogRequest.AutocompleteField)),
                new KeyValuePair<Type,string>(typeof(TestDialogRequest.TestDialogRequestItem), nameof(TestDialogRequest.TestDialogRequestItem.AutocompleteField)),
            };
            foreach (var prop in props)
            {
                this.AddAutocompleteFunction(new AutocompleteFunction((recordForm) =>
                {
                    return new[]
                    {
                        "aaaaa",
                        "abcde",
                        "abcdef",
                        "abcdeg",
                        "abcdeh",
                        "abcdeabcde",
                        "ZZZZZZZZ",
                        "blah",
                        "BLAHs"
                    };
                }), prop.Key, prop.Value);
            }
        }
    }
}