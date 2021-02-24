using eshop.Areas.Admin.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eshop.Models.Database.Helpers
{
    public class ProductHelper
    {
        public static IList<Product> GenerateProduct(List<ProductCategory> categories)
        {
            IList<Product> products = new List<Product>();

            string baseUrl = "https://www.electroworld.cz/";
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(new List<string>() { "--silent-launch", "--no-startup-window", "no-sandbox", "headless", "disable-gpu" });

            using (var driver = new ChromeDriver("C:\\Users\\Filip\\.nuget\\packages\\selenium.webdriver.chromedriver\\88.0.4324.9600\\driver\\win32", chromeOptions))
            {
                for (int j = 0; j < categories.Count; j++)
                {
                    for (int q = 0; q < 4; q++)
                    {
                        driver.Url = q == 0 ? $"{baseUrl}{categories[j].Name}" : $"{baseUrl}{categories[j].Name}?page={q}";
                        IList<IWebElement> imagesAndAlts = driver.FindElements(By.ClassName("product-box__image"));
                        IList<IWebElement> prices = driver.FindElements(By.ClassName("product-box__price"));
                        if (imagesAndAlts.Count != 0 && prices.Count != 0)
                        {
                            for (int i = 0; i < imagesAndAlts.Count; i++)
                            {
                                var product = new Product();
                                product.Name = imagesAndAlts[i].FindElement(By.ClassName("img-box__img")).GetAttribute("alt");
                                product.ImageSrc = imagesAndAlts[i].FindElement(By.ClassName("img-box__img")).GetAttribute("src");
                                product.Price = Int32.Parse(prices[i].Text.Replace(" ", "").Replace("Kč", ""));
                                product.ProductCategoryID = categories[j].ID;

                                if (product.ImageSrc.Contains("http")) products.Add(product);
                            }
                        }
                    }
                }
            }

            return products;

        }
    }
}
