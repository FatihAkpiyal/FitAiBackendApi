using FitAIAPI.Domain.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Domain.Entities
{
    public class FoodEntry : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int FoodId { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }

        public Food Food { get; set; }
        public User User { get; set; }

    }
}
