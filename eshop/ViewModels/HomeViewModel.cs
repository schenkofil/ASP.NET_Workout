using eshop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.ViewModels
{
    public class HomeViewModel
    {
        public List<Carousel> Carousels { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}
