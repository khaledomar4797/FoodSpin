using FoodSpin.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodSpin.Services.User
{
    public interface IUserService
    {
        Task<bool> DeleteUserAsync(string id);
        Task<UserDetail> GetUserByIdAsync(string id);
        Task<IEnumerable<UserListItem>> GetUsersAsync();
        Task<bool> UpdateUserAsync(UserEdit model);
    }
}