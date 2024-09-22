namespace GradeCenter.API.Interfaces
{
    public interface IAbsenceService
    {
        Task<Response<int>> Add(AddAbsenceRequest request);
        Task<Response<string>> Edit(AbsenceDto absenceDto);
        Task<IEnumerable<AbsenceDto>> GetAll(int? studentId);
        Task<AbsenceDto?> GetById(int absenceId);
        Task<Response<string>> Delete(int absenceId);
    }
}
