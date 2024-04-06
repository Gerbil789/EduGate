using Database.Models;
using Database.Repositories;
class Program
{
    static void Main()
    {
        var schoolRepository = new DatabaseRepository();

        try
        {
            //var student = new Student
            //{
            //    FirstName = "Vojtěch",
            //    LastName = "Rubeš",
            //    BirthDate = new System.DateTime(2001, 11, 1),
            //    Address = new Address
            //    {
            //        Street = "Komenského",
            //        Number = "48",
            //        City = "Lhota u Opavy",
            //        State = "Česká Republika",
            //        ZipCode = "74792"
            //    },
            //    Email = "vojta.rubes.01@gmail.com",
            //    Phone = new("+420", "603 197 038")
            //};

            var student = new Student
            {
                FirstName = "Viktorie",
                LastName = "Nováková",
                BirthDate = new System.DateTime(2002, 12, 1),
                Address = new Address
                {
                    Street = "U Lesa",
                    Number = "69",
                    City = "Lhota u Opavy",
                    State = "Česká Republika",
                    ZipCode = "74792"
                },
                Email = "myemail.01@gmail.com",
                Phone = new("+420", "603 775 444")
            };



            schoolRepository.AddStudent(student);

            Console.WriteLine("All students in the database:");
            var students = schoolRepository.GetStudents();
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }


            //var school = new School
            //{
            //    Name = "School of Might & Magic",
            //    Address = new Address
            //    {
            //        Street = "Technical",
            //        Number = "666",
            //        City = "Ostrava",
            //        State = "Česká Republika",
            //        ZipCode = "12345"
            //    }
            //};

            //schoolRepository.AddSchool(school);

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