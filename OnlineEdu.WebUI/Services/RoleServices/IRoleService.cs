﻿using OnlineEdu.WebUI.DTOs.RoleDtos;

namespace OnlineEdu.WebUI.Services.RoleServices
{
    public interface IRoleService
    {
        Task<List<ResultRoleDto>> GetAllRoleAsync();

        Task<UpdateRoleDto> GetRoleByIdAsync(int id);

        Task CreateRoleAsync(CreateRoleDto createRoleDto);

        Task DeleteRoleAsync(int id);
    }
}
