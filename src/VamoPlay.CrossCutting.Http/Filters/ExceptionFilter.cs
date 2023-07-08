﻿using VamoPlay.Application.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace VamoPlay.CrossCutting.Http.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        if (context.Exception is VamoPlayException ex)
        {
            var response = new
            {
                Success = false,
                Data = ex.CustomData == null ? new { } : ex.CustomData,
                Errors = new List<string> { ex.Message }
            };

            context.Result = new ObjectResult(response) { StatusCode = (int)ex.HttpStatusCode };
        }
        else
        {
            var response = new
            {
                Success = false,
                Data = new { },
                Errors = new List<string> { context.Exception.Message }
            };

            context.Result = new ObjectResult(response) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
