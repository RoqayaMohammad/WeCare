using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private readonly IGenericRepository<Departement> _deptRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public DepartementController(IGenericRepository<Departement> deptRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _deptRepo = deptRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartementDto>> GetById(int id)
        {
            var dept = await _deptRepo.GetByIdAsync(id);
            return _mapper.Map<Core.Models.Departement, DepartementDto>(dept);
        }

        [HttpGet]
        public async Task<ActionResult<DepartementDto>> GetAll()
        {
            var depts = await _deptRepo.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Core.Models.Departement>, IReadOnlyList<DepartementDto>>(depts);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<DepartementDto>> Create(DepartementDto deptDto)
        {
            if (deptDto == null)
            {
                return BadRequest("ProductDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(deptDto.Name))
            {
                throw new ArgumentException("Product name cannot be empty or whitespace.", nameof(deptDto));
            }

            var productEntity = _mapper.Map<Core.Models.Departement>(deptDto);

            try
            {
                await _deptRepo.AddAsync(productEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdPatientDto = _mapper.Map<DepartementDto>(productEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdPatientDto.Dept_ID }, createdPatientDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartementDto>> UpdateProduct(int id, DepartementDto deptDto)
        {
            // Map the ProductDto to a Product entity
            var patientEntity = _mapper.Map<Core.Models.Departement>(deptDto);
            patientEntity.Id = id;

            // Update the Product entity in the database
            await _deptRepo.UpdateAsync(patientEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedPatientDto = _mapper.Map<DepartementDto>(patientEntity);

            return Ok(updatedPatientDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var productEntity = await _deptRepo.GetByIdAsync(id);

            if (productEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _deptRepo.DeleteAsync(productEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
