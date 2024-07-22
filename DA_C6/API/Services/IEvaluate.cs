using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface IEvaluate
    {
        public IEnumerable<Evaluate> GetEvaluate();
    }
}
