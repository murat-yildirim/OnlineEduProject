﻿using Microsoft.AspNetCore.Identity;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.UserDtos;

namespace OnlineEdu.WebUI.Services.UserServices
{
    public interface IUserServices
    {
        Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegisterDto);

        Task<string> LoginAsync(UserLoginDto userLoginDto);
        Task LogoutAsync();

        Task<bool> CreateRoleAsync(UserRoleDto userRoleDto);

        Task<bool> AssignRoleAsync(List<AssignRoleDto> assignRoleDto);

        Task<List<AppUser>> GetAllUsersAsync();

        Task<List<ResultUserDto>> Get4Teachers(); 

        Task<AppUser> GetUserByIdAsync(int id);

        Task<int> GetTeacherCount();

        Task<List<ResultUserDto>> GetAllTeachers();
    }
}
