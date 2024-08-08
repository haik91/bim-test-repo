using System.ComponentModel.DataAnnotations;

namespace WinemakerAPI.Models
{
    public class UpdateWineBottle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(1900, 2024)]
        public int Year { get; set; }

        [Range(50, 2000)]
        public int Size { get; set; } // Size in ml

        [Range(0, 3000)]
        public int CountInWineCellar { get; set; }

        [StringLength(50)]
        public string Style { get; set; }

        [StringLength(500)]
        public string Taste { get; set; } // Text description of taste

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string FoodPairing { get; set; }

        [Url]
        public string Link { get; set; }

        [Url]
        public string Image { get; set; } // URL to image

        [Required]
        public int WineMakerId { get; set; }
    }
}
