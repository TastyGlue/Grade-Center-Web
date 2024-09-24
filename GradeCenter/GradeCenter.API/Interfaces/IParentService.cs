namespace GradeCenter.API.Interfaces
{
    public interface IParentService
    {
        Task<Response<Guid>> AddParent(AddParentRequest request);
        Task<Response<string>> Edit(ParentDto parentDto);
        Task<IEnumerable<ParentDto>> GetAll();
        Task<ParentDto?> GetById(Guid id);
    }
}
