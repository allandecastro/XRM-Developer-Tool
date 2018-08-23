﻿using JosephM.Application.Desktop.Module.ServiceRequest;
using JosephM.Core.Attributes;
using JosephM.Core.Service;

namespace JosephM.CodeGenerator.FetchToJavascript
{
    [MyDescription("Generate C# Or JavaScript Code For A Multiline String Value")]
    public class FetchToJavascriptModule :
        ServiceRequestModule
            <FetchToJavascriptDialog, FetchToJavascriptService, FetchToJavascriptRequest, FetchToJavascriptResponse, ServiceResponseItem>
    {
        public override string MenuGroup => "Code Generation";

        public override string MainOperationName => "Convert Fetch To Javascript";
    }
}