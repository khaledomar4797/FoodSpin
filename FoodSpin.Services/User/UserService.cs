using FoodSpin.Data;
using FoodSpin.Models.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSpin.Services.User
{
    public class UserService : IUserService
    {
        public async Task<IEnumerable<UserListItem>> GetUsersAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    await ctx
                        .Users
                        .Select(
                            user =>
                                new UserListItem
                                {
                                    UserName = user.UserName,
                                    Email = user.Email,
                                    PhoneNumber = user.PhoneNumber
                                }
                        ).ToListAsync();

                return query;
            }
        }

        public async Task<UserDetail> GetUserByIdAsync(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Users
                        .Where(user => user.UserName.Substring(0, user.UserName.IndexOf("@")) == id)
                        .FirstOrDefaultAsync();
                return
                    new UserDetail
                    {
                        UserName = entity.UserName,
                        Email = entity.Email,
                        PhoneNumber = entity.PhoneNumber
                    };
            }
        }

        public async Task<bool> UpdateUserAsync(UserEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Users
                        .Where(user => user.UserName == model.UserName)
                        .FirstOrDefaultAsync();

                entity.UserName = model.UserName;
                entity.Email = model.Email;
                entity.PhoneNumber = model.PhoneNumber;

                return await ctx.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    await ctx
                        .Users
                        .Where(user => user.UserName.Substring(0, user.UserName.IndexOf("@")) == id)
                        .FirstOrDefaultAsync();

                ctx.Users.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}
