using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Services
{
    public interface IImage
    {
        Task<IEnumerable<ImageDetails>> GetImagesAsync(int productId);
        Task<ImageDetails> AddImageAsync(string image, int id);
        Task<ImageDetails> DeleteImageAsync(int id);
    }
}
