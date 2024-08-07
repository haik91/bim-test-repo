namespace WinemakerAPI.Models
{
    public class PostWineBottle
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Size { get; set; } // Size in ml
        public int CountInWineCellar { get; set; }
        public string Style { get; set; }
        public string Taste { get; set; } // Text description of taste
        public string Description { get; set; }
        public string FoodPairing { get; set; }
        public string Link { get; set; }
        public string Image { get; set; } // URL to image
        public int WineMakerId { get; set; }
    }
}
