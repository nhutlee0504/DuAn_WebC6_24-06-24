using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class ImageResponse : IImage
    {
       private readonly ApplicationDbContext context;
        private readonly IImage _imageService;
        public ImageResponse(ApplicationDbContext ct) => context = ct;
        
		public IEnumerable<ImageDetails> GetImages(int productId)
		{
			return context.ImageDetails.Where(x => x.IDProduct == productId);
		}
        public ImageDetails AddImage(string image, int id)
        {
            var prod = context.Products.FirstOrDefault(x => x.IDProduct == id);
            if (prod == null)
            {
                return null;
            }
            ImageDetails imageDetails = new ImageDetails();
            imageDetails.IDProduct = prod.IDProduct;
            imageDetails.Image = image;
            context.ImageDetails.Add(imageDetails);
            context.SaveChanges();
            return imageDetails;
        }

        public ImageDetails DeleteImage(int id)
        {
            var image = context.ImageDetails.FirstOrDefault(img => img.IDImage == id);

            if (image == null)
            {
                return null;
            }

            context.ImageDetails.Remove(image);
            context.SaveChanges();

            return image;
        }
    }
}
