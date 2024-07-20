using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public interface ISize
    {
        public IEnumerable<Sizes> GetSizes();

        public Sizes AddSize(Sizes sizes);
        public Sizes GetSizeId(int id);
        public Sizes UpdateSizes(int id, Sizes upsize);

    }
}
