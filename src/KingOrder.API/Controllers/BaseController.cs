using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using KingOrder.Application.Shared.ViewModels.Response;
using KingOrder.Application.Shared.Exceptions;

namespace KingOrder.API.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        #region protected methods implementation

        protected new IActionResult Response(object result = null)
        {
            if (result != null)
            {
                return Ok(new BaseResponseViewModel
                {
                    Success = true,
                    Data = result,
                    Errors = new List<string>()
                });
            }
            return BadRequest(new
            {
                Success = false,
                Data = new { },
                Errors = new List<string>()
            });
        }

        protected new IActionResult Response(object result = null, ModelStateDictionary modelState = null)
        {
            if (result != null)
            {
                return Ok(new BaseResponseViewModel
                {
                    Success = true,
                    Data = result,
                    Errors = new List<string>()
                });
            }

            var modelErrors = new List<string>();

            if (modelState != null && !ModelState.IsValid)
                foreach (var state in ModelState.Values)
                    foreach (var modelError in state.Errors)
                        modelErrors.Add(modelError.ErrorMessage);

            return BadRequest(new
            {
                Success = false,
                Data = new { },
                Errors = modelErrors
            });
        }

        protected new IActionResult Response(object result = null, List<string> modelErrors = null)
        {
            if (result != null)
            {
                return Ok(new BaseResponseViewModel
                {
                    Success = true,
                    Data = null,
                    Errors = new List<string>()
                });
            }

            return BadRequest(new
            {
                Success = false,
                Data = new { },
                Errors = modelErrors
            });
        }

        protected new IActionResult Response(object result = null, string error = "")
        {
            if (result != null)
            {
                return Ok(new BaseResponseViewModel
                {
                    Success = true,
                    Data = null,
                    Errors = new List<string>()
                });
            }

            return BadRequest(new
            {
                Success = false,
                Data = new { },
                Errors = new List<string>() { error }
            });
        }

        protected IActionResult ExceptionResponse(KingOrderException ex)
        {
            return StatusCode((int)ex.HttpStatusCode, new
            {
                Success = false,
                Data = new { },
                Errors = new List<string>() { ex.Message }
            });
        }

        #endregion protected methods implementation
    }
}
