﻿using Spark;
using System;
using Spark.Web.FubuMVC;
using Spark.Web.FubuMVC.Bootstrap;
using Spark.Web.FubuMVC.Extensions;
using FubuMVC.HelloSpark.Controllers;
using Spark.Web.FubuMVC.ViewCreation;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.HelloSpark
{
    public class HelloSparkRegistry : SparkDefaultStructureMapRegistry
    {
        private SparkViewFactory _sparkViewFactory;
        public HelloSparkRegistry(bool enableDiagnostics, string controllerAssembly, SparkViewFactory sparkViewFactory)
            : base(enableDiagnostics, controllerAssembly)
        {
            _sparkViewFactory = sparkViewFactory;

            Output.ToJson.WhenTheOutputModelIs<JsonResponse>();

            Output.To(call => new JavaScriptOutputNode(GetJavaScriptViewToken(call), call))
                .WhenTheOutputModelIs<JavaScriptResponse>();

            HomeIs<AirController>(c => c.TakeABreath());
        }

        private SparkViewToken GetJavaScriptViewToken(ActionCall call)
        {
            var response = JavaScriptResponse.GetResponse(call);
            string controllerName = call.HandlerType.Name.RemoveSuffix("Controller");
            return _sparkViewFactory.GetViewToken(call, controllerName, response.ViewName, LanguageType.Javascript);
        }
    }
}
