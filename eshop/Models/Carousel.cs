using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class Carousel
    {
        public int ID { get; set; }
        public string DataTarget { get; set; }
        public string ImageSrc { get; set; }
        public IFormFile Image { get; set; }
        public string ImageAlt { get; set; }
        public string CarouselContent { get; set; }
    }
}
