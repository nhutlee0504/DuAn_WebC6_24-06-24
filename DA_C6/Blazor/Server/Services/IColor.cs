using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public interface IColor
    {
        public IEnumerable<Colors> GetColors();
        public Colors AddColor(Colors colors);
        public Colors GetColorById(int id);
        public Colors UpdateColor(int id, Colors colors);
    }
}
