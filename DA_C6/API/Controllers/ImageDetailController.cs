﻿using Microsoft.AspNetCore.Http;
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
