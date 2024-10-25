
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;
using Microsoft.VisualBasic;

namespace AWSCloudFoundations.Data.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        List<Student> students = new List<Student>();

        public Student GetStudentById(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }

        public List<Student> GetStudents()
        {
            return students;
        }
    }
}