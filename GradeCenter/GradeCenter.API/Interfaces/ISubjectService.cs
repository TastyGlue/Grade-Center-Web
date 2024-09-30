namespace GradeCenter.API.Interfaces
{
    public interface ISubjectService
    {
        Task<CustomResult<Guid>> Add(AddSubjectRequest request);
        Task<CustomResult<string>> Edit(SubjectDto subjectDto);
        Task<IEnumerable<SubjectDto>> GetAll(Guid? schoolId);
        Task<SubjectDto?> GetById(Guid subjectId);
    }
}
