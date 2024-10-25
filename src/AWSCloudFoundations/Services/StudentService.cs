using AutoMapper;
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Repositories;

namespace AWSCloudFoundations.Services
{
    public interface IStudentService
    {
        List<StudentDTO> GetStudents();
        StudentDTO GetStudentById(int id);
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

        public StudentDTO GetStudentById(int id)
        {
            var studentEntity = repository.GetStudentById(id);
            var student = mapper.Map<StudentDTO>(studentEntity);
            return student;
        }

        public List<StudentDTO> GetStudents()
        {
            var studentsEntity = repository.GetStudents().ToList();   
            var students = mapper.Map<List<StudentDTO>>(studentsEntity);
            return students;

        }
    }
}