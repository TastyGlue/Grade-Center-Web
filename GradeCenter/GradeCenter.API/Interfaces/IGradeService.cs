namespace GradeCenter.API.Interfaces
{
    public interface IGradeService
    {
        Task<CustomResult<Guid>> Add(AddGradeRequest request);
        Task<CustomResult<string>> Edit(GradeDto gradeDto);
        Task<IEnumerable<GradeDto>> GetAll(Guid? subjectId, Guid? teacherId);
        Task<GradeDto?> GetById(Guid gradeId);
        Task<CustomResult<string>> Delete(Guid gradeId);
    }
}
