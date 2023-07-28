using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IGenericRepository<Patient> _patientRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Patient> _localization;


        public PatientController(IGenericRepository<Patient> patientRepo, IUnitOfWork unitOfWork, IMapper mapper
            , IStringLocalizer<Patient> localization)
        {
            _patientRepo = patientRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = localization;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            return _mapper.Map<Core.Models.Patient, PatientDto>(patient);
        }

        [HttpGet]
        public async Task<ActionResult<PatientDto>> GetAll([FromQuery] PatientSpecParams patientParams)
        {
            var spec = new PatientSpecification(patientParams);
            var patients = await _patientRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Patient>, IReadOnlyList<PatientDto>>(patients);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest(string.Format(_localization["objNotNull"]));
            }
            if (string.IsNullOrWhiteSpace(patientDto.FirstName))
            {
                throw new ArgumentException(string.Format(_localization["nameNotNull"]), nameof(patientDto));
            }

            var productEntity = _mapper.Map<Core.Models.Patient>(patientDto);

            try
            {
                await _patientRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdPatientDto = _mapper.Map<PatientDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdPatientDto.Patient_ID }, createdPatientDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdateProduct(int id, PatientDto patientDto)
        {
            // Map the ProductDto to a Product entity
            var patientEntity = _mapper.Map<Core.Models.Patient>(patientDto);
            patientEntity.Id = id;

            // Update the Product entity in the database
            await _patientRepo.UpdateAsync(patientEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedPatientDto = _mapper.Map<PatientDto>(patientEntity);

            return Ok(updatedPatientDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _patientRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _patientRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
