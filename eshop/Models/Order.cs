using eshop.Areas.Admin.Models;
using eshop.Models.Identity;
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

        [StringLength(25)]
        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [ForeignKey(nameof(Identity.User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
