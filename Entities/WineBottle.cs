namespace WinemakerAPI.Entities
{
    /// <summary>
    /// Represents a wine bottle.
    /// </summary>
    public class WineBottle
    {
        /// <summary>
        /// The unique identifier for the wine bottle.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the wine bottle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The year the wine was produced.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The size of the wine bottle in milliliters (ml).
        /// </summary>
        public int Size { get; set; } 

        /// <summary>
        /// The number of bottles available in the wine cellar.
        /// </summary>
        public int CountInWineCellar { get; set; }

        /// <summary>
        /// The style of the wine.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// A description of the wine's taste.
        /// </summary>
        public string Taste { get; set; }

        /// <summary>
        /// A detailed description of the wine.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Food pairings that go well with the wine.
        /// </summary>
        public string FoodPairing { get; set; }

        /// <summary>
        /// A link to more information about the wine.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// The URL of the wine bottle's image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The unique identifier of the associated wine maker.
        /// </summary>
        public int WineMakerId { get; set; }

        /// <summary>
        /// The wine make of this bottle.
        /// </summary>
        public WineMaker WineMaker { get; set; }
    }
}
