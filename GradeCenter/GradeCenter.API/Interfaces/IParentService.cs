namespace GradeCenter.API.Interfaces
{
    public interface IParentService
    {
        Task<CustomResult<Guid>> AddParent(AddParentRequest request);
        Task<CustomResult<string>> Edit(ParentDto parentDto);
        Task<IEnumerable<ParentDto>> GetAll();
        Task<ParentDto?> GetById(Guid id);
    }
}
