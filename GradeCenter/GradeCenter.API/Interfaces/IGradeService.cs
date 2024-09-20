namespace GradeCenter.API.Interfaces
{
    public interface IGradeService
    {
        Task<Response<int>> Add(AddGradeRequest request);
        Task<Response<string>> Edit(GradeDto gradeDto);
        Task<IEnumerable<GradeDto>> GetAll(string? subjectId, int? teacherId);
        Task<GradeDto?> GetById(int gradeId);
        Task<Response<string>> Delete(int gradeId);
    }
}
