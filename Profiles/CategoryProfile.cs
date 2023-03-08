using AutoMapper;
using OlxApi.Dtos;
using OlxApi.Models;

namespace OlxApi.Profiles{

    public class CategoryProfile : Profile {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UptadeCategoryDto, Category>();
            CreateMap<Category, UptadeCategoryDto>();
             CreateMap<Category, ReadCategoryDto>();
        }
    }
}