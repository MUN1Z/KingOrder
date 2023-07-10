using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Services;
using VamoPlay.CrossCutting.Auth.Attributes;
using VamoPlay.Domain.Enums;

namespace VamoPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        #region Private Members

        private readonly IUserService _userService;

        #endregion

        #region Constructors

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public Methods

        [HttpGet(Name = "GetAll")]
        [Authorized(ClaimType.Users_Read)]
        public async Task<IActionResult> GetAll([FromQuery] UserFilter filter)
        {
            var result = await _userService.GetAll(filter);
            return Response(result);
        }

        [HttpPost("New")]
        [AllowAnonymous]
        public async Task<IActionResult> New([FromBody] UserRequestViewModel userAccount)
        {
            var result = await _userService.RegisterAsync(userAccount);

            if (result != null)
                return Response(result);

            return StatusCode(200);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LogInRequestViewModel user)
        {
            var result = await _userService.LoginAsync(user);
            return Response(result);
        }

        [HttpDelete("LogOff")]
        [AllowAnonymous]
        public async Task<IActionResult> LogOff()
        {
            await _userService.LogOffAsync();
            return NoContent();
        }

        #endregion
    }
}