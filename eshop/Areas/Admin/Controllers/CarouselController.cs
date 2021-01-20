using eshop.Models;
using eshop.Models.DatabaseFake;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        IList<Carousel> carousels = DatabaseFake.Carousels;
        IHostingEnvironment env;

        public CarouselController(IHostingEnvironment env)
        {
            this.env = env;
        }

        public IActionResult Select()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = carousels;
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

            carousels.Add(carousel);
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int id)
        {
            Carousel selectedCarousel = carousels.Where(x => x.ID == id).FirstOrDefault();
            if (selectedCarousel != null) return View(selectedCarousel);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            Carousel selectedCarousel = carousels.Where(x => x.ID == carousel.ID).FirstOrDefault();
            if (selectedCarousel != null) 
            {
                selectedCarousel.DataTarget = carousel.DataTarget;
                selectedCarousel.ImageAlt = carousel.ImageAlt;
                selectedCarousel.CarouselContent = carousel.CarouselContent;

                var fuh = new FileUploadHelper(env);
                await fuh.UploadFileAsync(carousel);

                selectedCarousel.ImageSrc = await fuh.UploadFileAsync(carousel) ? carousel.ImageSrc : String.Empty;

                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }

        public IActionResult Delete(int id)
        {
            Carousel selectedCarousel = carousels.Where(x => x.ID == id).FirstOrDefault();
            if (selectedCarousel != null)
            {
                carousels.Remove(selectedCarousel);
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
