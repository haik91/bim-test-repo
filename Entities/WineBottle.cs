using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the wine bottle.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The year the wine was produced.
        /// </summary>
        [Range(1900, 2024)]
        public int Year { get; set; }

        /// <summary>
        /// The size of the wine bottle in milliliters (ml).
        /// </summary>
        [Range(50, 2000)]
        public int Size { get; set; }

        /// <summary>
        /// The number of bottles available in the wine cellar.
        /// </summary>
        [Range(0, 3000)]
        public int CountInWineCellar { get; set; }

        /// <summary>
        /// The style of the wine.
        /// </summary>
        [StringLength(50)]
        public string Style { get; set; }

        /// <summary>
        /// A description of the wine's taste.
        /// </summary>
        [StringLength(500)]
        public string Taste { get; set; }

        /// <summary>
        /// A detailed description of the wine.
        /// </summary>
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// Food pairings that go well with the wine.
        /// </summary>
        [StringLength(500)]
        public string FoodPairing { get; set; }

        /// <summary>
        /// A link to more information about the wine.
        /// </summary>
        [Url]
        public string Link { get; set; }

        /// <summary>
        /// The URL of the wine bottle's image.
        /// </summary>
        [Url]
        public string Image { get; set; }

        /// <summary>
        /// The unique identifier of the associated wine maker.
        /// </summary>
        [Required]
        public int WineMakerId { get; set; }

        /// <summary>
        /// The wine make of this bottle.
        /// </summary>
        public WineMaker WineMaker { get; set; }
    }
}
