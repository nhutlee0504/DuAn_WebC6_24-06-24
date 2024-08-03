using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
{
    public interface IColor
    {
        public IEnumerable<Colors> GetColors();
        public Colors AddColor(Colors colors);
        public Colors GetColorById(int id);
        public Colors UpdateColor(int id, Colors colors);
    }
}
