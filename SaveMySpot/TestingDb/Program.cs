using SaveMySpot.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDb
{
    class Program
    {
        static void Main(string[] args)
        { 
            var context = new ApplicationContext();

            Database.SetInitializer<ApplicationContext>(new MigrateDatabaseToLatestVersion<ApplicationContext, SaveMySpot.Data.Migrations.Configuration>());

            var users = context.Users;

            foreach (var user in users)
            {
                Console.WriteLine(user.AuthCode);
            }
             
        }
    }
}
