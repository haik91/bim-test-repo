namespace WinemakerAPI.Models
{
    public class Winemaker
    {
        public int WinemakerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<WineBottle> WineBottles { get; set; }
    }
}
