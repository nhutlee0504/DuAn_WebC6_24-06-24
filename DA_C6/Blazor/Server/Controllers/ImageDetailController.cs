using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageDetailController : ControllerBase
    {
        private readonly IImage _imageService;

        public ImageDetailController(IImage imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{productId}")]
        [Route("GetImageDetails/{id}")]
        public IEnumerable<ImageDetails> GetImageDetails(int id)
        {
            return _imageService.GetImages(id);
        }

        [HttpPost]
        public ImageDetails AddImage(ImageDetails imageDetail)
        {
        
            return _imageService.AddImage(new ImageDetails
            {
                IDProduct = imageDetail.IDProduct,
                Image = imageDetail.Image
            });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteImg(int id)
        {
            var result = _imageService.DeleteImage(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent(); 
        }


    }
}
