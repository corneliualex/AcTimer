using AcTimer.DTOs;
using AcTimer.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcTimer.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            Mapper.CreateMap<Category, CategoryDto>();
            Mapper.CreateMap<CategoryDto, Category>();
            Mapper.CreateMap<Activity, ActivityDto>();
            Mapper.CreateMap<ActivityDto, Activity>();
        }
    }
}