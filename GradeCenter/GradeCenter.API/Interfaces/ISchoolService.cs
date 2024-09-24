namespace GradeCenter.API.Interfaces
{
    public interface ISchoolService
    {
        Task<Response<Guid>> Add(AddSchoolRequest request);
        Task<Response<string>> Edit(SchoolDto schoolDto);
        Task<IEnumerable<SchoolDto>> GetAll();
        Task<SchoolDto?> GetById(Guid schoolId);
        Task<Response<string>> Delete(Guid schoolId);
    }
}
