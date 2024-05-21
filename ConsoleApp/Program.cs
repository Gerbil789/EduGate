using NewDatabase.Models;
using NewDatabase;

namespace ConsoleApp
{
    class Program
    {

        static async Task Main(string[] args)
        {
            ReflectionORM orm = new();


            orm.CreateDatabase();
            
            Address address1 = new()
            {
                City = "Oslo",
                Street = "Karl Johans gate",
                Number = "1",
                ZipCode = "0159",
                State = "Oslo"
            };

            Address address2 = new()
            {
                City = "Bruh",
                Street = "Karl Johans gate",
                Number = "2",
                ZipCode = "0159",
                State = "Bruh"
            };

            await orm.Insert(address1);
            await orm.Insert(address2);

            School school1 = new()
            {
                Name = "Oslo School",
                AddressId = address1.AddressId
            };

            School school2 = new()
            {
                Name = "Bruh School",
                AddressId = address2.AddressId
            };

            await orm.Insert(school1);
            await orm.Insert(school2);


            StudyProgram studyProgram1 = new()
            {
                Name = "Computer Science",
                AvailableSeats = 10,
                SchoolId = school1.SchoolId
            };

            StudyProgram studyProgram2 = new()
            {
                Name = "Electronics",
                AvailableSeats = 20,
                SchoolId = school1.SchoolId
            };

            StudyProgram studyProgram3 = new()
            {
                Name = "Bruhing",
                AvailableSeats = 7,
                SchoolId = school2.SchoolId
            };



            await orm.Insert(studyProgram1);
            await orm.Insert(studyProgram2);
            await orm.Insert(studyProgram3);



            Student student = new()
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new System.DateTime(1990, 1, 1),
                Email = "Bruhman@gmail.com",
                AddressId = address2.AddressId,
                Password = "password",
                Code = "+420",
                Number = "123456789"
            };

            await orm.Insert(student);

           

            //Application application = new()
            //{
            //    StudentId = student.StudentId,
            //    StudyProgramId = studyProgram.StudyProgramId,
            //    Date = DateTime.Now
            //};

            //await orm.Insert(application);



            //int applicationId = application.ApplicationId;


            //var app = (await orm.Select<Application>()).FirstOrDefault(a => a.ApplicationId == applicationId); 
            //var pro = (await orm.Select<StudyProgram>()).FirstOrDefault(s => s.StudyProgramId == app.StudyProgramId);
            //var stud = (await orm.Select<Student>()).FirstOrDefault(s => s.StudentId == app.StudentId);
            //var sch = (await orm.Select<School>()).FirstOrDefault(s => s.SchoolId == pro.SchoolId);

            //Console.WriteLine($"Student: {stud.FirstName} {stud.LastName}");
            //Console.WriteLine($"School: {sch.Name}");
            //Console.WriteLine($"Study Program: {pro.Name}");
            //Console.WriteLine($"Application Date: {app.Date}");

          
        }
    }
}