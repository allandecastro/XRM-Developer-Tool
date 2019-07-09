﻿using JosephM.Application.ViewModel.Query;
using JosephM.Application.ViewModel.RecordEntry.Metadata;
using System.Collections.Generic;

namespace JosephM.Application.ViewModel.Fakes
{
    /// <summary>
    ///     Not actually showing yet - was attempting to show a sample form in vs
    /// </summary>
    public class FakeColumnEditDialogViewModel : ColumnEditDialogViewModel
    {
        public FakeColumnEditDialogViewModel()
            : base(FakeConstants.RecordType, new [] { new GridFieldMetadata(FakeConstants.StringField, 200) }, FakeRecordService.Get(), null, null,  new FakeApplicationController())
        {
        }
    }
}