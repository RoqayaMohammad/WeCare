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
    public class ServiceDoctorController : ControllerBase
    {
        private readonly IGenericRepository<ServiceDoctor> _servDoctorRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ServiceDoctorController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<ServiceDoctor> servDoctorRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _servDoctorRepo = servDoctorRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDoctorDto>> GetById(int id)
        {
            var spec = new ServiceDoctorSpecification(id);
            var doctor = await _servDoctorRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.ServiceDoctor, ServiceDoctorDto>(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceDoctorDto>> GetAll()
        {
            var spec = new ServiceDoctorSpecification();
            var doctors = await _servDoctorRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.ServiceDoctor>, IReadOnlyList<ServiceDoctorDto>>(doctors);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<ServiceDoctorDto>> Create(ServiceDoctorDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("DoctorDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(doctorDto.ServiceName))
            {
                throw new ArgumentException("Doctor name cannot be empty or whitespace.", nameof(doctorDto));
            }

            var productEntity = _mapper.Map<Core.Models.ServiceDoctor>(doctorDto);

            try
            {
                await _servDoctorRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDoctorDto = _mapper.Map<ServiceDoctorDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDoctorDto.servDoctorId }, createdDoctorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceDoctorDto>> UpdateProduct(int id, ServiceDoctorDto doctorDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.ServiceDoctor>(doctorDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _servDoctorRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<ServiceDoctorDto>(productEntity);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _servDoctorRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _servDoctorRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
