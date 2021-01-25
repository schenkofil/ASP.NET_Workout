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

                foreach(var p in carousels)
                {
                    dBContext.Carousels.Add(p);
                }
                dBContext.SaveChangesAsync();
            }
        }
    }
}
