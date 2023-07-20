using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure.Data;
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

            var spec = new EmpWithJobSpecification(id);
            var emp = await _empRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.Employee, EmployeeDto>(emp);
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeDto>> GetAll([FromQuery] EmpSpecParams empParams)
        {
            var spec = new EmpWithJobSpecification(empParams);
            var emps = await _empRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Employee>, IReadOnlyList<EmployeeDto>>(emps);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto empDto)
        {
       
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
        public async Task<ActionResult<EmployeeDto>> UpdateProduct(int id, EmployeeDto empDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.Employee>(empDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _empRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<EmployeeDto>(productEntity);

            return Ok(updatedProductDto);
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

