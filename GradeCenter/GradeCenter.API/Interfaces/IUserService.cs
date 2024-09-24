namespace GradeCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<Response<Guid>> AddUser(AddUserRequest user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<Response<string>> Edit(UserDto userDto, StringValues authHeader);
        Task<IEnumerable<UserDto>> GetUsersWithoutRoles();
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(Guid id);
        Task<Response<bool>> RemoveFromRole(Guid userId);
    }
}
