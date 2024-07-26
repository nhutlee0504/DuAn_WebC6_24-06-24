using System.Collections.Generic;
using System.Linq;
using Blazor.Shared.Model;
namespace Blazor.Server.Services
{
    public interface ISize
    {
        public IEnumerable<Sizes> GetSizes();

        public Sizes AddSize(Sizes sizes);
        public Sizes GetSizeId(int id);
        public Sizes UpdateSizes(int id, Sizes upsize);

    }
}
