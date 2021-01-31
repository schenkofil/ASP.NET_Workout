using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
