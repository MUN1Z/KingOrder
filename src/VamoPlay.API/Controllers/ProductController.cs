using VamoPlay.Application.Filters;
using VamoPlay.Application.Services.Interfaces;
using VamoPlay.Application.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace VamoPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        #region Private Members

        private readonly IProductService _productService;

        #endregion

        #region Constructors

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductFilter filter)
           => Response(await _productService.GetAll(filter));

        [HttpGet("{productGuid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid productGuid)
          => Response(await _productService.GetByGuid(productGuid));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestViewModel productViewModel)
          => Response(await _productService.Create(productViewModel));

        [HttpDelete("{productGuid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productGuid)
            => Response(await _productService.Delete(productGuid));

        [HttpPut("favorite/{productGuid}")]
        public async Task<IActionResult> Favorite([FromRoute] Guid productGuid)
            => Response(await _productService.Favorite(productGuid));

        [HttpPut("{productGuid}")]
        public async Task<IActionResult> Update([FromRoute] Guid productGuid, [FromBody] ProductRequestViewModel productViewModel)
            => Response(await _productService.Update(productGuid, productViewModel));

        #endregion
    }
}