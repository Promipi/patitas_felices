using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using patitas_felices.API.Repositories;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Responses;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.API.Persistence;
using patitas_felices.Common;
using patitas_felices.Common.Models.Photo;
using patitas_felices.Common.Models.Photo.DTOs;

namespace patitas_felices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PatitasPolicy")]
    
    public class PhotosController : Controller
    {
        IPhotoRepository _photoRepository;
        private PatitasDbContext _dbContext;

        public PhotosController(IPhotoRepository photoRepository, PatitasDbContext dbContext)
        {
            _photoRepository = photoRepository;
            _dbContext = dbContext;
        }

        

        [HttpGet]
        public async Task<ActionResult<GetResponseDto<DataCollection<Feeder>>>> GetAllPhotos(int page = 1, int take = 10, string feederId="") //to get all feeder
        {
            var filter = new List<Func<Photo, bool>>() { x => x.Id == x.Id };
            if (feederId.Length != 0)
            {
                filter.Add(x => x.FeederId == feederId);
            }

            var response = await _photoRepository.GetAsync(filter, page, take);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<GetResponseDto<DataCollection<Feeder>>>> UploadPhoto(PhotoCreateDto photoCreateDto) //to get all feeder
        {
            var response = await _photoRepository.UploadAsync(photoCreateDto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<DeleteResponseDto>> DeletePhoto(string photoId) //to get all feeder
        {
            var response = await _photoRepository.DeleteAsync(photoId);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }


    }
}
