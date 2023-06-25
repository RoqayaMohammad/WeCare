using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IGenericRepository<Doctor> _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DoctorsController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Doctor> doctorRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _doctorRepo = doctorRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            var spec = new DoctorWithDeptSpecification(id);
            var doctor = await _doctorRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.Doctor, DoctorDto>(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<DoctorDto>> GetAll([FromQuery] DoctorSpecParams doctorParams)
        {
            var spec = new DoctorWithDeptSpecification(doctorParams);
            var doctors = await _doctorRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Doctor>, IReadOnlyList<DoctorDto>>(doctors);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Create(DoctorDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("ProductDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(doctorDto.FName))
            {
                throw new ArgumentException("Product name cannot be empty or whitespace.", nameof(doctorDto));
            }

            var productEntity = _mapper.Map<Core.Models.Doctor>(doctorDto);

            try
            {
                  await _doctorRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDoctorDto = _mapper.Map<DoctorDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDoctorDto.Doctor_ID }, createdDoctorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateProduct(int id, DoctorDto doctorDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.Doctor>(doctorDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _doctorRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<DoctorDto>(productEntity);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _doctorRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _doctorRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }

    }
}
