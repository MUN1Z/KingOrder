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
    public class RoleController : BaseController
    {
        #region private members

        private readonly IRoleService _roleService;

        #endregion private members

        #region constructors

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion constructors

        #region public methods implementations

        [HttpGet(Name = "GetAllRoles")]
        [Authorized(ClaimType.Roles_Read)]
        public async Task<IActionResult> GetAllRoles([FromQuery] RoleFilter filter)
            => Response(await _roleService.GetAll(filter));

        [HttpGet("{guid}")]
        [Authorized(ClaimType.Roles_Read)]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var result = await _roleService.GetByGuid(guid);
            return Response(result);
        }

        [HttpPost("New")]
        [Authorized(ClaimType.Roles_Create)]
        public async Task<IActionResult> NewUseRole([FromBody] RoleRequestViewModel role)
        {
            var result = await _roleService.RegisterAsync(role);

            if (result != null)
                return Response(result);

            return StatusCode(200);
        }

        [HttpPut("{guid}")]
        [Authorized(ClaimType.Roles_Write)]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] RoleRequestViewModel role)
        {
            var result = await _roleService.Update(guid, role);

            if (result != null)
                return Response(result);

            return StatusCode(200);
        }

        [HttpGet("Permissions")]
        [Authorized(ClaimType.Roles_Read)]
        public IActionResult GetAllUserClaims() => Response(_roleService.GetAllUserClaims());

        [HttpDelete("{guid}")]
        [Authorized(ClaimType.Roles_Delete)]
        public async Task<IActionResult> RemoveByGuid([FromRoute] Guid guid)
        {
            await _roleService.RemoveByGuid(guid);
            return NoContent();
        }


        #endregion public methods implementations
    }
}
