using WebApplicationMVC.DatabaseModels;
using NewDatabase.Models;

namespace WebApplicationMVC
{
    public class Database
    {
        private readonly ReflectionORM orm = new();
        public Database(){}

        public async Task<DatabaseModels.Student> Login(string email, string password)
        {
            var db_student = (await orm.Select<NewDatabase.Models.Student>()).FirstOrDefault(x => x.Email == email && x.Password == password);
            if (db_student == null)
            {
                throw new Exception("Invalid email or password");
            }

            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_student.AddressId);
            var db_applications = (await orm.Select<NewDatabase.Models.Application>()).Where(x => x.StudentId == db_student.StudentId).ToList();

            var db_studyPrograms = (await orm.Select<NewDatabase.Models.StudyProgram>()).ToList();

         


            DatabaseModels.Address address = new()
            {
                AddressId = db_address.AddressId,
                City = db_address.City,
                Street = db_address.Street,
                Number = db_address.Number,
                ZipCode = db_address.ZipCode,
                State = db_address.State
            };


            DatabaseModels.Student student = new()
            {
                StudentId = db_student.StudentId,
                FirstName = db_student.FirstName,
                LastName = db_student.LastName,
                BirthDate = db_student.BirthDate,
                Address = address,
                Email = db_student.Email,
                Code = db_student.Code,
                Number = db_student.Number,
                Password = db_student.Password
            };

            List<DatabaseModels.Application> applications = new();
            foreach (var db_application in db_applications)
            {
                if(db_application.StudyProgramId != 0)
                {
                    var db_studyProgram = db_studyPrograms.FirstOrDefault(x => x.StudyProgramId == db_application.StudyProgramId);
                    var db_school = (await orm.Select<NewDatabase.Models.School>()).FirstOrDefault(x => x.SchoolId == db_studyProgram.SchoolId);
                    var db_schoolAddress = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_school.AddressId);

                    DatabaseModels.School school = new()
                    {
                        SchoolId = db_school.SchoolId,
                        Name = db_school.Name,
                        Address = new()
                        {
                            AddressId = db_schoolAddress.AddressId,
                            City = db_schoolAddress.City,
                            Street = db_schoolAddress.Street,
                            Number = db_schoolAddress.Number,
                            ZipCode = db_schoolAddress.ZipCode,
                            State = db_schoolAddress.State
                        }
                    };

                    DatabaseModels.StudyProgram studyProgram = new()
                    {
                        StudyProgramId = db_studyProgram.StudyProgramId,
                        Name = db_studyProgram.Name,
                        AvailableSeats = db_studyProgram.AvailableSeats,
                        School = school
                    };

 
                    applications.Add(new()
                    {
                        ApplicationId = db_application.ApplicationId,
                        Student = student,
                        Date = db_application.Date,
                        StudyProgram = studyProgram
                    });
                }
                else
                {
                    applications.Add(new()
                    {
                        ApplicationId = db_application.ApplicationId,
                        Student = student,
                        Date = db_application.Date,
                        StudyProgram = null
                    });
                }

            }

            student.Applications = applications;
            return student;
        }


        public async Task<DatabaseModels.Student> Register(DatabaseModels.Student student)
        {
            var db_address = new NewDatabase.Models.Address();
            await orm.Insert(db_address);

            var db_student = new NewDatabase.Models.Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                AddressId = db_address.AddressId,
                Email = student.Email,
                Code = student.Code,
                Number = student.Number,
                Password = student.Password
            };

            await orm.Insert(db_student);

            foreach (var application in student.Applications)
            {
                var db_application = new NewDatabase.Models.Application()
                {
                    StudentId = db_student.StudentId,
                    StudyProgramId = application.StudyProgram?.StudyProgramId ?? 0, 
                    Date = application.Date
                };

                await orm.Insert(db_application);
            }

            student.StudentId = db_student.StudentId;
            return student;
        }

        public async Task<List<DatabaseModels.School>> GetSchools()
        {
            var db_schools = await orm.Select<NewDatabase.Models.School>();
            var db_addresses = await orm.Select<NewDatabase.Models.Address>();
            var db_studyPrograms = await orm.Select<NewDatabase.Models.StudyProgram>();

            List<DatabaseModels.School> schools = new();
            foreach (var db_school in db_schools)
            {
                var db_address = db_addresses.FirstOrDefault(x => x.AddressId == db_school.AddressId);
                DatabaseModels.Address address = new()
                {
                    AddressId = db_address.AddressId,
                    City = db_address.City,
                    Street = db_address.Street,
                    Number = db_address.Number,
                    ZipCode = db_address.ZipCode,
                    State = db_address.State
                };

                DatabaseModels.School school = new()
                {
                    SchoolId = db_school.SchoolId,
                    Name = db_school.Name,
                    Address = address
                };


                List<DatabaseModels.StudyProgram> studyPrograms = new();
                foreach (var db_studyProgram in db_studyPrograms.Where(x => x.SchoolId == db_school.SchoolId))
                {
                    studyPrograms.Add(new()
                    {
                        StudyProgramId = db_studyProgram.StudyProgramId,
                        Name = db_studyProgram.Name,
                        AvailableSeats = db_studyProgram.AvailableSeats,
                        School = school
                    });
                }
                school.StudyPrograms = studyPrograms;

                schools.Add(school);
            }

            return schools;
        }

        public async Task UpdateStudent(WebApplicationMVC.DatabaseModels.Student student)
        {
            var db_student = (await orm.Select<NewDatabase.Models.Student>()).FirstOrDefault(x => x.StudentId == student.StudentId);
            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_student.AddressId);
            var db_applications = (await orm.Select<NewDatabase.Models.Application>()).Where(x => x.StudentId == student.StudentId).ToList();

            for (int i = 0; i < 3; i++)
            {
                var application = student.Applications[i];
                var db_application = db_applications[i];

                if (db_application == null)
                {
                    throw new Exception("Application not found for student with ID " + student.StudentId);
                }

                if (application.StudyProgram != null)
                {
                    db_application.StudyProgramId = application.StudyProgram.StudyProgramId;
                    await orm.Update(db_application);
                }
            }

            db_student.FirstName = student.FirstName;
            db_student.LastName = student.LastName;
            db_student.BirthDate = student.BirthDate;
            db_student.Email = student.Email;
            db_student.Code = student.Code;
            db_student.Number = student.Number;

            db_address.Street = student.Address.Street;
            db_address.Number = student.Address.Number;
            db_address.City = student.Address.City;
            db_address.State = student.Address.State;
            db_address.ZipCode = student.Address.ZipCode;

            await orm.Update(db_student);
            await orm.Update(db_address);
        }

        public async Task<DatabaseModels.Student> GetStudent(int id)
        {
            var db_student = (await orm.Select<NewDatabase.Models.Student>()).FirstOrDefault(x => x.StudentId == id);

            if (db_student == null)
            {
                throw new Exception("Student not found");
            }

            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_student.AddressId);

            var db_applications = (await orm.Select<NewDatabase.Models.Application>()).Where(x => x.StudentId == db_student.StudentId).ToList();
            var db_studyPrograms = (await orm.Select<NewDatabase.Models.StudyProgram>()).ToList();

            DatabaseModels.Address address = new()
            {
                AddressId = db_address.AddressId,
                City = db_address.City,
                Street = db_address.Street,
                Number = db_address.Number,
                ZipCode = db_address.ZipCode,
                State = db_address.State
            };

            DatabaseModels.Student student = new()
            {
                StudentId = db_student.StudentId,
                FirstName = db_student.FirstName,
                LastName = db_student.LastName,
                BirthDate = db_student.BirthDate,
                Address = address,
                Email = db_student.Email,
                Code = db_student.Code,
                Number = db_student.Number,
                Password = db_student.Password
            };

            List<DatabaseModels.Application> applications = new();

            foreach (var db_application in db_applications)
            {
                if(db_application.StudyProgramId == 0)
                {
                    applications.Add(new()
                    {
                        ApplicationId = db_application.ApplicationId,
                        Student = student,
                        Date = db_application.Date,
                        StudyProgram = null
                    });
                    continue;
                }

                var db_studyProgram = db_studyPrograms.FirstOrDefault(x => x.StudyProgramId == db_application.StudyProgramId);
                var db_school = (await orm.Select<NewDatabase.Models.School>()).FirstOrDefault(x => x.SchoolId == db_studyProgram.SchoolId);
                var db_schoolAddress = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_school.AddressId);

                DatabaseModels.School school = new()
                {
                    SchoolId = db_school.SchoolId,
                    Name = db_school.Name,
                    Address = new()
                    {
                        AddressId = db_schoolAddress.AddressId,
                        City = db_schoolAddress.City,
                        Street = db_schoolAddress.Street,
                        Number = db_schoolAddress.Number,
                        ZipCode = db_schoolAddress.ZipCode,
                        State = db_schoolAddress.State
                    }
                };

                DatabaseModels.StudyProgram studyProgram = new()
                {
                    StudyProgramId = db_studyProgram.StudyProgramId,
                    Name = db_studyProgram.Name,
                    AvailableSeats = db_studyProgram.AvailableSeats,
                    School = school
                };
            }

            student.Applications = applications;

            return student;
        }

    }
}
