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
    public class CarouselController : Controller
    {
        IList<Carousel> carousels = DatabaseFake.Carousels;

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
        public IActionResult Create(Carousel carousel)
        {
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
