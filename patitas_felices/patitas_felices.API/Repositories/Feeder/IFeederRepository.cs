using patitas_felices.Common.Models.User;
using patitas_felices.Common.Responses;
using patitas_felices.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.Feeder;

namespace patitas_felices.API.Repositories
{
    public interface IFeederRepository
    {
        public Task<GetResponseDto<DataCollection<Feeder>>> GetAsync(List<Func<patitas_felices.Common.Models.Feeder.Feeder, bool>> filter, int page, int take);

        public Task<GetResponseDto<FeederGetDto>> GetByIdAsync(string id);
    }
}
