using AutoMapper;
using AWSCloudFoundations.Data.DTOs;
using AWSCloudFoundations.Data.Entities;

namespace AWSCloudFoundations.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(x => x.Nombres, opt => opt.MapFrom(src => src.Names))
                .ForMember(x => x.Apellidos, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(x => x.Matricula, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(x => x.Promedio, opt => opt.MapFrom(src => src.Average));

            CreateMap<Teacher, TeacherDTO>()
                .ForMember(x => x.NumeroEmpleado, opt => opt.MapFrom(src => src.EmployeeNumber))
                .ForMember(x => x.Nombres, opt => opt.MapFrom(src => src.Names))
                .ForMember(x => x.Apellidos, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(x => x.HorasClase, opt => opt.MapFrom(src => src.ClassHours));
        }
    }
}
