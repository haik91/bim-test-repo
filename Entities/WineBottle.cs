using WinemakerAPI.Models;

namespace WinemakerAPI.Entities
{
    public class WineBottle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Size { get; set; } // The unit of the size is ml 
        public int CountInWineCellar { get; set; }
        public string Style { get; set; }
        public string Taste { get; set; } // text decribes the taste 
        public string Description { get; set; }
        public string FoodPairing { get; set; }
        public string Link { get; set; }
        public string Image { get; set; } // URL 
        public int WineMakerId { get; set; }
        public WineMaker WineMaker { get; set; }
    }
}
