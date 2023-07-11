using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IGenericRepository<Service> _servRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Service> servRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _servRepo = servRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetById(int id)
        {
            var spec = new ServiceSpecification(id);
            var doctor = await _servRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.Service, ServiceDto>(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceDto>> GetAll()
        {
            var spec = new ServiceSpecification();
            var doctors = await _servRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Service>, IReadOnlyList<ServiceDto>>(doctors);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceDto>> Create(ServiceDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("DoctorDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(doctorDto.Name))
            {
                throw new ArgumentException("Doctor name cannot be empty or whitespace.", nameof(doctorDto));
            }

            var productEntity = _mapper.Map<Core.Models.Service>(doctorDto);

            try
            {
                await _servRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDoctorDto = _mapper.Map<ServiceDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDoctorDto.ServId }, createdDoctorDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDto>> UpdateProduct(int id, ServiceDto servDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.Service>(servDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _servRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<ServiceDto>(productEntity);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _servRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _servRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
