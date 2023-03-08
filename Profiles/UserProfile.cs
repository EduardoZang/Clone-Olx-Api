using AutoMapper;
using OlxApi.Dtos;
using OlxApi.Models;

namespace OlxApi.Profiles{

    public class UserProfile : Profile {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UptadeUserDto, User>();
            CreateMap<User, UptadeUserDto>();
             CreateMap<User, ReadUserDto>();
        }
    }
}