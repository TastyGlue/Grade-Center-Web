namespace GradeCenter.API.Interfaces
{
    public interface IStudentService
    {
        Task<CustomResult<Guid>> AddStudent(AddStudentRequest request);
        Task<CustomResult<string>> Edit(StudentDto studentDto);
        Task<IEnumerable<StudentDto>> GetAll(Guid? schoolId);
        Task<StudentDto?> GetById(Guid id);
    }
}
