namespace GradeCenter.API.Interfaces
{
    public interface IClassService
    {
        Task<CustomResult<Guid>> Add(AddClassRequest request);
        Task<CustomResult<string>> Edit(ClassDto classDto);
        Task<IEnumerable<ClassDto>> GetAll(Guid? schoolId);
        Task<ClassDto?> GetById(Guid classId);
        Task<CustomResult<string>> Delete(Guid classId);
    }
}
