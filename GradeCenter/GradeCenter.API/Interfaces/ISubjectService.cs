namespace GradeCenter.API.Interfaces
{
    public interface ISubjectService
    {
        Task<Response<Guid>> Add(AddSubjectRequest request);
        Task<Response<string>> Edit(SubjectDto subjectDto);
        Task<IEnumerable<SubjectDto>> GetAll(Guid? schoolId);
        Task<SubjectDto?> GetById(Guid subjectId);
    }
}
