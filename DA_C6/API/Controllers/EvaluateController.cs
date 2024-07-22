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
