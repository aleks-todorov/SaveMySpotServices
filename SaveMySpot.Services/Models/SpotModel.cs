using System; 
using System.Linq;
using System.Runtime.Serialization;
 
namespace SaveMySpot.Services.Models
{
    [DataContract]
    public class SpotModel
    { 
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "authCode")]
        public string AuthCode { get; set; }

    }
}