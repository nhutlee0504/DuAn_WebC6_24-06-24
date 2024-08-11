using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public interface IEvaluate
    {
        public IEnumerable<Evaluate> GetEvaluate();

        public Evaluate AddEva(Evaluate evaluate);
    }
}
