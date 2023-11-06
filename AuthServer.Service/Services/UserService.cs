using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using AuthServer.Service.Mapping;
using AuthServer.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);

        }

        public async Task<Response<NoContent>> CreateUserRoles(string userName)
        {
            // "Admin" rolünün var olup olmadığını kontrol edin
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                // "Admin" rolü yoksa oluşturun
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // "Manager" rolünün var olup olmadığını kontrol edin
            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                // "Manager" rolü yoksa oluşturun
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            // Kullanıcıyı bulun
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Response<NoContent>.Fail("User not found", 404, true);
            }

            // Kullanıcıyı "Admin" rolüne ekleyin
            var adminResult = await _userManager.AddToRoleAsync(user, "Admin");
            if (!adminResult.Succeeded)
            {
                return Response<NoContent>.Fail(adminResult.Errors.FirstOrDefault()?.Description, 500, true);
            }

            // Kullanıcıyı "Manager" rolüne ekleyin
            var managerResult = await _userManager.AddToRoleAsync(user, "Manager");
            if (!managerResult.Succeeded)
            {
                return Response<NoContent>.Fail(managerResult.Errors.FirstOrDefault()?.Description, 500, true);
            }

            // Her şey başarılıysa, 201 durum kodu ile başarı yanıtı döndürün
            return Response<NoContent>.Success(StatusCodes.Status201Created);
        }


        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return Response<UserAppDto>.Fail("UserName not found", 404, true);

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
