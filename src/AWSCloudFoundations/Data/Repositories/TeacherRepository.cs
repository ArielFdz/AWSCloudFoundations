using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;

namespace AWSCloudFoundations.Data.Repositories
{
    public interface ITeacherRepository
    {
        List<Teacher> GetTeachers();
        Teacher GetTeacherById(int id);
        void PostTeacher(Teacher teacher);
        bool PutTeacher(int id, TeacherDTO teacherUpdate);
        void DeleteTeacher(Teacher teacher);
    }

    public class TeacherRepository : ITeacherRepository
    {
        static List<Teacher> teachers = new List<Teacher>();

        public List<Teacher> GetTeachers()
        {
            return teachers;
        }

        public Teacher GetTeacherById(int id)
        {
            return teachers.FirstOrDefault(x => x.Id == id);
        }

        public void PostTeacher(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public bool PutTeacher(int id, TeacherDTO teacherUpdate)
        {
            var teacher = teachers.FirstOrDefault(x => x.Id == id);

            if (teacher != null)
            {
                teacher.Names = teacherUpdate.Nombres;
                teacher.Surnames = teacherUpdate.Apellidos;
                teacher.EmployeeNumber = teacherUpdate.NumeroEmpleado;
                teacher.ClassHours = teacherUpdate.HorasClase;
                return true;
            }

            return false;
        }

        public void DeleteTeacher(Teacher teacher)
        {
            teachers.Remove(teacher);
        }
    }
}
