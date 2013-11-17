using SaveMySpot.Models;
using System; 
using System.Linq;
  
namespace SaveMySpot.Data
{
    public interface IUowData
    { 
        IRepository<Spot> Spots { get; } 

        int SaveChanges();
    }
}
