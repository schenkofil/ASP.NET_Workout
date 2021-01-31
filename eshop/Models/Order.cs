using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table(nameof(Order))]
    public class Order : Entity
    {

        [Required]
        [StringLength(25)]
        public string OrderNumber { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
