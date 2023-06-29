using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitleController : ControllerBase
    {
        private readonly IGenericRepository<JobTitle> _jobRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public JobTitleController(IGenericRepository<JobTitle> jobRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jobRepo = jobRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobTitleDto>> GetById(int id)
        {
            var dept = await _jobRepo.GetByIdAsync(id);
            return _mapper.Map<Core.Models.JobTitle, JobTitleDto>(dept);
        }

        [HttpGet]
        public async Task<ActionResult<JobTitleDto>> GetAll()
        {
            var depts = await _jobRepo.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Core.Models.JobTitle>, IReadOnlyList<JobTitleDto>>(depts);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<JobTitleDto>> Create(JobTitleDto deptDto)
        {
            if (deptDto == null)
            {
                return BadRequest("ProductDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(deptDto.Title))
            {
                throw new ArgumentException("Product name cannot be empty or whitespace.", nameof(deptDto));
            }

            var productEntity = _mapper.Map<Core.Models.JobTitle>(deptDto);

            try
            {
                await _jobRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdPatientDto = _mapper.Map<JobTitleDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdPatientDto.job_ID }, createdPatientDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobTitleDto>> UpdateProduct(int id, JobTitleDto deptDto)
        {
            // Map the ProductDto to a Product entity
            var patientEntity = _mapper.Map<Core.Models.JobTitle>(deptDto);
            patientEntity.Id = id;

            // Update the Product entity in the database
            await _jobRepo.UpdateAsync(patientEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedPatientDto = _mapper.Map<JobTitleDto>(patientEntity);

            return Ok(updatedPatientDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _jobRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _jobRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
