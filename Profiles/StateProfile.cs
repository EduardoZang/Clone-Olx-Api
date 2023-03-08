using AutoMapper;
using OlxApi.Dtos;
using OlxApi.Models;

namespace OlxApi.Profiles{

    public class StateProfile : Profile {
        public StateProfile()
        {
            CreateMap<CreateStateDto, State>();
            CreateMap<UptadeStateDto, State>();
            CreateMap<State, UptadeStateDto>();
             CreateMap<State, ReadStateDto>();
        }
    }
}