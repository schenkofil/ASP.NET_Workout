using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
