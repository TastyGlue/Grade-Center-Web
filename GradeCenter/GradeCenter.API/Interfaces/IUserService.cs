namespace GradeCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<string> AddUser(AddUserRequest user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<string?> Edit(string userId, UserDto userDto, StringValues authHeader);
        Task<IEnumerable<UserDto>> GetUsersWithoutRoles();
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(string id);
    }
}
