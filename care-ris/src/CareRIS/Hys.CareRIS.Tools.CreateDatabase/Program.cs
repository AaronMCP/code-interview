using System;
using System.Data.Entity;
using Hys.Consultation.EntityFramework;

namespace Hys.CareRIS.Tools.CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var contextFactory = new MigrationsContextFactory();
            using (var dbContext = contextFactory.Create())
            {
                try
                {
                    Console.WriteLine("Creating a database...");

                    var db = new DropCreateDatabaseAlways<ConsultationContext>();
                    db.InitializeDatabase(dbContext);
                    Database.SetInitializer(db);
                    new DemoDataHelper().CreateConsultationConfigData();
                    Console.WriteLine("Complete");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something unexpected happened:");
                    Console.WriteLine(ex);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
