﻿#region

using JosephM.Core.Attributes;
using JosephM.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace JosephM.Core.Service
{
    [Group(Sections.FatalError, false, 0)]
    [Group(Sections.ResponseItems, true, 1, displayLabel: false)]
    public class ServiceResponseBase<TResponseItem> : IProcessCompletion
        where TResponseItem : ServiceResponseItem
    {
        private readonly List<TResponseItem> _errors = new List<TResponseItem>();

        public ServiceResponseBase()
        {
            Success = true;
        }

        public void SetFatalError(Exception ex)
        {
            Success = false;
            Exception = ex;
        }

        [Hidden]
        public bool Success { get; private set; }

        [Hidden]
        public Exception Exception { get; set; }

        public void AddResponseItem(TResponseItem responseItem)
        {
            _errors.Add(responseItem);
        }

        public void AddResponseItems(IEnumerable<TResponseItem> responseItems)
        {
            _errors.AddRange(responseItems);
        }

        [AllowDownload]
        [Group(Sections.ResponseItems)]
        [DisplayOrder(20)]
        [PropertyInContextByPropertyValue(nameof(HasResponseItems), true)]
        public IEnumerable<TResponseItem> ResponseItems
        {
            get { return _errors; }
        }

        public IEnumerable<TResponseItem> GetResponseItemsWithError()
        {
            return ResponseItems.Where(e => e.HasError);
        }

        IEnumerable<object> IProcessCompletion.GetResponseItemsWithError()
        {
            return GetResponseItemsWithError();
        }

        [Hidden]
        public bool HasResponseItems
        {
            get { return ResponseItems.Any(); }
        }

        [Hidden]
        public bool HasResponseItemError
        {
            get { return ResponseItems.Any(r => r.HasError); }
        }

        [Hidden]
        public bool HasError
        {
            get { return Exception != null || HasResponseItemError; }
        }

        bool IProcessCompletion.Success => Success;

        Exception IProcessCompletion.Exception => Exception;

        private static class Sections
        {
            public const string FatalError = "Summary";
            public const string ResponseItems = "Response Items";
        }
    }
}