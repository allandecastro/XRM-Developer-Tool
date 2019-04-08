﻿using JosephM.Application.ViewModel.RecordEntry.Form;

namespace JosephM.Application.ViewModel.RecordEntry.Field
{
    public class StringFieldViewModel : FieldViewModel<string>
    {
        public StringFieldViewModel(string fieldName, string label, RecordEntryViewModelBase recordForm)
            : base(fieldName, label, recordForm)
        {
        }

        public override string Value
        {
            get { return ValueObject == null ? null : ValueObject.ToString(); }
            set { ValueObject = value; }
        }

        public int? MaxLength { get; set; }

        public bool IsMultiline { get; set; }

        public int NumberOfLines
        {
            get { return IsMultiline ? 10 : 1; }
        }
    }
}