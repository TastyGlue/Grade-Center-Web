namespace GradeCenter.API.Interfaces
{
    public interface IHeadmasterService
    {
        Task<CustomResult<Guid>> AddHeadmaster(AddHeadmasterRequest request);
        Task<CustomResult<string>> Edit(HeadmasterDto headmasterDto);
        Task<IEnumerable<HeadmasterDto>> GetAll(Guid? schoolId);
        Task<HeadmasterDto?> GetById(Guid id);
    }
}
