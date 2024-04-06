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
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await dbContext.Students.Include(x => x.Address).Include(x => x.Phone).ToListAsync();
        }
        public async Task<bool> AddStudent(Student student)
        {
            await dbContext.Students.AddAsync(student);
            return await dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateStudent(Student modifiedStudent)
        {
            try
            {
                var student = await dbContext.Students.Include(x => x.Address).Include(x => x.Phone).FirstOrDefaultAsync(x => x.StudentId == modifiedStudent.StudentId);
                if (student == null)
                {
                    return false;
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
                return false;
            }
        }
        public async Task<bool> DeleteStudent(Student student)
        {
            dbContext.Students.Remove(student);
            return await dbContext.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<School>> GetSchools()
        {
            return await dbContext.Schools.Include(x => x.Address).ToListAsync();
        }
        public async Task<bool> AddSchool(School school)
        {
            await dbContext.Schools.AddAsync(school);
            return await dbContext.SaveChangesAsync() > 0;
        }
      
    }
}
