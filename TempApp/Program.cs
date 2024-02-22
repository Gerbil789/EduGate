using Database.Models;
using Database.Repositories;
using Database;

// this app is for testing purposes only !!!
class Program
{
    static void Main()
    {
        var schoolRepository = new SchoolRepository();

        try
        {
            //using(var db = new SchoolDbContext())
            //{
            //    db.Database.EnsureCreated();
            //}

            var school = new School
            {
                Name = "School of Might & Magic",
                Address = "1223 Main"
            };

            schoolRepository.AddSchool(school);

            var schools = schoolRepository.GetAllSchools();

            Console.WriteLine("All schools in the database:");
            foreach (var s in schools)
            {
                Console.WriteLine(s.Name);
            }


        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
}