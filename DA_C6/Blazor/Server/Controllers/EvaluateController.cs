using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using Blazor.Services;
using System.Collections.Generic;

namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluateController : ControllerBase
    {
        private IEvaluate _evaluate;
        public EvaluateController(IEvaluate eva)
        {
            _evaluate = eva;
        }

        [HttpGet]
        public IEnumerable<Evaluate> Get()
        {
            return _evaluate.GetEvaluate();
        }
    }
}
