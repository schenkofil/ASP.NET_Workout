using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Helpers
{
    public class ProductHelper
    {
        public static IList<Product> GenerateProduct()

        {
            IList<Product> products = new List<Product>()
            {
                new Product()
                    {
                    ID = 1,
                    Name = "PC",
                    Price = 999,
                    Weight = 13.8,
                    Color = "Red"
                    },


                 new Product()
                    {
                    ID = 2,
                    Name = "TV box",
                    Price = 5000,
                    Weight = 12.5,
                    Color = "Blue"
                    },

                  new Product()
                    {
                    ID = 3,
                    Name = "Dunno",
                    Price = 25000,
                    Weight = 3.68,
                    Color = "Yellow"
                    },

                   new Product()
                    {
                    ID = 4,
                    Name = "#myCarousel",
                    Price = 2489,
                    Weight = 2.5,
                    Color = "Brown"
                    },

            };
            return products;

        }
    }
}
