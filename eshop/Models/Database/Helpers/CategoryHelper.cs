using eshop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Helpers
{
    public class CategoryHelper
    {
        public static IList<ProductCategory> GenerateCategories()
        {
            var categories = new List<ProductCategory>()
            {
                new ProductCategory()
                    {
                    Name = "notebooky",
                    },
                new ProductCategory()
                    {
                    Name = "monitory",
                    },
                new ProductCategory()
                    {
                    Name = "televize",
                    },
                new ProductCategory()
                    {
                    Name = "chytre-mobily",
                    },
                new ProductCategory()
                    {
                    Name = "playstation",
                    },
                new ProductCategory()
                    {
                    Name = "zrcadlovky",
                    },
            };

            return categories;
        }
    }
}
