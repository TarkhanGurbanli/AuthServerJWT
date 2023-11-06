using AuthServer.Core.DTOs;
using AuthServer.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<NoContent>> CreateUserRoles(string userName);
    }
}
