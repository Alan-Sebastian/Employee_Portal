using AutoMapper;
using Employee_Portal.DTO;
using Employee_Portal.Models;

namespace Employee_Portal.Automapper
{
    public class AutoMapperProfile : Profile
    {
        

      
       public AutoMapperProfile() 
        { 
            CreateMap<SignupDto,User>().ForMember(dest=>dest.UserName,
                opt=>opt.MapFrom(src=>src.UserName))
                .ForMember(dest=>dest.PasswordHash,opt=>opt.MapFrom(src=>src.PasswordHash))
                .ForAllMembers(opt=>opt.Ignore());

            CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()) // Ignore EmployeeId
            .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.ToUniversalTime())) // optional UTC conversion
            .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.ToUniversalTime())); // optional UTC conversion

            CreateMap<UpdateEmployeeDto, Employee>()
           .ForMember(dest => dest.HireDate, opt => opt.Condition(src => src.HireDate.HasValue))
           .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.Value.ToUniversalTime()))
           .ForMember(dest => dest.Dob, opt => opt.Condition(src => src.Dob.HasValue))
           .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.Dob.Value.ToUniversalTime()))
           .ForAllMembers(opt => opt.Condition((src, dest, srcValue) => srcValue != null));

        }

    }
}
