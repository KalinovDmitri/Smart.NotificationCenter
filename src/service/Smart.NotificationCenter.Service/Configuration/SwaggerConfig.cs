using System;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;

using Swashbuckle.Application;
using Swashbuckle.Swagger;
using Swashbuckle.SwaggerUi;
using WebActivatorEx;

using Smart.NotificationCenter.Service;

namespace Smart.NotificationCenter.Service
{
	public static class SwaggerConfig
	{
		public static void Configure(HttpConfiguration configuration)
		{
			configuration
				.EnableSwagger(ConfigureSwagger)
				.EnableSwaggerUi(ConfigureSwaggerUI);
		}

		private static void ConfigureSwagger(SwaggerDocsConfig docsConfig)
		{
			docsConfig.PrettyPrint();
			docsConfig.SingleApiVersion("v1", "Smart LUIS Web API");

			docsConfig.DescribeAllEnumsAsStrings();
			docsConfig.IgnoreObsoleteActions();

			docsConfig.IncludeXmlComments();
		}

		private static void ConfigureSwaggerUI(SwaggerUiConfig uiConfig)
		{
			uiConfig.DisableValidator();
			uiConfig.DocExpansion(DocExpansion.List);
			uiConfig.SupportedSubmitMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
		}

		private static void IncludeXmlComments(this SwaggerDocsConfig docsConfig)
		{
			string binDirectory = HostingEnvironment.MapPath(@"~\bin\");

			var documentationFiles = Directory.EnumerateFiles(binDirectory, "Smart.NotificationCenter.*.xml");
			foreach (var documentationFile in documentationFiles)
			{
				docsConfig.IncludeXmlComments(documentationFile);
			}
		}
	}
}
