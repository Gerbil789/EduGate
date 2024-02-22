using Database.Models;

namespace Database.Repositories
{
    public class SchoolRepository
    {
        private readonly SchoolDbContext dbContext;
        public SchoolRepository()
        {
            dbContext = new SchoolDbContext();
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
    }
}
