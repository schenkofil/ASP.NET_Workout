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
            carousel.ImageSrc = String.Empty;
            var img = carousel.Image;

            if(img != null && img.ContentType.ToLower().Contains("image") && img.Length > 0 && img.Length < 2_000_000)
            {
                string fileNameWithExtension = (Path.GetFileNameWithoutExtension(img.FileName) + Path.GetExtension(img.FileName));
                //TODO: osetrit prepsani stejnojmenneho souboru
                //var filinameGenerated = Path.GetRandomFileName();

                var filePathRelative = Path.Combine("image", "Carousels", fileNameWithExtension);
                var filePath = Path.Combine(env.WebRootPath, "images", "Carousels", fileNameWithExtension);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                carousel.ImageSrc = $"/{filePathRelative}";
            }

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
        public IActionResult Edit(Carousel carousel)
        {
            Carousel selectedCarousel = carousels.Where(x => x.ID == carousel.ID).FirstOrDefault();
            if (selectedCarousel != null) 
            {
                selectedCarousel.DataTarget = carousel.DataTarget;
                selectedCarousel.ImageSrc = carousel.ImageSrc;
                selectedCarousel.ImageAlt = carousel.ImageAlt;
                selectedCarousel.CarouselContent = carousel.CarouselContent;

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
