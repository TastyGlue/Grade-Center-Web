namespace GradeCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<string> AddUser(AddUserRequest user);
        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
