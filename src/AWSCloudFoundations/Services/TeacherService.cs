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
        void PostTeacher(TeacherDTO teacherCreate);
        bool PutTeacher(int id, TeacherDTO teacherUpdate);
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

        public void PostTeacher(TeacherDTO teacherCreate)
        {
            var teacher = mapper.Map<Teacher>(teacherCreate);
            repository.PostTeacher(teacher);
        }

        public bool PutTeacher(int id, TeacherDTO teacherUpdate)
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
