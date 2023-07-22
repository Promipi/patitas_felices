using AutoMapper;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.Photo;
using patitas_felices.Common.Models.Photo.DTOs;
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
            CreateMap<FeederCreateDto, Feeder>();
            CreateMap<FeederUpdateDto, Feeder>();
            CreateMap<Feeder, FeederGetDto>();

            //PHOTO
            CreateMap<PhotoCreateDto, Photo>();
            CreateMap<PhotoUpdateDto, Photo>();
            CreateMap<Photo, PhotoGetDto>();

        }
    }
}
