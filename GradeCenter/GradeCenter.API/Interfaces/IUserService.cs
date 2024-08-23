namespace GradeCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<Response<string>> AddUser(AddUserRequest user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<Response<string>> Edit(string userId, UserDto userDto, StringValues authHeader);
        Task<IEnumerable<UserDto>> GetUsersWithoutRoles();
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(string id);
    }
}
