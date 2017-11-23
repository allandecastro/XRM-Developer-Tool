﻿using JosephM.Application.Modules;
using JosephM.Core.Attributes;
using JosephM.Core.Service;
using JosephM.Prism.Infrastructure.Module;
using JosephM.Prism.XrmModule.XrmConnection;

namespace JosephM.CodeGenerator.CSharp
{
    [MyDescription("Generate C# Code Constants For The Customisations In A CRM Instance")]
    [DependantModule(typeof(XrmConnectionModule))]
    public class CSharpModule :
        ServiceRequestModule
            <CSharpDialog, CSharpService, CSharpRequest, CSharpResponse, ServiceResponseItem>
    {
        public override string MenuGroup => "Code Generation";
    }
}