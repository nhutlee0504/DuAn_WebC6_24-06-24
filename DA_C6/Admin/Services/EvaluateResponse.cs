using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
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
