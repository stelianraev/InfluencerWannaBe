namespace InfluencerWannaBe.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Country
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.CountryNameMaxLenght)]
        public string Name { get; set; }
    }
}
