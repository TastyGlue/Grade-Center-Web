namespace GradeCenter.API.Interfaces
{
    public interface IGradeService
    {
        Task<Response<Guid>> Add(AddGradeRequest request);
        Task<Response<string>> Edit(GradeDto gradeDto);
        Task<IEnumerable<GradeDto>> GetAll(Guid? subjectId, Guid? teacherId);
        Task<GradeDto?> GetById(Guid gradeId);
        Task<Response<string>> Delete(Guid gradeId);
    }
}
