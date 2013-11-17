using SaveMySpot.Models;
using System; 
using System.Data.Entity;
using System.Linq;
  
namespace SaveMySpot.Data
{
    public class ApplicationContext : DbContext
    { 
        public ApplicationContext()
            : base("SaveMySpotDatabase")
        {

        }
          
        public IDbSet<Spot> Spots { get; set; }
    } 
}
