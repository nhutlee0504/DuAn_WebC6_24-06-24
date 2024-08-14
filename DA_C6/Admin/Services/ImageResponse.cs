using Admin.Data;
using Admin.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Services
{
    public class ImageResponse : IImage
    {
        private readonly ApplicationDbContext _context;

        public ImageResponse(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<ImageDetails>> GetImagesAsync(int productId)
        {
            return await _context.ImageDetails
                                 .Where(x => x.IDProduct == productId)
                                 .ToListAsync();
        }

        public async Task<ImageDetails> AddImageAsync(string image, int id)
        {
            var prod = await _context.Products
                                     .FindAsync(id);

            if (prod == null)
            {
                return null;
            }

            var imageDetails = new ImageDetails
            {
                IDProduct = prod.IDProduct,
                Image = image
            };

            try
            {
                await _context.ImageDetails.AddAsync(imageDetails);
                await _context.SaveChangesAsync();
                return imageDetails;
            }
            catch (System.Exception ex)
            {
                // Log exception (ex.Message) here
                return null;
            }
        }

        public async Task<ImageDetails> DeleteImageAsync(int id)
        {
            var image = await _context.ImageDetails
                                      .FindAsync(id);

            if (image == null)
            {
                return null;
            }

            try
            {
                _context.ImageDetails.Remove(image);
                await _context.SaveChangesAsync();
                return image;
            }
            catch (System.Exception ex)
            {
                // Log exception (ex.Message) here
                return null;
            }
        }
    }
}
