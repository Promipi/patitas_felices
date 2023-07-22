using patitas_felices.Common.Models.User;
using patitas_felices.Common.Responses;
using patitas_felices.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Photo.DTOs;
using patitas_felices.Common.Models.Photo;

namespace patitas_felices.API.Repositories
{
    public interface IPhotoRepository
    {
        public Task<GetResponseDto<DataCollection<Photo>>> GetAsync(List<Func<patitas_felices.Common.Models.Photo.Photo, bool>> filter, int page, int take);

        public Task<DeleteResponseDto> DeleteAsync(string photoId);

        public Task<PostResponseDto<Photo>> UploadAsync(PhotoCreateDto photoCreateDto);
    }
}
