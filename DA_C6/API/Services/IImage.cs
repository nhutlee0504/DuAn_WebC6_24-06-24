using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
        public interface IImage
        {
            public IEnumerable<ImageDetails> GetImages(int productId);
        public ImageDetails AddImage(string image, int id);
        public ImageDetails DeleteImage(int id);
    }
    }
