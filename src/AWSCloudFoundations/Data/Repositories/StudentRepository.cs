using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;

namespace AWSCloudFoundations.Data.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
        void PostStudent(Student student);
        bool PutStudent(int id, StudentDTO updatedStudent);
        void DeleteStudent(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        static List<Student> students = new List<Student>();

        public List<Student> GetStudents()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }

        public void PostStudent(Student student)
        {
            students.Add(student);
        }

        public bool PutStudent(int id, StudentDTO updatedStudent)
        {
            var student = students.FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                student.Names = updatedStudent.Nombres;
                student.Surnames = updatedStudent.Apellidos;
                student.StudentId = updatedStudent.Matricula;
                student.Average = updatedStudent.Promedio;
                return true;
            }

            return false;
        }

        public void DeleteStudent(Student student)
        {
            students.Remove(student);
        }
    }
}