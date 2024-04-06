using Database.Models;

namespace Database.Repositories
{
    public class SchoolRepository
    {
        private readonly EduGateDbContext dbContext;
        public SchoolRepository()
        {
            dbContext = new EduGateDbContext();
        }

        public IEnumerable<Student> GetStudents()
        {
            return dbContext.Students;
        }

        public void AddStudent(Student student)
        {
            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }

        public void AddSchool(School school)
        {
            dbContext.Schools.Add(school);
            dbContext.SaveChanges();
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
