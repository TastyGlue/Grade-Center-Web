namespace GradeCenter.API.Interfaces
{
    public interface IAbsenceService
    {
        Task<CustomResult<Guid>> Add(AddAbsenceRequest request);
        Task<CustomResult<string>> Edit(AbsenceDto absenceDto);
        Task<IEnumerable<AbsenceDto>> GetAll(Guid? studentId);
        Task<AbsenceDto?> GetById(Guid absenceId);
        Task<CustomResult<string>> Delete(Guid absenceId);
    }
}
