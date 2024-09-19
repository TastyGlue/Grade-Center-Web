namespace GradeCenter.API.Interfaces
{
    public interface IClassService
    {
        Task<Response<Guid>> Add(AddClassRequest request);
        Task<Response<string>> Edit(ClassDto classDto);
        Task<IEnumerable<ClassDto>> GetAll(string? schoolId);
        Task<ClassDto?> GetById(Guid classId);
        Task<Response<string>> Delete(Guid classId);
    }
}
