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

namespace patitas_felices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PatitasPolicy")]
    
    public class FeedersController : Controller
    {
        IFeederRepository _feederRepository;
        private PatitasDbContext _dbContext;

        public FeedersController(IFeederRepository feederRepository, PatitasDbContext dbContext)
        {
            _feederRepository= feederRepository;
            _dbContext = dbContext;
        }

        

        [HttpGet]
        public async Task<ActionResult<GetResponseDto<DataCollection<Feeder>>>> GetAllFeeders(int page = 1, int take = 10, string userId="") //to get all feeder
        {
            var filter = new List<Func<Feeder, bool>>() { x => x.Id == x.Id };
            if (userId.Length != 0)
            {
                filter.Add(x => x.UserId == userId);
            }

            var response = await _feederRepository.GetAsync(filter, page, take);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }


        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetResponseDto<FeederGetDto>>> Get(string id)
        {
            var response = await _feederRepository.GetByIdAsync(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

       
    }
}
