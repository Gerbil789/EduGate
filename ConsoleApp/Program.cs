using NewDatabase.Models;
using NewDatabase;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
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

            orm.Insert(address1);
            orm.Insert(address2);

            School school = new()
            {
                Name = "Oslo School",
                AddressId = address1.AddressId
            };

            orm.Insert(school);


            StudyProgram studyProgram = new()
            {
                Name = "Computer Science",
                AvailableSeats = 10,
                SchoolId = school.SchoolId
            };

            orm.Insert(studyProgram);

            Student student = new()
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new System.DateTime(1990, 1, 1),
                Email = "Bruhman@gmail.com",
                AddressId = address2.AddressId,
                Password = "password",
                Number = "123456789",
                Code = "+420"
            };

            orm.Insert(student);

            var students = orm.Select<Student>();

            Application application = new()
            {
                StudentId = student.StudentId,
                StudyProgramId = studyProgram.StudyProgramId,
                Date = DateTime.Now
            };

            orm.Insert(application);



            int applicationId = application.ApplicationId;


            var app = orm.Select<Application>().FirstOrDefault(a => a.ApplicationId == applicationId); 
            var pro = orm.Select<StudyProgram>().FirstOrDefault(s => s.StudyProgramId == app.StudyProgramId);
            var stud = orm.Select<Student>().FirstOrDefault(s => s.StudentId == app.StudentId);
            var sch = orm.Select<School>().FirstOrDefault(s => s.SchoolId == pro.SchoolId);

          
        }
    }
}