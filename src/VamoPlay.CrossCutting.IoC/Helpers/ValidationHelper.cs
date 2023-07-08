using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VamoPlay.CrossCutting.IoC.Helpers
{
    public static class ValidationHelper
    {
        public static BadRequestObjectResult GetInvalidModelStateResponse(ActionContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                var response = new
                {
                    Success = false,
                    Data = new { },
                    Errors = errors
                };

                return new BadRequestObjectResult(response);
            }

            return null;
        }
    }
}
