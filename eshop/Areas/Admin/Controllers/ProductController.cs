using eshop.Areas.Admin.Models;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly EshopDBContext _eshopDBContext;
        //readonly ILogger<ProductController> _log;

        public ProductController(IHostingEnvironment env, EshopDBContext dBContext)
        {
            _env = env;
            _eshopDBContext = dBContext;
        }

        public async Task<IActionResult> Select()
        {
            //_log.LogInformation("Producs selection now!");
            var products = await _eshopDBContext.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var fuh = new FileUploadHelper(_env.WebRootPath, "Products", "image");
            await fuh.UploadFileAsync(product.Image);

            _eshopDBContext.Add(product);
            await _eshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int id)
        {
            Product selectedProduct = _eshopDBContext.Products.FirstOrDefault(x => x.ID == id);
            if (selectedProduct != null) return View(selectedProduct);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            Product selectedProduct = _eshopDBContext.Products.FirstOrDefault(x => x.ID == product.ID);
            //if (ModelState.IsValid)
            //{
                selectedProduct.Name = product.Name;
                selectedProduct.Price = product.Price;

                //var fuh = new FileUploadHelper(env.WebRootPath, "Products", "image");

                //if (!String.IsNullOrWhiteSpace(carousel.ImageSrc = await fuh.UploadFileAsync(carousel.Image)))
                //{
                //    selectedCarousel.ImageSrc = carousel.ImageSrc;
                //}

                await _eshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Select));
            //}
            //else return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product selectedProduct = _eshopDBContext.Products.FirstOrDefault(x => x.ID == id);
            if (selectedProduct != null)
            {
                _eshopDBContext.Products.Remove(selectedProduct);
                await _eshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
