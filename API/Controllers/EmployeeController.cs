using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _empRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly storeContext _context;

        public EmployeeController(
            IGenericRepository<Employee> empRepo,
            IUnitOfWork unitOfWork, IMapper mapper, storeContext conext)
        {
            _empRepo = empRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = conext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {

            var emp = await _empRepo.GetByIdAsync(id, includeProperties: "Departement");
            return _mapper.Map<Employee, EmployeeDto>(emp);
            // var emp = await _empRepo.GetByIdAsync(id);
            // return _mapper.Map<Employee, EmployeeDto>(emp);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {

            var employees = await _empRepo.ListAllAsync(includeProperties: "Departement");
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto empDto)
        {

            //var employee = new Employee
            //{
            //    jobTitle = empDto.jobTitle,
            //    FirstName = empDto.FirstName,
            //    LastName = empDto.LastName,
            //    Address = empDto.Address,
            //    Age = empDto.Age,
            //    Phone1 = empDto.Phone1,
            //    Phone2 = empDto.Phone2,
            //    Salary = empDto.Salary
            //};
            //var createdEmployee = await _empRepo.AddAsync(employee);
            //var createdEmployeeDto = new EmployeeDto
            //{
            //    jobTitle = createdEmployee.jobTitle,
            //    FirstName = createdEmployee.FirstName,
            //    LastName = createdEmployee.LastName,
            //    Address = createdEmployee.Address,
            //    Age = createdEmployee.Age,
            //    Phone1 = createdEmployee.Phone1,
            //    Phone2 = createdEmployee.Phone2,
            //    Salary = createdEmployee.Salary
            //};
            //return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployeeDto);
            if (empDto == null)
            {
                return BadRequest("EmpDto cannot be null");
            }

            if (string.IsNullOrWhiteSpace(empDto.FirstName))
            {
                return BadRequest("Employee first name cannot be empty or whitespace.");
            }

            var empEntity = _mapper.Map<Employee>(empDto);
            var createdEmployee = await _empRepo.AddAsync(empEntity);

            var createdEmployeeDto = _mapper.Map<EmployeeDto>(empEntity);
            return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, EmployeeDto empDto)
        {
            if (empDto == null)
            {
                return BadRequest("EmpDto cannot be null");
            }

            if (string.IsNullOrWhiteSpace(empDto.FirstName))
            {
                return BadRequest("Employee first name cannot be empty or whitespace.");
            }

            var existingEmpEntity = await _empRepo.GetByIdAsync(id);

            if (existingEmpEntity == null)
            {
                return NotFound($"Employee with Id {id} not found");
            }

            // Map the updated fields from the EmployeeDto to the existing Employee entity
            _mapper.Map(empDto, existingEmpEntity);

            await _empRepo.UpdateAsync(existingEmpEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Employee entity back to a EmployeeDto
            var updatedEmpDto = _mapper.Map<EmployeeDto>(existingEmpEntity);

            return Ok(updatedEmpDto);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var empEntity = await _empRepo.GetByIdAsync(id);

            if (empEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _empRepo.DeleteAsync(empEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}

