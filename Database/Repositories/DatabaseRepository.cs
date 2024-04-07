using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
                return await dbContext.Students.Include(x => x.Address).Include(x => x.Phone).ToListAsync();
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
                return await dbContext.Schools.Include(x => x.Address).ToListAsync();
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
        public async Task<bool> UpdateSchool(School modifiedSchool)
        {
            try
            {
                var school = await dbContext.Schools.Include(x => x.Address).FirstOrDefaultAsync(x => x.SchoolId == modifiedSchool.SchoolId);
                if (school == null)
                {
                    throw new Exception($"School {modifiedSchool.Name} not found in database");
                }

                school.Name = modifiedSchool.Name;
                school.Address = modifiedSchool.Address;
                school.Address.Number = modifiedSchool.Address.Number;
                school.Address.Street = modifiedSchool.Address.Street;
                school.Address.City = modifiedSchool.Address.City;
                school.Address.State = modifiedSchool.Address.State;
                school.Address.ZipCode = modifiedSchool.Address.ZipCode;

                await dbContext.SaveChangesAsync();
                return true;
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
