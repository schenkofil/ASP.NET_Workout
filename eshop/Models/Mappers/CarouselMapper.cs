using AutoMapper;
using eshop.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Mappers
{
    public class CarouselMapper : Profile
    {
        public CarouselMapper()
        {
            CreateMap<Carousel, Carousel>();
        }
    }
}
