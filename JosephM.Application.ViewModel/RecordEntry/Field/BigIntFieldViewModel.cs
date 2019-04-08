﻿#region

using System;
using JosephM.Application.ViewModel.RecordEntry.Form;
using JosephM.Core.Service;

#endregion

namespace JosephM.Application.ViewModel.RecordEntry.Field
{
    public class BigIntFieldViewModel : FieldViewModel<long?>
    {
        public BigIntFieldViewModel(string fieldName, string label, RecordEntryViewModelBase recordForm)
            : base(fieldName, label, recordForm)
        {
            MinValue = Int64.MinValue;
            MaxValue = Int64.MaxValue;
        }

        public long MaxValue { get; set; }
        public long MinValue { get; set; }

        protected override IsValidResponse VerifyValueRequest(object value)
        {
            var response = new IsValidResponse();
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                var intValue = long.Parse(value.ToString());
                if (intValue > MaxValue)
                {
                    response.AddInvalidReason(
                        string.Format("The entered value is greater than the maximum of {0}", MaxValue));
                }
                if (intValue < MinValue)
                {
                    response.AddInvalidReason(
                        string.Format("The entered value is less than the minimum of {0}", MinValue));
                }
            }
            else if (IsNotNullable && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
            {
                response.AddInvalidReason(string.Format("A Value Is Required"));
            }
            return response;
        }
    }
}