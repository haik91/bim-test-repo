using WinemakerAPI.Entities;

namespace WinemakerAPI.Models
{
    public class GetWineMaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<GetWineBottle> WineBottles { get; set; }
    }
}
