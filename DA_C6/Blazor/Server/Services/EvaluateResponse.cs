using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public class EvaluateResponse : IEvaluate
    {
        private readonly ApplicationDbContext _context;
        public EvaluateResponse(ApplicationDbContext context)
        {
            _context = context;
        }

		public Evaluate AddEva(Evaluate evaluate)
		{
			_context.Evaluates.Add(evaluate);
            _context.SaveChanges();
            return evaluate;
		}

		public IEnumerable<Evaluate> GetEvaluate()
        {
            return _context.Evaluates;
        }
    }
}
