using eshop.Models.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class DbInitializer
    {
        public static void Initialize(EshopDBContext dBContext)
        {
            if (dBContext.Database.EnsureCreated())
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                IList<Product> products = ProductHelper.GenerateProduct();

                foreach(var p in carousels)
                {
                    dBContext.Carousels.Add(p);
                }
                foreach(var p in products)
                {
                    dBContext.Products.Add(p);
                }
                dBContext.SaveChangesAsync();
            }
        }
    }
}
