using Database.Repositories;
using Database.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplicationMVC
{
    public class DatabaseService
    {
        private readonly DatabaseRepository databaseRepository = new();

        public async Task<bool> Login(string email, string password, HttpContext httpContext)
        {
            try
            {
                var student = await databaseRepository.Login(email, password);
                httpContext.Session.SetInt32("LoggedUserId", student.StudentId);
                httpContext.Session.SetString("LoggedUserEmail", student.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(Student student, HttpContext httpContext)
        {
            try
            {
                var s = await databaseRepository.Register(student);
                httpContext.Session.SetInt32("LoggedUserId", s.StudentId);
                httpContext.Session.SetString("LoggedUserEmail", s.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Logout(HttpContext httpContext)
        {
            httpContext.Session.Remove("LoggedUserId");
            httpContext.Session.Remove("LoggedUserEmail");
        }
        public async Task<List<School>> GetSchoolsAsync()
        {
            return (await databaseRepository.GetSchools()).ToList();
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            try
            {
                return await databaseRepository.UpdateStudent(student);
            }
            catch
            {
                return false;
            }
        }

        public async Task<Student> GetStudent(int studentId)
        {
            return await databaseRepository.GetStudent(studentId);
        }
    }
}
