namespace WinemakerAPI.Entities
{
    /// <summary>
    /// Represents a wine maker entity.
    /// </summary>
    public class WineMaker
    {
        /// <summary>
        /// Thehe unique identifier for the wine maker.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the wine maker.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the wine maker.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The collection of wine bottles associated with the wine maker.
        /// </summary>
        public ICollection<WineBottle> WineBottles { get; set; }
    }
}
