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

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.emp_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Phone2, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Phone2) ? null : src.Phone2))
                .ForMember(dest => dest.jobName, opt => opt.MapFrom(src => src.jobTitle.Title))
                .ReverseMap();


            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.emp_Id))
                .ForMember(dest => dest.jobTitle, opt => opt.MapFrom<jobTitleResolver>())
                .ForMember(dest => dest.jobID, opt => opt.MapFrom<jobIdValueResolver>())
                .ReverseMap();







            //
            CreateMap<JobTitle, JobTitleDto>()
                .ForMember(dest => dest.job_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Departement, DepartementDto>()
                .ForMember(dest => dest.Dept_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Day, DayDto>()
                .ForMember(dest => dest.Day_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.Patient_ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Branch, BranchDto>()
                .ForMember(dest => dest.Branch_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.weekendDay, opt => opt.MapFrom(src => src.weekend.Name))
                .ReverseMap();


            CreateMap<BranchDto, Branch>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Branch_ID))
                .ForMember(dest => dest.weekend, opt => opt.MapFrom<dayNameResolver>())
                .ForMember(dest => dest.weekendID, opt => opt.MapFrom<dayIdValueResolver>()).ReverseMap();


            CreateMap<BranchDoctor, BranchDoctorDto>()
                .ForMember(dest => dest.branchDoctor_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.doctorName, opt => opt.MapFrom(src => src.doctor.FName))
                .ForMember(dest => dest.branchName, opt => opt.MapFrom(src => src.branch.Name))
                .ReverseMap();

            CreateMap<BranchDoctorDto, BranchDoctor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.branchDoctor_ID))
                .ForMember(dest => dest.doctorID, opt => opt.MapFrom<doctorIdValueResolver>())
                .ForMember(dest => dest.doctor, opt => opt.MapFrom<doctorNameResolver>())
                .ForMember(dest => dest.branchID, opt => opt.MapFrom<branchIdValueResolver>())
                .ForMember(dest => dest.branch, opt => opt.MapFrom<branchNameResolver>())
                .ReverseMap();

            CreateMap<DoctorShift, DoctorShiftDto>()
                .ForMember(dest => dest.DoctorShift_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.branchDoctor.doctor.FName))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.branchDoctor.branch.Name))
                .ForMember(dest => dest.dayOfWeek, opt => opt.MapFrom(src => src.day.Name))
                .ReverseMap();

            CreateMap<DoctorShiftDto, DoctorShift>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DoctorShift_ID))
                .ForMember(dest => dest.branchDoctor, opt => opt.MapFrom<branchDoctorNameResolver>())
                .ForMember(dest => dest.branchDoctor_ID, opt => opt.MapFrom<branchDoctorIdValueResolver>())
                .ForMember(dest => dest.day_ID, opt => opt.MapFrom<dayIdDSValueResolver>())
                .ForMember(dest => dest.day, opt => opt.MapFrom<dayNameDSResolver>())
                .ReverseMap();

            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.Doctor_ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.departementName, opt => opt.MapFrom(src => src.departement.Name))
                .ReverseMap();

            CreateMap<DoctorDto, Doctor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Doctor_ID))
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
                if (source.departementName != null)
                {
                    var dept = _context.Departements.FirstOrDefault(pc => pc.Name == source.departementName);
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
                if (source.departementName != null)
                {
                    var dept = _context.Departements.FirstOrDefault(pc => pc.Name == source.departementName);
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
                if (source.weekendDay != null)
                {
                    var day = _context.days.FirstOrDefault(pc => pc.Name == source.weekendDay);
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
                if (source.weekendDay != null)
                {
                    var day = _context.days.FirstOrDefault(pc => pc.Name == source.weekendDay);
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
    public class dayIdDSValueResolver : IValueResolver<DoctorShiftDto, DoctorShift, int>
    {
        private readonly storeContext _context;

        public dayIdDSValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(DoctorShiftDto source, DoctorShift destination, int destMember, ResolutionContext context)
        {
            if (source.dayOfWeek != null)
            {
                var day = _context.days.FirstOrDefault(pc => pc.Name == source.dayOfWeek);
                if (day != null)
                {
                    destination.day_ID = day.Id;

                    return destination.day_ID;
                }
            }

            return 1;
        }
    }

    public class dayNameDSResolver : IValueResolver<DoctorShiftDto, DoctorShift, Day>
    {
        private readonly storeContext _context;

        public dayNameDSResolver(storeContext context)
        {
            _context = context;
        }

        Day IValueResolver<DoctorShiftDto, DoctorShift, Day>.Resolve(DoctorShiftDto source, DoctorShift destination, Day destMember, ResolutionContext context)
        {
            if (source.dayOfWeek != null)
            {
                var day = _context.days.FirstOrDefault(pc => pc.Name == source.dayOfWeek);
                if (day != null)
                {
                    destination.day_ID = day.Id;
                    destination.day = day;
                    return destination.day;
                }
            }

            throw new NotImplementedException();
        }
    }
    public class doctorNameResolver : IValueResolver<BranchDoctorDto, BranchDoctor, Doctor>
    {
        private readonly storeContext _context;

        public doctorNameResolver(storeContext context)
        {
            _context = context;
        }

        public Doctor Resolve(BranchDoctorDto source, BranchDoctor destination, Doctor destMember, ResolutionContext context)
        {
            if (source.doctorName != null)
            {
                var doctor = _context.Doctors.FirstOrDefault(pc => pc.FName == source.doctorName);
                if (doctor != null)
                {
                    destination.doctorID = doctor.Id;
                    destination.doctor = doctor;
                    return destination.doctor;
                }
            }

            return destination.doctor;
        }
    }
    public class doctorIdValueResolver : IValueResolver<BranchDoctorDto, BranchDoctor, int>
    {
        private readonly storeContext _context;

        public doctorIdValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(BranchDoctorDto source, BranchDoctor destination, int destMember, ResolutionContext context)
        {
            if (source.doctorName != null)
            {
                var empDept = _context.Doctors.FirstOrDefault(pc => pc.FName == source.doctorName);
                if (empDept != null)
                {
                    destination.doctorID = empDept.Id;

                    return destination.doctorID;
                }
            }

            return 1;
        }
    }
    public class branchNameResolver : IValueResolver<BranchDoctorDto, BranchDoctor, Branch>
    {
        private readonly storeContext _context;

        public branchNameResolver(storeContext context)
        {
            _context = context;
        }

        public Branch Resolve(BranchDoctorDto source, BranchDoctor destination, Branch destMember, ResolutionContext context)
        {
            if (source.branchName != null)
            {
                var empDepart = _context.Branches.FirstOrDefault(pc => pc.Name == source.branchName);
                if (empDepart != null)
                {
                    destination.branchID = empDepart.Id;
                    destination.branch = empDepart;
                    return destination.branch;
                }
            }

            return destination.branch;
        }
    }
    public class branchIdValueResolver : IValueResolver<BranchDoctorDto, BranchDoctor, int>
    {
        private readonly storeContext _context;

        public branchIdValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(BranchDoctorDto source, BranchDoctor destination, int destMember, ResolutionContext context)
        {
            if (source.branchName != null)
            {
                var empDept = _context.Branches.FirstOrDefault(pc => pc.Name == source.branchName);
                if (empDept != null)
                {
                    destination.branchID = empDept.Id;

                    return destination.branchID;
                }
            }

            return 1;
        }
    }

    public class branchDoctorNameResolver : IValueResolver<DoctorShiftDto, DoctorShift, BranchDoctor>
    {
        private readonly storeContext _context;

        public branchDoctorNameResolver(storeContext context)
        {
            _context = context;
        }

        public BranchDoctor Resolve(DoctorShiftDto source, DoctorShift destination, BranchDoctor destMember, ResolutionContext context)
        {
            if (source.BranchName != null && source.DoctorName != null)
            {
                var branchDoctor = _context.BranchDoctors.FirstOrDefault(pc => pc.branch.Name == source.BranchName
                && pc.doctor.FName == source.DoctorName);

                if (branchDoctor != null)
                {
                    destination.branchDoctor_ID = branchDoctor.Id;
                    destination.branchDoctor = branchDoctor;
                    return destination.branchDoctor;
                }
            }

            return destination.branchDoctor;
        }
    }
    public class branchDoctorIdValueResolver : IValueResolver<DoctorShiftDto, DoctorShift, int>
    {
        private readonly storeContext _context;

        public branchDoctorIdValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(DoctorShiftDto source, DoctorShift destination, int destMember, ResolutionContext context)
        {
            if (source.BranchName != null && source.DoctorName != null)
            {
                var branchDoctor = _context.BranchDoctors.FirstOrDefault(pc => pc.branch.Name == source.BranchName
                && pc.doctor.FName == source.DoctorName);

                if (branchDoctor != null)
                {
                    destination.branchDoctor_ID = branchDoctor.Id;

                    return destination.branchDoctor_ID;
                }
            }

            return 1;
        }
    }

    public class jobTitleResolver : IValueResolver<EmployeeDto, Employee, JobTitle>
    {
        private readonly storeContext _context;

        public jobTitleResolver(storeContext context)
        {
            _context = context;
        }

        public JobTitle Resolve(EmployeeDto source, Employee destination, JobTitle destMember, ResolutionContext context)
        {
            if (source.jobName != null)
            {
                var doctor = _context.JobTitles.FirstOrDefault(pc => pc.Title == source.jobName);
                if (doctor != null)
                {
                    destination.jobID = doctor.Id;
                    destination.jobTitle = doctor;
                    return destination.jobTitle;
                }
            }

            return destination.jobTitle;
        }
    }
    public class jobIdValueResolver : IValueResolver<EmployeeDto, Employee, int>
    {
        private readonly storeContext _context;

        public jobIdValueResolver(storeContext context)
        {
            _context = context;
        }
        public int Resolve(EmployeeDto source, Employee destination, int destMember, ResolutionContext context)
        {
            if (source.jobName != null)
            {
                var empDept = _context.JobTitles.FirstOrDefault(pc => pc.Title == source.jobName);
                if (empDept != null)
                {
                    destination.jobID = empDept.Id;

                    return destination.jobID;
                }
            }

            return 1;
        }
    }
}
