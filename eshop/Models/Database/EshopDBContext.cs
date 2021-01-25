using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class EshopDBContext : DbContext
    {
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Product> Products { get; set; }

        public EshopDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
