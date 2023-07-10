//using VamoPlay.Application.Filters;
//using VamoPlay.Application.Services.Interfaces;
//using VamoPlay.Application.ViewModels.Response;
//using Microsoft.AspNetCore.Mvc;

//namespace VamoPlay.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TournamentCategoryController : BaseController
//    {
//        #region Private Members

//        private readonly ITournamentCategoryService _tournamentCategoryService;

//        #endregion

//        #region Constructors

//        public TournamentCategoryController(ITournamentCategoryService tournamentCategoryService)
//        {
//            _tournamentCategoryService = tournamentCategoryService;
//        }

//        #endregion

//        #region Public Methods

//        [HttpGet]
//        public async Task<IActionResult> GetAll([FromQuery] TournamentCategoryFilter filter)
//           => Response(await _tournamentCategoryService.GetAll(filter));

//        [HttpGet("{tournamentGuid}")]
//        public async Task<IActionResult> GetByGuid([FromRoute] Guid tournamentGuid)
//          => Response(await _tournamentCategoryService.GetByGuid(tournamentGuid));

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] TournamentRequestViewModel tournamentViewModel)
//          => Response(await _tournamentCategoryService.Create(tournamentViewModel));

//        [HttpDelete("{tournamentGuid}")]
//        public async Task<IActionResult> Delete([FromRoute] Guid tournamentGuid)
//            => Response(await _tournamentCategoryService.Delete(tournamentGuid));

//        [HttpPut("favorite/{tournamentGuid}")]
//        public async Task<IActionResult> Favorite([FromRoute] Guid tournamentGuid)
//            => Response(await _tournamentCategoryService.Favorite(tournamentGuid));

//        [HttpPut("{tournamentGuid}")]
//        public async Task<IActionResult> Update([FromRoute] Guid tournamentGuid, [FromBody] TournamentRequestViewModel tournamentViewModel)
//            => Response(await _tournamentCategoryService.Update(tournamentGuid, tournamentViewModel));

//        #endregion
//    }
//}