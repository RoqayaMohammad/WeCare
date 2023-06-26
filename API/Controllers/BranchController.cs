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
    public class BranchController : ControllerBase
    {
        private readonly IGenericRepository<Branch> _branchRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BranchController(IGenericRepository<Branch> branchRepo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _branchRepo = branchRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetById(int id)
        {
            var spec = new BranchSpecification(id);
            var branch = await _branchRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.Branch, BranchDto>(branch);
        }

        [HttpGet]
        public async Task<ActionResult<BranchDto>> GetAll()
        {
            var spec = new BranchSpecification();
            var branches = await _branchRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Branch>, IReadOnlyList<BranchDto>>(branches);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<BranchDto>> Create(BranchDto branchDto)
        {
            if (branchDto == null)
            {
                return BadRequest("ProductDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(branchDto.Name))
            {
                throw new ArgumentException("Product name cannot be empty or whitespace.", nameof(branchDto));
            }

            var branchEntity = _mapper.Map<Core.Models.Branch>(branchDto);

            try
            {
                await _branchRepo.AddAsync(branchEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdbranchDto = _mapper.Map<BranchDto>(branchEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdbranchDto.Branch_ID }, createdbranchDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchDto>> UpdateProduct(int id, BranchDto branchDto)
        {
            // Map the ProductDto to a Product entity
            var branchEntity = _mapper.Map<Core.Models.Branch>(branchDto);
            branchEntity.Id = id;

            // Update the Product entity in the database
            await _branchRepo.UpdateAsync(branchEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedbranchDto = _mapper.Map<BranchDto>(branchEntity);

            return Ok(updatedbranchDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var branchEntity = await _branchRepo.GetByIdAsync(id);

            if (branchEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _branchRepo.DeleteAsync(branchEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
