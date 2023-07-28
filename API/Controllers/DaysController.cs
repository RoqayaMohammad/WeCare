using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IGenericRepository<Day> _dayRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Day> _localization;


        public DaysController(IGenericRepository<Day> dayRepo, IUnitOfWork unitOfWork, IMapper mapper
            , IStringLocalizer<Day> localization)
        {
            _dayRepo = dayRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localization = localization;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DayDto>> GetById(int id)
        {
            var day = await _dayRepo.GetByIdAsync(id);
            return _mapper.Map<Core.Models.Day, DayDto>(day);
        }

        [HttpGet]
        public async Task<ActionResult<DepartementDto>> GetAll()
        {
            var days = await _dayRepo.ListAllAsync();
            var data = _mapper.Map<IReadOnlyList<Core.Models.Day>, IReadOnlyList<DayDto>>(days);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<DayDto>> Create(DayDto dayDto)
        {
            if (dayDto == null)
            {
                return BadRequest(string.Format(_localization["objNotNull"]));
            }
            if (string.IsNullOrWhiteSpace(dayDto.Name))
            {
                throw new ArgumentException(string.Format(_localization["nameNotNull"]), nameof(dayDto));
            }

            var dayEntity = _mapper.Map<Core.Models.Day>(dayDto);

            try
            {
                await _dayRepo.AddAsync(dayEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdDayDto = _mapper.Map<DayDto>(dayEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdDayDto.Day_ID }, createdDayDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DayDto>> UpdateProduct(int id, DayDto deptDto)
        {
            // Map the ProductDto to a Product entity
            var dayEntity = _mapper.Map<Core.Models.Day>(deptDto);
            dayEntity.Id = id;

            // Update the Product entity in the database
            await _dayRepo.UpdateAsync(dayEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedDayDto = _mapper.Map<DayDto>(dayEntity);

            return Ok(updatedDayDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var dayEntity = await _dayRepo.GetByIdAsync(id);

            if (dayEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _dayRepo.DeleteAsync(dayEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
