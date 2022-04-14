using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AareonTechnicalTest.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<ExceptionFilter> _logger;


        public ExceptionFilter(
            IWebHostEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider,
            ILogger<ExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("An Error has occurred", context.Exception);
            //if (!_hostingEnvironment.IsDevelopment())
            //{
            //    return;
            //}
            //var result = new ViewResult { ViewName = "Error" };
            //result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
            //    context.ModelState);
            //result.ViewData.Add("Exception", context.Exception);

            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError); 
        }
    }
}
