using FitAIAPI.Domain.Entities;
using FitAIAPI.Domain.Repositories.WorkoutPlan;
using FitAIAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FitAIAPI.Infrastructure.Repositories.WorkoutPlan
{
    public class WorkoutPlanRepository : GenericRepository<UserWorkoutPlan>, IWorkoutPlanRepository
    {
        private readonly DbSet<UserWorkoutPlan> _dbSet;
        private readonly FitAIDbContext _context;

        public WorkoutPlanRepository(FitAIDbContext context) : base(context)
        {
            _dbSet = context.Set<UserWorkoutPlan>();
            _context = context;
        }

        public async Task<UserWorkoutPlan> GetByUserIdAsync(int userId)
        {
            return await _context.UserWorkoutPlans.FirstOrDefaultAsync(wp => wp.UserId == userId);
        }
    }

}

