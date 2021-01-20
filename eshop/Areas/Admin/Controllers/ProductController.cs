using eshop.Models;
using eshop.Models.DatabaseFake;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IList<Product> products = DatabaseFake.Products;

        public IActionResult Select()
        {
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            products.Add(product);
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int id)
        {
            Product selectedProduct = products.Where(x => x.ID == id).FirstOrDefault();
            if (selectedProduct != null) return View(selectedProduct);
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            Product selectedProduct = products.Where(x => x.ID == product.ID).FirstOrDefault();
            if (selectedProduct != null)
            {
                selectedProduct.Name = product.Name;
                selectedProduct.Price = product.Price;
                selectedProduct.Weight = product.Weight;
                selectedProduct.Color = product.Color;

                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }

        public IActionResult Delete(int id)
        {
            Product selectedProduct = products.Where(x => x.ID == id).FirstOrDefault();
            if (selectedProduct != null)
            {
                products.Remove(selectedProduct);
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
