using AutoMapper;
using OlxApi.Dtos;
using OlxApi.Models;

namespace OlxApi.Profiles{

    public class AdProfile : Profile {
        public AdProfile()
        {
            CreateMap<CreateAdDto, Ad>();
            CreateMap<UptadeAdDto, Ad>();
            CreateMap<Ad, UptadeAdDto>();
             CreateMap<Ad, ReadAdDto>();
        }
    }
}