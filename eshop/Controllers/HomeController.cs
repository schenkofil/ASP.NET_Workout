using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class HomeController : Controller
    {
        readonly EshopDBContext EshopDBContext;//todo - private
        readonly ILogger<HomeController> Logger;
        public HomeController(EshopDBContext eshopDBContext, ILogger<HomeController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            CarouselViewModel carousel = new CarouselViewModel();//todo - var
            carousel.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(carousel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var featureException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
            this.Logger.LogWarning("Error occured: " + featureException.Error.ToString() + Environment.NewLine + "Exception Path: " + featureException.Path);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorCodeStatus(int? code = null)
        {
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if(features != null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            var statCode = code.HasValue ? code.Value : 0;
            this.Logger.LogWarning("StatusCode: " + statCode + "Original URL:");

            if(code.HasValue && code.Value == 404)
            {
                var vm404 = new _404ViewModel()
                {
                    StatusCode = code.HasValue ? code.Value : 0
                };
            return View(code.ToString(), vm404);
            }

            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = code.HasValue ? code.Value : 0,
                OriginalURL = originalURL
            };
            return View(vm);
        }
    }
}
