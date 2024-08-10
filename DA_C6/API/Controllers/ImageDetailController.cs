using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using API.Services;
using System.Collections.Generic;

namespace API.Controllers
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
        public IEnumerable<ImageDetails> GetImageDetails(int productId)
        {
            return _imageService.GetImages(productId);
        }

        [HttpPost]
        public IActionResult AddImage(string image, int id)
        {
            if (string.IsNullOrEmpty(image))
            {
                return BadRequest();
            }

            var imageDetails = new ImageDetails
            {
                IDProduct = id,
                Image = image
            };

            var result = _imageService.AddImage(image, id);

            if (result == null)
            {
                return StatusCode(500);
            }

            return Ok(result);
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
