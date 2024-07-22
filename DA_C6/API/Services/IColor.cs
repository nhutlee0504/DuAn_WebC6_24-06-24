using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface IColor
    {
        public IEnumerable<Colors> GetColors();
        public Colors AddColor(Colors colors);
        public Colors GetColorById(int id);
        public Colors UpdateColor(int id, Colors colors);
    }
}
