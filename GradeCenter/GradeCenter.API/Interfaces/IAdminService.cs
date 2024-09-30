namespace GradeCenter.API.Interfaces
{
    public interface IAdminService
    {
        Task<CustomResult<Guid>> AddAdmin(Guid userId);
        Task<CustomResult<string>> Edit(AdminDto adminDto);
        Task<IEnumerable<AdminDto>> GetAll();
        Task<AdminDto?> GetById(Guid id);
    }
}
