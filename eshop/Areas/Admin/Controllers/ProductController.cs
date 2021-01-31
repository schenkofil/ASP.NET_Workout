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
            var fuh = new FileUploadHelper(Env.WebRootPath, "Products", "image");
            await fuh.UploadFileAsync(product.Image);

            EshopDBContext.Add(product);
            await EshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Select));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product != null)
            {
                product.Name = product.Name;
                product.Price = product.Price;
                product.Weight = product.Weight;
                product.Color = product.Color;

                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product selectedProduct = EshopDBContext.Products.Where(x => x.ID == id).FirstOrDefault();
            if (selectedProduct != null)
            {
                EshopDBContext.Products.Remove(selectedProduct);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
