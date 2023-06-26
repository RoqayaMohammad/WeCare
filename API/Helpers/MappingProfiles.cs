using API.Dtos;
using AutoMapper;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Drawing;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Departement, DepartementDto>()
                .ForMember(dest => dest.Dept_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Day, DayDto>()
                .ForMember(dest => dest.Day_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Patient_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Departement, DoctorDto>()
                .ForMember(dest => dest.departement, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();


            CreateMap<Day, BranchDto>()
                .ForMember(dest => dest.weekend, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();


            CreateMap<Branch, BranchDto>()
                .ForMember(dest => dest.Branch_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.weekend, opt => opt.MapFrom(src => src.weekend.Name))
                .ReverseMap();

            CreateMap<BranchDto, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Branch_ID))
                .ForMember(dest => dest.weekend, opt => opt.MapFrom<dayNameResolver>())
                .ForMember(dest => dest.weekendID, opt => opt.MapFrom<dayIdValueResolver>()).ReverseMap();
        
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
        public class dayIdValueResolver : IValueResolver<BranchDto, Branch, int>
        {
            private readonly storeContext _context;

            public dayIdValueResolver(storeContext context)
            {
                _context = context;
            }
            public int Resolve(BranchDto source, Branch destination, int destMember, ResolutionContext context)
            {
                if (source.weekend != null)
                {
                    var day = _context.days.FirstOrDefault(pc => pc.Name == source.weekend);
                    if (day != null)
                    {
                        destination.weekendID = day.Id;

                        return destination.weekendID;
                    }
                }

                return 1;
            }
        }
        public class dayNameResolver : IValueResolver<BranchDto, Branch, Day>
        {
            private readonly storeContext _context;

            public dayNameResolver(storeContext context)
            {
                _context = context;
            }

            Day IValueResolver<BranchDto, Branch, Day>.Resolve(BranchDto source, Branch destination, Day destMember, ResolutionContext context)
            {
                if (source.weekend != null)
                {
                    var day = _context.days.FirstOrDefault(pc => pc.Name == source.weekend);
                    if (day != null)
                    {
                        destination.weekendID = day.Id;
                        destination.weekend = day;
                        return destination.weekend;
                    }
                }

                throw new NotImplementedException();
            }
        }

    }
    
}
