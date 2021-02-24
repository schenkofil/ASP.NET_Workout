using eshop.Areas.Admin.Models;
using eshop.Models.Database.Helpers;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class DbInitializer
    {
        public static void Initialize(EshopDBContext dBContext)
        {
            dBContext.Database.EnsureCreated();

            if(dBContext.ProductCategories.Count() == 0)
            {
                IList<ProductCategory> categories = CategoryHelper.GenerateCategories();
                foreach(var p in categories)
                {
                    dBContext.ProductCategories.Add(p);
                }
                dBContext.SaveChanges();
            }

            if (dBContext.Carousels.Count() == 0)
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                foreach (var p in carousels)
                {
                    dBContext.Carousels.Add(p);
                }
                dBContext.SaveChanges();
            }

            if (dBContext.Products.Count() == 0)
            {
                IList<Product> products = ProductHelper.GenerateProduct(dBContext.ProductCategories.ToList());
                foreach (var p in products)
                {
                    dBContext.Products.Add(p);
                }
                dBContext.SaveChanges();
            }

        }

        public async static void EnsureRoleCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                var roleManager = services.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                string[] roles = Enum.GetNames(typeof(Roles));

                foreach(var p in roles)
                {
                    await roleManager.CreateAsync(new Role(p));
                }

            }
        }

        public async static void EnsureAdminCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {

                //admin
                var userManager = services.ServiceProvider.GetRequiredService<UserManager<User>>();
                var admin = new User()
                {
                    UserName = "admin",
                    Email = "f_schenk@utb.cz",
                    FirstName = "Filip",
                    LastName = "Schenk",
                    EmailConfirmed = true
                };
                var password = "Srandicka#1";

                var adminInDb = await userManager.FindByNameAsync(admin.UserName);

                if (adminInDb == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(admin, password);

                    if (iResult.Succeeded)
                    {

                        string[] roles = Enum.GetNames(typeof(Roles));
                        foreach (var p in roles)
                        {
                            await userManager.AddToRoleAsync(admin, p);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach(var er in iResult.Errors)
                        {
                            Debug.WriteLine($"Error during role creation: {er.Code} => {er.Description}");
                        }
                    }
                }

                //manager
                var manager = new User()
                {
                    UserName = "manager",
                    Email = "manager_f_schenk@utb.cz",
                    FirstName = "Filip",
                    LastName = "Schenk",
                    EmailConfirmed = true
                };

                var managerInDb = await userManager.FindByNameAsync(manager.UserName);

                if (managerInDb == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(manager, password);

                    if (iResult.Succeeded)
                    {

                        string[] roles = Enum.GetNames(typeof(Roles));
                        foreach (var p in roles)
                        {
                            if(p != Roles.Admin.ToString())
                            {
                                await userManager.AddToRoleAsync(manager, p);
                            }
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach (var er in iResult.Errors)
                        {
                            Debug.WriteLine($"Error during role creation: {er.Code} => {er.Description}");
                        }
                    }
                }
            }
        }
    }
}
