using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoinmentsController : ControllerBase
    {
        private readonly IGenericRepository<Appointment> _AppRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly storeContext _context;

        public AppoinmentsController(
            IGenericRepository<Appointment> appRepo,
            IUnitOfWork unitOfWork, IMapper mapper, storeContext conext)
        {
            _AppRepo = appRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = conext;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<EmployeeDto>> GetById(int id)
        //{

        //    //var spec = new EmpWithJobSpecification(id);
        //    //var emp = await _AppRepo.GetEntityWithSpec(spec);
        //    //return _mapper.Map<Core.Models.Employee, EmployeeDto>(emp);
        //}
    }
}
