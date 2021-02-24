﻿using eshop.Areas.Admin.Models;
using eshop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database.Helpers
{
    public class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()

        {
            IList<Carousel> carousels = new List<Carousel>()
            {
                new Carousel()
                    {
                    DataTarget = "#myCarousel",
                    ImageSrc="/images/banner1.svg",
                    ImageAlt="ASP.NET",
                    CarouselContent="Learn how to build ASP.NET apps that can run anywhere."
                    },


                 new Carousel()
                    {
                    DataTarget = "#myCarousel",
                    ImageSrc="/images/banner2.svg",
                    ImageAlt="Visual Studio",
                    CarouselContent= "There are powerful new features in Visual Studio for building modern web apps"
                 },

                  new Carousel()
                    {
                    DataTarget = "#myCarousel",
                    ImageSrc="/images/banner3.svg",
                    ImageAlt="Microsoft Azure",
                    CarouselContent= "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps."
                 },

                   new Carousel()
                    {
                    DataTarget = "#myCarousel",
                    ImageSrc="/images/banner3.svg",
                    ImageAlt="Microsoft Azure",
                    CarouselContent= "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps."
                  }

            };
            return carousels;

        }
    }
}
