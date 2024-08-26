namespace GradeCenter.API.Interfaces
{
    public interface IHeadmasterService
    {
        Task<Response<int>> AddHeadmaster(AddHeadmasterRequest request);
        Task<Response<string>> Edit(int headmasterId, HeadmasterDto headmasterDto);
        Task<IEnumerable<HeadmasterDto>> GetAll(string? schoolId);
        Task<HeadmasterDto?> GetById(int id);
    }
}
