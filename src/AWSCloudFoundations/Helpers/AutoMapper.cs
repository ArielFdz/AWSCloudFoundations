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
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Nombres, opt => opt.MapFrom(src => src.Names))
                .ForMember(x => x.Apellidos, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(x => x.Matricula, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(x => x.Promedio, opt => opt.MapFrom(src => src.Average));

            CreateMap<StudentDTO, Student>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Names, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(x => x.Surnames, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(x => x.StudentId, opt => opt.MapFrom(src => src.Matricula))
                .ForMember(x => x.Average, opt => opt.MapFrom(src => src.Promedio));

            CreateMap<Teacher, TeacherDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.NumeroEmpleado, opt => opt.MapFrom(src => src.EmployeeNumber))
                .ForMember(x => x.Nombres, opt => opt.MapFrom(src => src.Names))
                .ForMember(x => x.Apellidos, opt => opt.MapFrom(src => src.Surnames))
                .ForMember(x => x.HorasClase, opt => opt.MapFrom(src => src.ClassHours));

            CreateMap<TeacherDTO, Teacher>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.EmployeeNumber, opt => opt.MapFrom(src => src.NumeroEmpleado))
                .ForMember(x => x.Names, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(x => x.Surnames, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(x => x.ClassHours, opt => opt.MapFrom(src => src.HorasClase));
        }
    }
}
