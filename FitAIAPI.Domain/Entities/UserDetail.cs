using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class UserDetail : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        
        public string? Gender { get; set; } // Cinsiyet
        public double? Height { get; set; } // Boy

        public double? FirstWeight { get; set; } // ilk kilo
        public double? CurrentWeight { get; set; } // Şuanki Kilo
        public double? TargetWeight { get; set; } // Hedef Kilo
        public string? DateOfBirth { get; set; } // Doğum Günü
        public string? Goals { get; set; } // Hedefler (Kilo kaybı, kilo alma, kas yapma vb.)

        public string? PreferredActivities { get; set; } // Tercih edilen spor aktiviteleri (JSON)
        public string? WorkoutFrequency { get; set; } // Spor yapma sıklığı
        public string? FocusAreas { get; set; } // Odaklanılan bölgeler (JSON)
        public string? HealthProblem { get; set; } // Odaklanılan bölgeler (JSON)

        public int? BasalMetabolism { get; set; }
        public int? DailyKcalGoal { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Foreign key for User
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}