﻿using System.Collections.Generic;
using JosephM.Application.ViewModel.RecordEntry.Form;

namespace JosephM.Application.ViewModel.RecordEntry.Field
{
    public class StringEnumerableFieldViewModel : FieldViewModel<IEnumerable<string>>
    {
        public StringEnumerableFieldViewModel(string fieldName, string label, RecordEntryViewModelBase recordForm)
            : base(fieldName, label, recordForm)
        {
        }

        public int? MaxLength { get; set; }
    }
}