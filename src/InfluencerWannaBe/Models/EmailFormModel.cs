using System.ComponentModel.DataAnnotations;

namespace InfluencerWannaBe.Models
{
    public class EmailFormModel
    {
        [Required]
        public string SenderEmail { get; set; }
        [Required]
        public string RecepientEmail { get; set; }

        [MaxLength(500)]
        [MinLength(10)]
        [Required]
        public string Body { get; set; }
    }
}
