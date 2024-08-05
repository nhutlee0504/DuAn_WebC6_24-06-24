using Microsoft.AspNetCore.Mvc;
using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services
{
        public interface IImage
        {
            public IEnumerable<ImageDetails> GetImages(int productId);
        public ImageDetails AddImage(ImageDetails image);
        public ImageDetails DeleteImage(int id);
    }
    }
