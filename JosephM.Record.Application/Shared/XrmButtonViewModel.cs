﻿#region

using System;
using System.Windows.Input;
using JosephM.Application.Application;
using Microsoft.Practices.Prism.Commands;

#endregion

namespace JosephM.Application.ViewModel.Shared
{
    public class XrmButtonViewModel : ViewModelBase
    {
        private bool _saveButtonVisible = true;

        public XrmButtonViewModel(string label, Action clickAction, IApplicationController applicationController)
            : this(label, label, clickAction, applicationController)
        {
            Label = label;
            Command = new DelegateCommand(clickAction);
        }

        public XrmButtonViewModel(string id, string label, Action clickAction, IApplicationController applicationController)
    : base(applicationController)
        {
            Id = id;
            Label = label;
            Command = new DelegateCommand(clickAction);
        }

        public string Id { get; set; }

        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                OnPropertyChanged(nameof(Label));
            }
        }

        public ICommand Command { get; private set; }

        public bool IsVisible
        {
            get { return _saveButtonVisible; }
            set
            {
                _saveButtonVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public void Invoke()
        {
            Command.Execute(null);
        }
    }
}