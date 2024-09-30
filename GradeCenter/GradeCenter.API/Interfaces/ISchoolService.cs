namespace GradeCenter.API.Interfaces
{
    public interface ISchoolService
    {
        Task<CustomResult<Guid>> Add(AddSchoolRequest request);
        Task<CustomResult<string>> Edit(SchoolDto schoolDto);
        Task<IEnumerable<SchoolDto>> GetAll();
        Task<SchoolDto?> GetById(Guid schoolId);
        Task<CustomResult<string>> Delete(Guid schoolId);
    }
}
