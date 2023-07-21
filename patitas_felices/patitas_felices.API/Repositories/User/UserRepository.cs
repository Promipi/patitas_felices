using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using patitas_felices.API.Persistence;
using patitas_felices.Common;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Models.User.Auth;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IMapper _mapper;
        private PatitasDbContext _context;
        private IConfiguration _configuration;
        private RoleManager<IdentityRole> _roleManager;

        public UserRepository(PatitasDbContext context, UserManager<User> userManager , SignInManager<User> signInManager,IMapper mapper,IConfiguration configuration,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager; _signInManager = signInManager; _mapper = mapper;
            _context = context; _configuration = configuration;
            _roleManager = roleManager;
            
        }

        public async Task<GetResponseDto<TokenInfo>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var response = new GetResponseDto<TokenInfo>();
            var user = _mapper.Map<User>(userCreateDto);
            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            if (result.Succeeded)
            {
                response.Success = true; response.Message = "User Created";
                response.Content = await BuildToken(user); 
            }
            else
            {
                result.Errors.ToList().ForEach(x =>
                {
                    response.Message += x.Description + "\n";
                });
            }
            
            return response;
        }

        public async Task<GetResponseDto<TokenInfo>> LoginAsync(LoginDto loginDto)
        {
            var response = new GetResponseDto<TokenInfo>();
            SignInResult result = new SignInResult();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                response.Message = "The user doesnt exists";
            }
            
            else
            {
                result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, true);
                if (result.IsLockedOut) response.Message = "Your acoount is locked for any rason";
                if (result.Succeeded)
                {
                    response.Success = true; response.Content = await BuildToken(user);
                }
                else
                {
                    response.Message = "Invalid Credentials";
                }
            }
            
            return response;
        }


        public async Task<DeleteResponseDto> DeleteAsync(UserDeleteDto userDeleteDto)
        {
            var response = new DeleteResponseDto();
            var user = await _userManager.FindByIdAsync(userDeleteDto.Id);
            if (user == null)
            {
                response.Message = "Invalid Credetianls";
            }

            var result = await _signInManager.PasswordSignInAsync(user,userDeleteDto.Password, false, true);
            if (result.IsLockedOut) response.Message = "Your acoount is locked for any rason";

            if (result.Succeeded)
            {
                response.Success = true; response.Message = "User Deleted";
                await _userManager.DeleteAsync(new User() { Id = userDeleteDto.Id }); await _context.SaveChangesAsync();
            }
            else
            {
                response.Message = "Invalid Credentials";
            }      
            return response;
        }

        public async Task<GetResponseDto<DataCollection<User>>> GetAsync(List<Func<User, bool>> filter, int page, int take)
        {
            var response = new GetResponseDto<DataCollection<User>>();
            try
            {
                var users = _context.Users.ToList();
                filter.ForEach(f => { users = users.Where(f).ToList(); });
                var result = await users.GetPagedAsync(page, take);

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

        public async Task<GetResponseDto<UserGetDto>> GetByIdAsync(string id)
        {
            var response = new GetResponseDto<UserGetDto>();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                response.Message = "The user doesn't exist";
            }
            else
            {
                response.Success = true; response.Content = _mapper.Map<UserGetDto>(user);
            }
            return response;
        }

       
        public async Task<PostResponseDto<UserGetDto>> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var response = new PostResponseDto<UserGetDto>();
            try
            {
                var user = _context.Users.Find(userUpdateDto.Id);
                user.UserName = userUpdateDto.Username;

                var result = _context.Users.Update(user);
                _context.SaveChanges();
                response.Success = true; response.Entity = _mapper.Map<UserGetDto>(result.Entity); 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private async Task<TokenInfo> BuildToken(User userInfo)
        {
            var claims = new List<Claim> //the claims are created
            {
                new Claim(JwtRegisteredClaimNames.NameId,userInfo.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName,userInfo.UserName),
            };

            foreach (var role in await _userManager.GetRolesAsync(userInfo))
            {
                claims.Add(new Claim("role", role)); //user roles have be added
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["api_key"])); //for now the private key is matita
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(30);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
            );

            return (new TokenInfo
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            }); //return the info token
        }

        public async Task<GetResponseDto<TokenInfo>> AddAdministratorRole(UserAdministratorDto userAdministratorDto)
        {
            
            var response = new GetResponseDto<TokenInfo>();
            if (userAdministratorDto.Password == "aguantegustavo")
            {
                var user = await _userManager.FindByIdAsync(userAdministratorDto.Id);
                var result = await _userManager.AddToRoleAsync(user, "Administrator");
                if (result.Succeeded) {
                    response.Success = true; response.Message = "Administrator role added";                 
                    response.Content = await BuildToken(user);
                }
            else
            {
                result.Errors.ToList().ForEach(x => response.Message += x + "\n");
            }
            }
            else {
                response.Message = "The password to be an administrator is incorrect";
            }
            return response;
        }
    }
}
