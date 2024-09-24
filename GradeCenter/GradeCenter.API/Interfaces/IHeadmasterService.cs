namespace GradeCenter.API.Interfaces
{
    public interface IHeadmasterService
    {
        Task<Response<Guid>> AddHeadmaster(AddHeadmasterRequest request);
        Task<Response<string>> Edit(HeadmasterDto headmasterDto);
        Task<IEnumerable<HeadmasterDto>> GetAll(Guid? schoolId);
        Task<HeadmasterDto?> GetById(Guid id);
    }
}
