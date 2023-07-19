
using Microsoft.AspNetCore.Identity;
using patitas_felices.Common;
using patitas_felices.Common.Models.User;
using patitas_felices.Common.Models.User.Auth;
using patitas_felices.Common.Models.User.DTOs;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services.Repository
{
    public partial interface IUserRepository
    {
        public Task<GetResponseDto<DataCollection<User>>> GetAsync(List<Func<User, bool>> filter, int page, int take);

        public Task<PostResponseDto<UserGetDto>> UpdateAsync(UserUpdateDto helpUpdateDto);

        public Task<DeleteResponseDto> DeleteAsync(UserDeleteDto userDeleteDto);

        public Task<GetResponseDto<UserGetDto>> GetByIdAsync(string id);
    }

    public partial interface IUserRepository
    {
        public Task<GetResponseDto<TokenInfo>> LoginAsync(LoginDto loginDto);

        public Task<GetResponseDto<TokenInfo>> CreateUserAsync(UserCreateDto userCreateDto);

        public Task<GetResponseDto<TokenInfo>> AddAdministratorRole(UserAdministratorDto userAdministratorDto);
    }
}
