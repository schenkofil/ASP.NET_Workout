using eshop.Models;
using eshop.Models.Database;
using eshop.Models.DatabaseFake;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        IHostingEnvironment env;
        EshopDBContext EshopDBContext;

        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env)
        {
            this.EshopDBContext = eshopDBContext;
            this.env = env;
        }

        public async Task<IActionResult> Select()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(carousel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            var fuh = new FileUploadHelper(env);
            await fuh.UploadFileAsync(carousel);

            EshopDBContext.Add(carousel);
            await EshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int id)
        {
            Carousel selectedCarousel = EshopDBContext.Carousels.Where(x => x.ID == id).FirstOrDefault();
            if (selectedCarousel != null) return View(selectedCarousel);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            Carousel selectedCarousel = EshopDBContext.Carousels.Where(x => x.ID == carousel.ID).FirstOrDefault();
            if (selectedCarousel != null) 
            {
                selectedCarousel.DataTarget = carousel.DataTarget;
                selectedCarousel.ImageAlt = carousel.ImageAlt;
                selectedCarousel.CarouselContent = carousel.CarouselContent;

                var fuh = new FileUploadHelper(env);
                await fuh.UploadFileAsync(carousel);

                selectedCarousel.ImageSrc = await fuh.UploadFileAsync(carousel) ? carousel.ImageSrc : String.Empty;

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            Carousel selectedCarousel = EshopDBContext.Carousels.Where(x => x.ID == id).FirstOrDefault();
            if (selectedCarousel != null)
            {
                EshopDBContext.Carousels.Remove(selectedCarousel);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
