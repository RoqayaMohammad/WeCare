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
    public class DoctorShiftController : ControllerBase
    {
        private readonly IGenericRepository<DoctorShift> _doctorShiftRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DoctorShiftController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<DoctorShift> doctorShiftRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _doctorShiftRepo = doctorShiftRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorShiftDto>> GetById(int id)
        {
            var spec = new DoctorShiftSpecification(id);
            var doctor = await _doctorShiftRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.DoctorShift, DoctorShiftDto>(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<DoctorShiftDto>> GetAll([FromQuery] DoctorShiftSpecParams doctorParams)
        {
            var spec = new DoctorShiftSpecification(doctorParams);
            var doctors = await _doctorShiftRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.DoctorShift>, IReadOnlyList<DoctorShiftDto>>(doctors);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorShiftDto>> Create(DoctorShiftDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("DoctorShiftDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(doctorDto.DoctorName) && string.IsNullOrWhiteSpace( doctorDto.BranchName))
            {
                throw new ArgumentException("Branch or doctor name cannot be empty or whitespace.", nameof(doctorDto));
            }

            var productEntity = _mapper.Map<Core.Models.DoctorShift>(doctorDto);

            try
            {
                await _doctorShiftRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDoctorDto = _mapper.Map<DoctorShiftDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDoctorDto.DoctorShift_ID }, createdDoctorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorShiftDto>> UpdateProduct(int id, DoctorShiftDto doctorDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.DoctorShift>(doctorDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _doctorShiftRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<DoctorShiftDto>(productEntity);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _doctorShiftRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _doctorShiftRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
