using AutoMapper;
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;
using AWSCloudFoundations.Data.Repositories;

namespace AWSCloudFoundations.Services
{
    public interface ITeacherService
    {
        List<TeacherDTO> GetTeachers();
        TeacherDTO GetTeacherById(int id);
        void PostTeacher(TeacherCreateDTO teacherCreate);
        bool PutTeacher(int id, TeacherCreateDTO teacherUpdate);
        bool DeleteTeacher(int id);
    }

    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository repository;
        private readonly IMapper mapper;

        public TeacherService(ITeacherRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<TeacherDTO> GetTeachers()
        {
            var teachersEntity = repository.GetTeachers();
            var teachers = mapper.Map<List<TeacherDTO>>(teachersEntity);
            return teachers;
        }

        public TeacherDTO GetTeacherById(int id)
        {
            var teacherEntity = repository.GetTeacherById(id);
            var teacher = mapper.Map<TeacherDTO>(teacherEntity);
            return teacher;
        }

        public void PostTeacher(TeacherCreateDTO teacherCreate)
        {
            var lstStudents = repository.GetTeachers().ToList();

            var teacher = mapper.Map<Teacher>(teacherCreate);
            teacher.Id = lstStudents.Count > 0 ? lstStudents.Max(x => x.Id) + 1 : 1;

            repository.PostTeacher(teacher);
        }

        public bool PutTeacher(int id, TeacherCreateDTO teacherUpdate)
        {
            return repository.PutTeacher(id, teacherUpdate);
        }

        public bool DeleteTeacher(int id)
        {
            var teacherEntity = repository.GetTeacherById(id);
            if (teacherEntity != null)
            {
                repository.DeleteTeacher(teacherEntity);
                return true;
            }
            return false;
        }
    }
}
