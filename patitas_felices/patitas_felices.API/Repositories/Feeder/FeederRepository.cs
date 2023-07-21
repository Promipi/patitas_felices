using AutoMapper;
using Microsoft.AspNetCore.Identity;
using patitas_felices.API.Persistence;
using patitas_felices.Common;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Feeder.DTOs;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patitas_felices.API.Repositories
{
    public class FeederRepository : IFeederRepository
    {
        private PatitasDbContext _context;
        private IMapper _mapper;

        public FeederRepository(PatitasDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetResponseDto<DataCollection<Feeder>>> GetAsync(List<Func<Feeder, bool>> filter, int page, int take)
        {
            var response = new GetResponseDto<DataCollection<Feeder>>();
            try
            {
                var feeders = _context.Feeders.ToList();
                filter.ForEach(f => { feeders = feeders.Where(f).ToList(); });
                var result = await feeders.GetPagedAsync(page, take);

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

        public async Task<GetResponseDto<FeederGetDto>> GetByIdAsync(string id)
        {
            var response = new GetResponseDto<FeederGetDto>();
            var user = await _context.FindAsync<Feeder>(id);
            if (user == null)
            {
                response.Message = "The user doesn't exist";
            }
            else
            {
                response.Success = true; response.Content = _mapper.Map<FeederGetDto>(user);
            }
            return response;
        }
    }
}
