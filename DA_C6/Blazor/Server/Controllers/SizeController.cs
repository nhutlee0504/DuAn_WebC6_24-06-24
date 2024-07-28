using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private ISize size;
        public SizeController(ISize s) => size = s;

        [HttpGet]
        [Route("GetSizes")]
        public IEnumerable<Sizes> GetSizes()
        {
            return size.GetSizes();
        }

        [HttpPost]
        public Sizes Add(Sizes si)
        {
            return size.AddSize(new Sizes
            {
                SizeName = si.SizeName,
            });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Sizes si)
        {
            var updatesize = size.GetSizeId(id);


            if (updatesize == null)
            {
                return NotFound();
            }

            updatesize.SizeName = si.SizeName;
            size.UpdateSizes(id, updatesize);
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult Getsizeid(int id)
        {
            var siz = size.GetSizeId(id);
            if (siz == null)
            {
                return NotFound();
            }
            return Ok(siz);
        }
    }
}
