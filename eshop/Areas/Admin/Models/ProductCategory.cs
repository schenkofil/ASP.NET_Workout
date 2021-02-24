using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Models
{
    public class ProductCategory
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
