using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using patitas_felices.API.Persistence;
using patitas_felices.Common;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.Photo;
using patitas_felices.Common.Models.Photo.DTOs;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patitas_felices.API.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private PatitasDbContext _context;
        private IMapper _mapper;

        public PhotoRepository(PatitasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeleteResponseDto> DeleteAsync(string photoId)
        {
            var response = new DeleteResponseDto();
            try
            {
                var actionDelete = _context.Photos.Remove(new Photo() { Id = ""});
                _context.SaveChanges();
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetResponseDto<DataCollection<Photo>>> GetAsync(List<Func<Photo, bool>> filter, int page, int take)
        {
            var response = new GetResponseDto<DataCollection<Photo>>();
            try
            {
                var photos = _context.Photos.ToList();
                filter.ForEach(f => { photos = photos.Where(f).ToList(); });
                var result = await photos.GetPagedAsync(page, take);

                response.Success = true;
                response.Message = "Success";
                response.Content = result;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<PostResponseDto<Photo>> UploadAsync(PhotoCreateDto photoCreateDto)
        {
            var response = new PostResponseDto<Photo>();
            try
            {
                var photoUpload = _mapper.Map<Photo>(photoCreateDto);
                var actionUpload = await _context.Photos.AddAsync(photoUpload);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Success";
                response.Entity = actionUpload.Entity;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
