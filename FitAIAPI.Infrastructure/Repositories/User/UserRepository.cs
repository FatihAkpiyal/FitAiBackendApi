using FitAIAPI.Domain.Entities;
using FitAIAPI.Domain.Repositories;
using FitAIAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text.Json;

namespace FitAIAPI.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        private readonly FitAIDbContext _context;

        public UserRepository(FitAIDbContext context):base(context)
        {
            _dbSet = context.Set<User>();
            _context = context;
        }

        public async Task<User> UserCheckAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return user;
            }
            else
                throw new UserNotFoundException(email);
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            var result = await _dbSet.Include(u => u.UserDetail)
                .Include(u => u.UserWorkoutPlans)
                .FirstOrDefaultAsync(u=>u.Id == id);
            if (result == null)
            {
                throw new ArgumentNullException(typeof(User).ToString());
            }

            return result;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task EditUserRoleToAdminAsync(int id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                user.Role = "Admin";
                await UpdateAsync(user);
            }
            else
            {
                throw new UserNotFoundException(id.ToString());
            }
        }


        public async Task<bool> UserExistenceCheckAsync(string email)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return true;
            }
            else
                return false;
        }

        
    }
}
