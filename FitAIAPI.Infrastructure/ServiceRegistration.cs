using FitAIAPI.Domain.Entities;
using FitAIAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<FitAIDbContext>(options => options.UseSqlServer("Server=localhost;Database=FitAIDatabase;User Id=sa;Password=31hatay31;"));
        }
    }
}
