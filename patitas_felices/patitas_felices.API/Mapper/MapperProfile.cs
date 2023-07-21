using AutoMapper;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Models.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace patitas_felices.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //USER
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserGetDto>();

            //FEEDER
            CreateMap<PhotoCreateDto, Photo>();
            CreateMap<FeederUpdateDto, User>();
            CreateMap<Photo, UserGetDto>();
        }
    }
}
