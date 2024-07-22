using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IEvaluate
    {
        public IEnumerable<Evaluate> GetEvaluate();
    }
}
