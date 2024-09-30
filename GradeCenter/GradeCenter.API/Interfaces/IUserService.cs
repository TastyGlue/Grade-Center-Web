namespace GradeCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<CustomResult<Guid>> AddUser(AddUserRequest user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<CustomResult<string>> Edit(UserDto userDto, StringValues authHeader);
        Task<IEnumerable<UserDto>> GetUsersWithoutRoles();
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetById(Guid id);
        Task<CustomResult<bool>> RemoveFromRole(Guid userId);
        Task<CustomResult<string>> AddPendingUser(AddUserRequest user);
        Task<IEnumerable<PendingUserDto>> GetAllPendingUsers();
        Task<PendingUserDto?> GetPendingUser(Guid id);
        Task<CustomResult<string>> EditPendingUser(PendingUserDto dto);
        Task<CustomResult<string>> DeletePendingUser(Guid id);
    }
}
