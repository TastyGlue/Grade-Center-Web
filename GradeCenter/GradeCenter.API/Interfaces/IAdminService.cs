﻿namespace GradeCenter.API.Interfaces
{
    public interface IAdminService
    {
        Task<Response<int>> AddAdmin(string userId);
        Task<Response<string>> Edit(int adminId, AdminDto adminDto);
        Task<IEnumerable<AdminDto>> GetAll();
        Task<AdminDto?> GetById(int id);
    }
}
