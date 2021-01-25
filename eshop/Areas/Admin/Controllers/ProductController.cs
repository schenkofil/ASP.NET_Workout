using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IHostingEnvironment Env;
        EshopDBContext EshopDBContext;

        public ProductController(IHostingEnvironment env, EshopDBContext dBContext)
        {
            this.Env = env;
            this.EshopDBContext = dBContext;
        }

        public async Task<IActionResult> Select()
        {
            var products = await EshopDBContext.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var fuh = new FileUploadHelper(Env);
            await fuh.UploadFileAsync(product);

            EshopDBContext.Add(product);
            await EshopDBContext.SaveChangesAsync();
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
