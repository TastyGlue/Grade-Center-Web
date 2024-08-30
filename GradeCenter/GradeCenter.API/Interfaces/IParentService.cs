namespace GradeCenter.API.Interfaces
{
    public interface IParentService
    {
        Task<Response<int>> AddParent(AddParentRequest request);
        Task<Response<string>> Edit(int parentId, ParentDto parentDto);
        Task<IEnumerable<ParentDto>> GetAll();
        Task<ParentDto?> GetById(int id);
    }
}
