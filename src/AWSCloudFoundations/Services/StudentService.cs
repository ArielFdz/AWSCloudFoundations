using AutoMapper;
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;
using AWSCloudFoundations.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AWSCloudFoundations.Services
{
    public interface IStudentService
    {
        List<StudentDTO> GetStudents();
        StudentDTO GetStudentById(int id);
        void PostStudent(StudentCreateDTO studentCreate);
        bool PutStudent(int id, StudentCreateDTO studentUpdate);
        bool DeleteStudent(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository repository;
        private readonly IMapper mapper;

        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<StudentDTO> GetStudents()
        {
            var studentsEntity = repository.GetStudents().ToList();
            var students = mapper.Map<List<StudentDTO>>(studentsEntity);
            return students;

        }

        public StudentDTO GetStudentById(int id)
        {
            var studentEntity = repository.GetStudentById(id);
            var student = mapper.Map<StudentDTO>(studentEntity);
            return student;
        }

        public void PostStudent(StudentCreateDTO studentCreate)
        {
            var lstStudents = repository.GetStudents().ToList();

            var student = mapper.Map<Student>(studentCreate);
            student.Id = lstStudents.Count > 0 ? lstStudents.Max(x => x.Id) + 1 : 1;

            repository.PostStudent(student);
        }

        public bool PutStudent(int id, StudentCreateDTO studentUpdate)
        {
            return repository.PutStudent(id, studentUpdate);
        }

        public bool DeleteStudent(int id)
        {
            var studentEntity = repository.GetStudentById(id);
            if (studentEntity != null)
            {
                repository.DeleteStudent(studentEntity);
                return true;
            }
            return false;
        }
    }
}