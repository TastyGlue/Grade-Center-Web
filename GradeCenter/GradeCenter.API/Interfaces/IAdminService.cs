namespace GradeCenter.API.Interfaces
{
    public interface IAdminService
    {
        Task<Response<Guid>> AddAdmin(Guid userId);
        Task<Response<string>> Edit(AdminDto adminDto);
        Task<IEnumerable<AdminDto>> GetAll();
        Task<AdminDto?> GetById(Guid id);
    }
}
