﻿using JosephM.Application.Desktop.Module.Crud;

namespace JosephM.TestModule.TestCrud
{
    public class TestCrudModule : CrudModule<TestCrudDialog>
    {
        public override string MainOperationName => "Crud / Query";
    }
}
