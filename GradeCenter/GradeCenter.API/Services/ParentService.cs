
namespace GradeCenter.API.Services
{
    public class ParentService : IParentService
    {
        private readonly UserManager<User> _userManager;
        private readonly GradeCenterDbContext _context;

        public ParentService(UserManager<User> userManager, GradeCenterDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response<int>> AddParent(AddParentRequest request)
        {
            // Find user in database
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new() { Succeeded = false, Message = "Couldn't find user" };

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole.Count > 0)
                return new() { Succeeded = false, Message = "User already has a role and can't be assigned to this role" };
            else
            {
                var result = await _userManager.AddToRoleAsync(user, "PARENT");
                if (!result.Succeeded)
                    return new() { Succeeded = false, Message = "There was an error adding user to the \"PARENT\"" };
            }

            Parent newParent = new()
            {
                User = user
            };

            try
            {
                await _context.Parents.AddAsync(newParent);
                await _context.SaveChangesAsync();

                foreach (var studentId in request.StudentIds)
                {
                    await _context.StudentParents.AddAsync(new()
                    {
                        StudentId = studentId,
                        ParentId = newParent.Id
                    });
                }
            }
            catch (Exception ex)
            {
                return new() { Succeeded = false, Message = ex.ToString() };
            }

            return new() { Succeeded = true, ReturnValue = newParent.Id };
        }

        public async Task<Response<string>> Edit(int parentId, ParentDto parentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParentDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ParentDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
