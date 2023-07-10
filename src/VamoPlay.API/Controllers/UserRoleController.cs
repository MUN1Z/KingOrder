using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels;
using VamoPlay.CrossCutting.Auth.Attributes;
using VamoPlay.Domain.Enums;

namespace VamoPlay.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class UserRoleController : BaseController
    {
        #region private members

        private readonly IUserRoleService _userRoleService;

        #endregion private members

        #region constructors

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        #endregion constructors

        #region public methods implementations

        [HttpGet(Name = "GetAllUserRoles")]
        [Authorized(UserClaim.UserRoles_Read)]
        public async Task<IActionResult> GetAllUserRoles([FromQuery] UserRoleFilter filter)
            => Response(await _userRoleService.GetAll(filter));

        [HttpGet("{guid}")]
        [Authorized(UserClaim.UserRoles_Read)]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var result = await _userRoleService.GetByGuid(guid);
            return Response(result);
        }

        [HttpPost("New")]
        [Authorized(UserClaim.UserRoles_Create)]
        public async Task<IActionResult> NewUseRole([FromBody] UserRoleRequestViewModel userRole)
        {
            var result = await _userRoleService.RegisterAsync(userRole);

            if (result != null)
                return Response(result);

            return StatusCode(200);
        }

        [HttpPut("{guid}")]
        [Authorized(UserClaim.UserRoles_Write)]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] UserRoleRequestViewModel userRole)
        {
            var result = await _userRoleService.Update(guid, userRole);

            if (result != null)
                return Response(result);

            return StatusCode(200);
        }

        [HttpGet("Permissions")]
        [Authorized(UserClaim.UserRoles_Read)]
        public IActionResult GetAllUserClaims() => Response(_userRoleService.GetAllUserClaims());

        [HttpDelete("{guid}")]
        [Authorized(UserClaim.UserRoles_Delete)]
        public async Task<IActionResult> RemoveByGuid([FromRoute] Guid guid)
        {
            await _userRoleService.RemoveByGuid(guid);
            return NoContent();
        }


        #endregion public methods implementations
    }
}
