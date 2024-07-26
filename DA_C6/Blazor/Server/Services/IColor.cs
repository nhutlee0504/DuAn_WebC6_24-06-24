using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
    public interface IColor
    {
        public IEnumerable<Colors> GetColors();
        public Colors AddColor(Colors colors);
        public Colors GetColorById(int id);
        public Colors UpdateColor(int id, Colors colors);
    }
}
