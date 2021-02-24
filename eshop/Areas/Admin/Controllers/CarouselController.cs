using eshop.Areas.Admin.Models;
using eshop.Areas.Admin.ViewModels;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
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
    //ToDo: user se sem nedostane, pokud se pouziva Autorize -> vyresit
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly EshopDBContext _eshopDBContext;

        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env)
        {
            _env = env;
            _eshopDBContext = eshopDBContext;
        }

        public async Task<IActionResult> Select()
        {
            CarouselViewModel carousel = new CarouselViewModel();
            carousel.Carousels = await _eshopDBContext.Carousels.ToListAsync();
            return View(carousel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            if (!ModelState.IsValid)
            {
                return View(carousel);
            }

            var fuh = new FileUploadHelper(_env.WebRootPath, "Carousels", "image");
            carousel.ImageSrc = await fuh.UploadFileAsync(carousel.Image);

            _eshopDBContext.Add(carousel);
            await _eshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Select));
        }

        public IActionResult Edit(int id)
        {
            var selectedCarousel = _eshopDBContext.Carousels.FirstOrDefault(x => x.ID == id);
            if (selectedCarousel != null) return View(selectedCarousel);
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            Carousel selectedCarousel = _eshopDBContext.Carousels.FirstOrDefault(x => x.ID == carousel.ID);
            if (ModelState.IsValid) 
            {
                selectedCarousel.DataTarget = carousel.DataTarget;
                selectedCarousel.ImageAlt = carousel.ImageAlt;
                selectedCarousel.CarouselContent = carousel.CarouselContent;

                var fuh = new FileUploadHelper(_env.WebRootPath, "Carousels", "image");

                if(!String.IsNullOrWhiteSpace(carousel.ImageSrc = await fuh.UploadFileAsync(carousel.Image)))
                {
                    selectedCarousel.ImageSrc = carousel.ImageSrc;
                }

                await _eshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Select));
            }
            else return View(carousel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Carousel selectedCarousel = _eshopDBContext.Carousels.FirstOrDefault(x => x.ID == id);
            if (selectedCarousel != null)
            {
                _eshopDBContext.Carousels.Remove(selectedCarousel);
                await _eshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else return NotFound();
        }
    }
}
