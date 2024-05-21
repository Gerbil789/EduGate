using System.Collections.ObjectModel;

namespace DesktopApp
{
    public class Database
    {
        private readonly ReflectionORM orm;
        public Database()
        {
            orm = new ReflectionORM();
        }

        public async Task AddSchool(DesktopApp.Models.School school)
        {
            NewDatabase.Models.Address db_address = new()
            {
                Street = school.Address.Street,
                Number = school.Address.Number,
                City = school.Address.City,
                State = school.Address.State,
                ZipCode = school.Address.ZipCode
            };

            await orm.Insert(db_address);




            NewDatabase.Models.School db_school = new()
            {
                Name = school.Name,
                AddressId = db_address.AddressId
            };

            await orm.Insert(db_school);
            school.SchoolId = db_school.SchoolId;


            foreach (var studyProgram in school.StudyPrograms)
            {
                NewDatabase.Models.StudyProgram db_studyProgram = new()
                {
                    Name = studyProgram.Name,
                    AvailableSeats = studyProgram.AvailableSeats,
                    SchoolId = db_school.SchoolId
                };

                await orm.Insert(db_studyProgram);
            }

        }

        public async Task UpdateSchool(DesktopApp.Models.School school)
        {
            var db_school = (await orm.Select<NewDatabase.Models.School>()).FirstOrDefault(x => x.SchoolId == school.SchoolId);
            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_school.AddressId);
            var db_programs = (await orm.Select<NewDatabase.Models.StudyProgram>()).Where(x => x.SchoolId == school.SchoolId).ToList();

            db_school.Name = school.Name;
            db_address.Street = school.Address.Street;
            db_address.Number = school.Address.Number;
            db_address.City = school.Address.City;
            db_address.State = school.Address.State;
            db_address.ZipCode = school.Address.ZipCode;

            //delete programs that are not in the list
            foreach (var db_program in db_programs)
            {
                if (!school.StudyPrograms.Any(x => x.StudyProgramId == db_program.StudyProgramId))
                {
                    await orm.Delete(db_program);
                }
            }

            //add or update programs
            foreach (var studyProgram in school.StudyPrograms)
            {
                var db_program = db_programs.FirstOrDefault(x => x.StudyProgramId == studyProgram.StudyProgramId);

                if (db_program == null)
                {
                    NewDatabase.Models.StudyProgram db_studyProgram = new()
                    {
                        Name = studyProgram.Name,
                        AvailableSeats = studyProgram.AvailableSeats,
                        SchoolId = school.SchoolId
                    };

                    await orm.Insert(db_studyProgram);
                }
                else
                {
                    db_program.Name = studyProgram.Name;
                    db_program.AvailableSeats = studyProgram.AvailableSeats;
                    await orm.Update(db_program);
                }
            }

            await orm.Update(db_school);
            await orm.Update(db_address);
            
        }

        public async Task DeleteSchool(DesktopApp.Models.School school)
        {
            var db_school = (await orm.Select<NewDatabase.Models.School>()).FirstOrDefault(x => x.SchoolId == school.SchoolId);
            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_school.AddressId);
            var db_programs = (await orm.Select<NewDatabase.Models.StudyProgram>()).Where(x => x.SchoolId == school.SchoolId).ToList();

            foreach (var db_program in db_programs)
            {
                await orm.Delete(db_program);
            }

            await orm.Delete(db_address);
            await orm.Delete(db_school);
        }

        public async Task<List<DesktopApp.Models.School>> GetSchools()
        {
            var db_schools = await orm.Select<NewDatabase.Models.School>();
            var db_addresses = await orm.Select<NewDatabase.Models.Address>();
            var db_programs = await orm.Select<NewDatabase.Models.StudyProgram>();


            List<DesktopApp.Models.School> schools = new();

            foreach (var db_school in db_schools)
            {
                var db_address = db_addresses.FirstOrDefault(x => x.AddressId == db_school.AddressId);

                if(db_address == null)
                {
                    throw new Exception("Address not found for school with ID " + db_school.SchoolId);
                }

                DesktopApp.Models.Address address = new()
                {
                    AddressId = db_address.AddressId,
                    Street = db_address.Street,
                    Number = db_address.Number,
                    City = db_address.City,
                    State = db_address.State,
                    ZipCode = db_address.ZipCode
                };


                ObservableCollection<DesktopApp.Models.StudyProgram> programs = new();

                foreach (var db_program in db_programs.Where(x => x.SchoolId == db_school.SchoolId))
                {
                    DesktopApp.Models.StudyProgram program = new()
                    {
                        StudyProgramId = db_program.StudyProgramId,
                        Name = db_program.Name,
                        AvailableSeats = db_program.AvailableSeats,
                        Identifier = $"{db_program.Name.Substring(0, 3).ToUpper()}-{db_program.AvailableSeats}"
                };

                    programs.Add(program);
                }


                DesktopApp.Models.School school = new()
                {
                    SchoolId = db_school.SchoolId,
                    Name = db_school.Name,
                    Address = address,
                    StudyPrograms = programs
                };

                schools.Add(school);
            }

            return schools;
        }

        public async Task AddStudent(DesktopApp.Models.Student student)
        {
            NewDatabase.Models.Address db_address = new()
            {
                Street = student.Address.Street,
                Number = student.Address.Number,
                City = student.Address.City,
                State = student.Address.State,
                ZipCode = student.Address.ZipCode
            };

            await orm.Insert(db_address);




            NewDatabase.Models.Student db_student = new()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Email = student.Email,
                AddressId = db_address.AddressId
            };

            await orm.Insert(db_student);
            student.StudentId = db_student.StudentId;


            foreach (var application in student.Applications)
            {
                NewDatabase.Models.Application db_application = new()
                {
                    StudentId = db_student.StudentId,
                    StudyProgramId = application.StudyProgram?.StudyProgramId ?? 0
                };

                await orm.Insert(db_application);
            }

        }
        public async Task UpdateStudent(DesktopApp.Models.Student student)
        {
            var db_student = (await orm.Select<NewDatabase.Models.Student>()).FirstOrDefault(x => x.StudentId == student.StudentId);
            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_student.AddressId);
            var db_applications = (await orm.Select<NewDatabase.Models.Application>()).Where(x => x.StudentId == student.StudentId).ToList();

            for(int i = 0; i < 3; i++)
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

        public async Task DeleteStudent(DesktopApp.Models.Student student)
        {
            var db_student = (await orm.Select<NewDatabase.Models.Student>()).FirstOrDefault(x => x.StudentId == student.StudentId);
            var db_address = (await orm.Select<NewDatabase.Models.Address>()).FirstOrDefault(x => x.AddressId == db_student.AddressId);
            var db_applications = (await orm.Select<NewDatabase.Models.Application>()).Where(x => x.StudentId == student.StudentId).ToList();

            foreach (var db_application in db_applications)
            {
                await orm.Delete(db_application);
            }

            await orm.Delete(db_address);
            await orm.Delete(db_student);
        }


        public async Task<List<DesktopApp.Models.Student>> GetStudents()
        {
            var db_students = await orm.Select<NewDatabase.Models.Student>();
            var db_addresses = await orm.Select<NewDatabase.Models.Address>();
            var db_applications = await orm.Select<NewDatabase.Models.Application>();
            var db_studyPrograms = await orm.Select<NewDatabase.Models.StudyProgram>();

            List<DesktopApp.Models.Student> students = new();

            foreach (var db_student in db_students)
            {
                var db_address = db_addresses.FirstOrDefault(x => x.AddressId == db_student.AddressId);

                if (db_address == null)
                {
                    throw new Exception("Address not found for student with ID " + db_student.StudentId);
                }

                DesktopApp.Models.Address address = new()
                {
                    AddressId = db_address.AddressId,
                    Street = db_address.Street,
                    Number = db_address.Number,
                    City = db_address.City,
                    State = db_address.State,
                    ZipCode = db_address.ZipCode
                };

                var student_db_applications = db_applications.Where(x => x.StudentId == db_student.StudentId).ToList();
                var applications = new List<DesktopApp.Models.Application>();

                foreach (var db_application in student_db_applications)
                {
                    var db_program = db_studyPrograms.FirstOrDefault(x => x.StudyProgramId == db_application.StudyProgramId);
                    var program = db_program == null ? null : new DesktopApp.Models.StudyProgram
                    {
                        StudyProgramId = db_program.StudyProgramId,
                        Name = db_program.Name,
                        AvailableSeats = db_program.AvailableSeats
                    };

                    DesktopApp.Models.Application application = new()
                    {
                        ApplicationId = db_application.ApplicationId,
                        StudyProgram = program
                    };

                    applications.Add(application);
                }




                DesktopApp.Models.Student student = new()
                {
                    StudentId = db_student.StudentId,
                    FirstName = db_student.FirstName,
                    LastName = db_student.LastName,
                    BirthDate = db_student.BirthDate,
                    Email = db_student.Email,
                    Address = address,
                    Code = db_student.Code,
                    Number = db_student.Number,
                    Applications = applications
                };

                students.Add(student);
            }

            return students;
        }

        public async Task<int> GetSchoolIdByStudyProgram(int studyProgramId)
        {
            var db_program = (await orm.Select<NewDatabase.Models.StudyProgram>()).FirstOrDefault(x => x.StudyProgramId == studyProgramId);
            var db_school = (await orm.Select<NewDatabase.Models.School>()).FirstOrDefault(x => x.SchoolId == db_program.SchoolId);

            return db_school.SchoolId;
       
        }
    }
}
