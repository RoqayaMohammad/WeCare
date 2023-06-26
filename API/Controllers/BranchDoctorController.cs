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
    public class BranchDoctorController : ControllerBase
    {
        private readonly IGenericRepository<BranchDoctor> _branchDoctorRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BranchDoctorController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<BranchDoctor> branchDoctorRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _branchDoctorRepo = branchDoctorRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDoctorDto>> GetById(int id)
        {
            var spec = new BranchDoctorSpecification(id);
            var doctor = await _branchDoctorRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.BranchDoctor, BranchDoctorDto>(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<BranchDoctorDto>> GetAll()
        {
            var spec = new BranchDoctorSpecification();
            var doctors = await _branchDoctorRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.BranchDoctor>, IReadOnlyList<BranchDoctorDto>>(doctors);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<BranchDoctorDto>> Create(BranchDoctorDto doctorDto)
        {
            if (doctorDto == null)
            {
                return BadRequest("ProductDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(doctorDto.doctorName))
            {
                throw new ArgumentException("doctor name cannot be empty or whitespace.", nameof(doctorDto));
            }
            if (string.IsNullOrWhiteSpace(doctorDto.branchName))
            {
                throw new ArgumentException("branch name cannot be empty or whitespace.", nameof(doctorDto));
            }
            var productEntity = _mapper.Map<Core.Models.BranchDoctor>(doctorDto);

            try
            {
                await _branchDoctorRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDoctorDto = _mapper.Map<BranchDoctorDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDoctorDto.branchDoctor_ID }, createdDoctorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchDoctorDto>> UpdateProduct(int id, BranchDoctorDto doctorDto)
        {
            // Map the ProductDto to a Product entity
            var productEntity = _mapper.Map<Core.Models.BranchDoctor>(doctorDto);
            productEntity.Id = id;

            // Update the Product entity in the database
            await _branchDoctorRepo.UpdateAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedProductDto = _mapper.Map<BranchDoctorDto>(productEntity);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _branchDoctorRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _branchDoctorRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
