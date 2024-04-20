using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class DatabaseRepository
    {
        private readonly EduGateDbContext dbContext;
        public DatabaseRepository()
        {
            dbContext = new EduGateDbContext();
        }

        //--------------------Students--------------------
        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await dbContext.Students.Include(x => x.Address).Include(x => x.Phone).Include(x => x.Application1).Include(x => x.Application3).Include(x => x.Application1).ToListAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                await dbContext.Students.AddAsync(student);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> UpdateStudent(Student modifiedStudent)
        {
            try
            {
                var student = await dbContext.Students.Include(x => x.Address).Include(x => x.Phone).FirstOrDefaultAsync(x => x.StudentId == modifiedStudent.StudentId);
                if (student == null)
                {
                    throw new Exception($"Student {modifiedStudent} not found in database");
                }

                student.FirstName = modifiedStudent.FirstName;
                student.LastName = modifiedStudent.LastName;
                student.BirthDate = modifiedStudent.BirthDate;
                student.Address = modifiedStudent.Address;
                student.Address.Number = modifiedStudent.Address.Number;
                student.Address.Street = modifiedStudent.Address.Street;
                student.Address.City = modifiedStudent.Address.City;
                student.Address.State = modifiedStudent.Address.State;
                student.Address.ZipCode = modifiedStudent.Address.ZipCode;
                student.Email = modifiedStudent.Email;
                student.Phone = modifiedStudent.Phone;
                student.Phone.Code = modifiedStudent.Phone.Code;
                student.Phone.Number = modifiedStudent.Phone.Number;
                student.Application1 = modifiedStudent.Application1;
                student.Application1.StudyProgram = modifiedStudent.Application1.StudyProgram;
                student.Application2 = modifiedStudent.Application2;
                student.Application2.StudyProgram = modifiedStudent.Application2.StudyProgram;
                student.Application3 = modifiedStudent.Application3;
                student.Application3.StudyProgram = modifiedStudent.Application3.StudyProgram;

                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteStudent(Student student)
        {
            try
            {
                dbContext.Students.Remove(student);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //--------------------Schools--------------------
        public async Task<IEnumerable<School>> GetSchools()
        {
            try
            {
                return await dbContext.Schools.Include(x => x.Address).Include(x => x.StudyPrograms).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> AddSchool(School school)
        {
            try
            {
                await dbContext.Schools.AddAsync(school);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
         
          
        }
        public async Task<School> UpdateSchool(School school)
        {
            try
            {
                var existingSchool = await dbContext.Schools.Include(x => x.Address).Include(x => x.StudyPrograms).FirstOrDefaultAsync(x => x.SchoolId == school.SchoolId);
                if (existingSchool == null)
                {
                    throw new Exception($"School {school.Name} not found in database");
                }

                existingSchool.Name = school.Name;
                existingSchool.Address = school.Address;
                existingSchool.Address.Number = school.Address.Number;
                existingSchool.Address.Street = school.Address.Street;
                existingSchool.Address.City = school.Address.City;
                existingSchool.Address.State = school.Address.State;
                existingSchool.Address.ZipCode = school.Address.ZipCode;

                foreach (var updatedStudyProgram in school.StudyPrograms)
                {
                    var existingStudyProgram = existingSchool.StudyPrograms.FirstOrDefault(sp => sp.StudyProgramId == updatedStudyProgram.StudyProgramId);

                    if (existingStudyProgram != null)
                    {
                        // Update existing study program properties
                        existingStudyProgram.Name = updatedStudyProgram.Name;
                        existingStudyProgram.Description = updatedStudyProgram.Description;
                        existingStudyProgram.Identifier = updatedStudyProgram.Identifier;
                        existingStudyProgram.AvailableSeats = updatedStudyProgram.AvailableSeats;
                    }
                    else
                    {
                        // Add the new study program to the existing school
                        existingSchool.StudyPrograms.Add(updatedStudyProgram);
                    }
                }

                foreach (var existingStudyProgram in existingSchool.StudyPrograms.ToList())
                {
                    if (!school.StudyPrograms.Any(sp => sp.StudyProgramId == existingStudyProgram.StudyProgramId))
                    {
                        existingSchool.StudyPrograms.Remove(existingStudyProgram);
                    }
                }


                await dbContext.SaveChangesAsync();
                return school;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteSchool(School school)
        {
            try
            {
                dbContext.Schools.Remove(school);
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

    }
}
