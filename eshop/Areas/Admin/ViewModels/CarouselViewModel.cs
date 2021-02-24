using eshop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.ViewModels
{
    public class CarouselViewModel
    {
        public IList<Carousel> Carousels { get; set; }
    }
}
