using eshop.Areas.Admin.Models;
using eshop.Models;
using eshop.Models.Database;
using eshop.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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
        readonly EshopDBContext _eshopDBContext;
        readonly ILogger<HomeController> _logger;
        public HomeController(EshopDBContext eshopDBContext, ILogger<HomeController> logger)
        {
            _eshopDBContext = eshopDBContext;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var hvm = new HomeViewModel()
            {
                Carousels = await _eshopDBContext.Carousels.ToListAsync(),
                Products = await _eshopDBContext.Products.ToListAsync(),
                Categories = await _eshopDBContext.ProductCategories.ToListAsync()
            };

            return View(hvm);
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
            
            _logger.LogWarning("Error occured: " + featureException.Error.ToString() + Environment.NewLine + "Exception Path: " + featureException.Path);

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
            _logger.LogWarning("StatusCode: " + statCode + "Original URL:");

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

        public async Task<IActionResult> FilterByCategory(int id)
        {
            var hvm = new HomeViewModel()
            {
                Carousels = await _eshopDBContext.Carousels.ToListAsync(),
                Products = await _eshopDBContext.Products.Where(x => x.ProductCategoryID == id).ToListAsync(),
                Categories = await _eshopDBContext.ProductCategories.ToListAsync()
            };

            return View(nameof(Index), hvm);
        }

        public IActionResult Detail(int id)
        {
            var selectedProduct = _eshopDBContext.Products.FirstOrDefault(x => x.ID == id);
            return View("~/Views/Products/Detail.cshtml", selectedProduct);
        }
    }
}
