using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using VamoPlay.CrossCutting.Auth.Attributes;
using VamoPlay.Domain.Enums;

namespace VamoPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : BaseController
    {
        #region Private Members

        private readonly ITournamentService _tournamentService;

        #endregion

        #region Constructors

        public TournamentController(ITournamentService toournamentService)
        {
            _tournamentService = toournamentService;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Authorized(ClaimType.Tournament_Read)]
        public async Task<IActionResult> GetAll([FromQuery] TournamentFilter filter)
           => Response(await _tournamentService.GetAll(filter));

        [HttpGet("{tournamentGuid}")]
        [Authorized(ClaimType.Tournament_Read)]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid tournamentGuid)
          => Response(await _tournamentService.GetByGuid(tournamentGuid));

        [HttpPost]
        [Authorized(ClaimType.Tournament_Create)]
        public async Task<IActionResult> Create([FromBody] TournamentRequestViewModel tournamentViewModel)
          => Response(await _tournamentService.Create(tournamentViewModel));

        [HttpDelete("{tournamentGuid}")]
        [Authorized(ClaimType.Tournament_Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid tournamentGuid)
            => Response(await _tournamentService.Delete(tournamentGuid));

        [HttpPut("{tournamentGuid}")]
        [Authorized(ClaimType.Tournament_Write)]
        public async Task<IActionResult> Update([FromRoute] Guid tournamentGuid, [FromBody] TournamentRequestViewModel tournamentViewModel)
            => Response(await _tournamentService.Update(tournamentGuid, tournamentViewModel));

        #endregion
    }
}