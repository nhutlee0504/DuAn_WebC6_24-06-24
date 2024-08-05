using System.Collections.Generic;
using System.Linq;
using Admin.Model;
namespace Admin.Services
{
    public interface ISize
    {
        public IEnumerable<Sizes> GetSizes();

        public Sizes AddSize(Sizes sizes);
        public Sizes GetSizeId(int id);
        public Sizes UpdateSizes(int id, Sizes upsize);

    }
}
