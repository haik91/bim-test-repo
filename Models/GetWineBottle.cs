namespace WinemakerAPI.Models
{
    public class GetWineBottle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Size { get; set; }
        public int CountInWineCellar { get; set; }
        public string Style { get; set; }
        public string Taste { get; set; }
        public string Description { get; set; }
        public string FoodPairing { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public int WineMakerId { get; set; }
        public string WineMakerName { get; set; }
    }
}
