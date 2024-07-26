using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public class EvaluateResponse : IEvaluate
    {
        private readonly ApplicationDbContext _context;
        public EvaluateResponse(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Evaluate> GetEvaluate()
        {
            return _context.Evaluates;
        }
    }
}
