using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Models
{
    public class Product : Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        [ContentType("image")]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }

        [Required]
        [ForeignKey(nameof(ProductCategory))]
        public int ProductCategoryID { get; set; }
    }
}
