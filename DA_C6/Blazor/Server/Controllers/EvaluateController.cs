﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;
using Blazor.Server.Data;

namespace Blazor.Server.Controllers
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
