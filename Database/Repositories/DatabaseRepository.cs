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

        public IEnumerable<Student> GetStudents()
        {
            return dbContext.Students.Include(x => x.Address).Include(x => x.Phone).ToList();
        }

        public bool AddStudent(Student student)
        {
            dbContext.Students.Add(student);
            return dbContext.SaveChanges() > 0;
        }

        public bool UpdateStudent(Student modifiedStudent)
        {
            try
            {
                var student = dbContext.Students.Include(x => x.Address).Include(x => x.Phone).FirstOrDefault(x => x.StudentId == modifiedStudent.StudentId);
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

                dbContext.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool DeleteStudent(Student student)
        {
            dbContext.Students.Remove(student);
            return dbContext.SaveChanges() > 0;
        }

        public bool AddSchool(School school)
        {
            dbContext.Schools.Add(school);
            return dbContext.SaveChanges() > 0;
        }

        public IEnumerable<School> GetAllSchools()
        {
            return dbContext.Schools;
        }

        public void UpdateSchool(School school)
        {
            dbContext.Schools.Update(school);
            dbContext.SaveChanges();
        }

        public void DeleteSchool(School school)
        {
            dbContext.Schools.Remove(school);
            dbContext.SaveChanges();
        }

        public void AddPhone(Phone phone)
        {
            dbContext.Phones.Add(phone);
            dbContext.SaveChanges();
        }

        public List<Phone> GetPhones()
        {
            return dbContext.Phones.ToList();
        }
    }
}
