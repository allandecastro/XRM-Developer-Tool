﻿namespace JosephM.XrmModule.SavedXrmConnections
{
    public class DiscoveryAutocomplete
    {
        public DiscoveryAutocomplete(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }
    }
}
