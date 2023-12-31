﻿using Identity.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Identity;
using patitas_felices.Common.Responses;
using patitas_felices.Common.Models.User.Auth;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Models.User;

namespace patitas_felices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("PatitasPolicy")]
    
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
        }

        [HttpPost("SignUp")] //SignUp
        public async Task<ActionResult<GetResponseDto<TokenInfo>>> SignUp(UserCreateDto userCreateDto)
        {
            var response = await _userRepository.CreateUserAsync(userCreateDto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("Administrator")]
        public async Task<ActionResult<GetResponseDto<TokenInfo>>> Administrator(UserAdministratorDto userAdministratorDto)
        {

            var response = await _userRepository.AddAdministratorRole(userAdministratorDto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("LogIn")] //Login
        public async Task<ActionResult<GetResponseDto<TokenInfo>>> LogIn(LoginDto loginDto)
        {
            var response = await _userRepository.LoginAsync(loginDto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        
        [HttpGet]
        
        public async Task<ActionResult<GetResponseDto<User>>> GetAllUsers(int page = 1, int take = 10)
        {
            List<Func<User, bool>> filter = new List<Func<User, bool>>() { x => x.Id == x.Id };
           
            var response = await _userRepository.GetAsync(filter, page, take);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetResponseDto<UserGetDto>>> Get(string id)
        {
            var response = await _userRepository.GetByIdAsync(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }


        
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteResponseDto>> Delete(string id, [FromQuery] string password)
        {
            var response = await _userRepository.DeleteAsync(new UserDeleteDto() { Id = id, Password = password });
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        
        [HttpPut]
        public async Task<ActionResult<PostResponseDto<UserGetDto>>> Update(UserUpdateDto userUpdateDto)
        {
            var response = await _userRepository.UpdateAsync(userUpdateDto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
