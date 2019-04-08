﻿#region

using System.Collections.Generic;
using JosephM.Core.FieldType;

#endregion

namespace JosephM.Application.ViewModel.Fakes
{
    /// <summary>
    ///     Object for access to the main UI thread and adding or removing UI items
    /// </summary>
    public class FakeObjectEntryClass
    {
        public FakeObjectEntryClass()
        {
            var grid = new List<FakeObjectEntryGridClass>();
            grid.Add(new FakeObjectEntryGridClass());
            GridObjects = grid;
        }

        public bool BooleanProperty { get; set; }

        public IEnumerable<FakeObjectEntryGridClass> GridObjects { get; set; }

        public class FakeObjectEntryGridClass
        {
            public Lookup LookupField { get; set; }
            public bool BooleanProperty { get; set; }
        }
    }


}