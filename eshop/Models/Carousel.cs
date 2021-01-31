using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table("Carousel")]
    public class Carousel : Entity
    {
        [Required]
        public string DataTarget { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }

        [NotMapped]
        [ContentType("image")]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageAlt { get; set; }

        [Required]
        [StringLength(255)]
        public string CarouselContent { get; set; }
    }
}
