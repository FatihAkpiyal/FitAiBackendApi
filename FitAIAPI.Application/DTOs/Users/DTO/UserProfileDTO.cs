namespace FitAIAPI.Application.DTOs
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string? Gender { get; set; } // Cinsiyet
        public double Height { get; set; } // Boy
        public double CurrentWeight { get; set; } // Mevcut Kilo
        public double TargetWeight { get; set; } // Hedef Kilo
        public DateTime DateOfBirth { get; set; } // Doğum Günü

        public string? Goals { get; set; } // Hedefler (Kilo kaybı, kilo alma, kas yapma vb.)
        public string? PreferredActivities { get; set; } // Tercih edilen spor aktiviteleri (JSON)
        public string? WorkoutFrequency { get; set; } // Spor yapma sıklığı
        public string? FocusAreas { get; set; } // Odaklanılan bölgeler (JSON)

        public double? FirstWeight { get; set; }
        public int? BasalMetabolism { get; set; }
        public int? DailyKcalGoal { get; set; }

       // public bool IsDeleted { get; set; }



    }
}
