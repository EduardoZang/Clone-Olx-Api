using AutoMapper;
using OlxApi.Dtos;
using OlxApi.Models;

namespace OlxApi.Profiles{

    public class ImageProfile : Profile {
        public ImageProfile()
        {
            CreateMap<CreateImageDto, Image>();
            CreateMap<UptadeImageDto, Image>();
            CreateMap<Image, UptadeImageDto>();
             CreateMap<Image, ReadImageDto>();
        }
    }
}