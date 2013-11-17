
using System.ComponentModel.DataAnnotations;
namespace SaveMySpot.Models
{
    public class Spot
    {
        public int SpotId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public string AuthCode { get; set; } 
    }
}
