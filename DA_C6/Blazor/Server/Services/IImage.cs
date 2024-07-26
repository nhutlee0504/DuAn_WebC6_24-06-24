using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Model;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Services
{
        public interface IImage
        {
            public IEnumerable<ImageDetails> GetImages(int productId);
        public ImageDetails AddImage(ImageDetails image);
        public ImageDetails DeleteImage(int id);
    }
    }
