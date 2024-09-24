namespace GradeCenter.API.Interfaces
{
    public interface IAbsenceService
    {
        Task<Response<Guid>> Add(AddAbsenceRequest request);
        Task<Response<string>> Edit(AbsenceDto absenceDto);
        Task<IEnumerable<AbsenceDto>> GetAll(Guid? studentId);
        Task<AbsenceDto?> GetById(Guid absenceId);
        Task<Response<string>> Delete(Guid absenceId);
    }
}
