using API.Dtos;
using AutoMapper;
using Core.Models;
using Infrastructure;
using System.Drawing;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Phone2) ? null : src.Phone2))
            .ForMember(dest => dest.DepartementName, opt => opt.MapFrom(src => src.Departement != null ? src.Departement.Name : null))
            .ReverseMap();


            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Departement, opt => opt.MapFrom<DeptNameResolver>())
                .ForMember(dest => dest.DeptId, opt => opt.MapFrom<DeptIdValueResolver>())
                ;


            

           
        //
        CreateMap<Departement, DepartementDto>()
                .ForMember(dest => dest.Dept_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Patient_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Departement, DoctorDto>()
                .ForMember(dest => dest.departement, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Doctor_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FName))
                .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Phone1, opt => opt.MapFrom(src => src.Phone1))
                .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => src.Phone2))
                .ForMember(dest => dest.BirthOfDate, opt => opt.MapFrom(src => src.BirthOfDate))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.departement, opt => opt.MapFrom(src => src.departement.Name))
                .ReverseMap();

            CreateMap<DoctorDto, Doctor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Doctor_ID))
                .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FName))
                .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Phone1, opt => opt.MapFrom(src => src.Phone1))
                .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => src.Phone2))
                .ForMember(dest => dest.BirthOfDate, opt => opt.MapFrom(src => src.BirthOfDate))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.departement, opt => opt.MapFrom<deptNameResolver>())
                .ForMember(dest => dest.DeptID, opt => opt.MapFrom<deptIdValueResolver>()).ReverseMap();
        }
        public class deptIdValueResolver : IValueResolver<DoctorDto, Doctor, int>
        {
            private readonly storeContext _context;

            public deptIdValueResolver(storeContext context)
            {
                _context = context;
            }
            public int Resolve(DoctorDto source, Doctor destination, int destMember, ResolutionContext context)
            {
                if (source.departement != null)
                {
                    var dept = _context.Departements.FirstOrDefault(pc => pc.Name == source.departement);
                    if (dept != null)
                    {
                        destination.DeptID = dept.Id;

                        return destination.DeptID;
                    }
                }

                return 1;
            }
        }
        public class deptNameResolver : IValueResolver<DoctorDto, Doctor, Departement>
        {
            private readonly storeContext _context;

            public deptNameResolver(storeContext context)
            {
                _context = context;
            }

            public Departement Resolve(DoctorDto source, Doctor destination, Departement destMember, ResolutionContext context)
            {
                if (source.departement != null)
                {
                    var dept = _context.Departements.FirstOrDefault(pc => pc.Name == source.departement);
                    if (dept != null)
                    {
                        destination.DeptID = dept.Id;
                        destination.departement = dept;
                        return destination.departement;
                    }
                }

                throw new NotImplementedException();
            }
        }
    }

    public class DeptNameResolver : IValueResolver<EmployeeDto, Employee, Departement>
    {
        private readonly storeContext _context;

        public DeptNameResolver(storeContext context)
        {
            _context = context;
        }

        public Departement Resolve(EmployeeDto source, Employee destination, Departement destMember, ResolutionContext context)
        {
            if (source.DepartementName != null)
            {
                var empDepart = _context.Departements.FirstOrDefault(pc => pc.Name == source.DepartementName);
                if (empDepart != null)
                {
                    destination.DeptId = empDepart.Id;
                    destination.Departement = empDepart;
                    return destination.Departement;
                }
            }

            return destination.Departement;
        }
    }
    public class DeptIdValueResolver : IValueResolver<EmployeeDto, Employee, int>
    {
        private readonly storeContext _context;

        public DeptIdValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(EmployeeDto source, Employee destination, int destMember, ResolutionContext context)
        {
            if (source.DepartementName != null)
            {
                var empDept = _context.Departements.FirstOrDefault(pc => pc.Name == source.DepartementName);
                if (empDept != null)
                {
                    destination.DeptId = empDept.Id;

                    return destination.DeptId;
                }
            }

            return 1;
        }
    }

}
