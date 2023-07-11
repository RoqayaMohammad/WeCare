﻿using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifictions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppoinmentsController : ControllerBase
    {
        private readonly IGenericRepository<Appointment> _appRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public AppoinmentsController(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<Appointment> appRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appRepo = appRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetById(int id)
        {
            var spec = new AppoinmentWithSpecification(id);
            var app = await _appRepo.GetEntityWithSpec(spec);
            return _mapper.Map<Core.Models.Appointment, AppointmentDto>(app);
        }

        [HttpGet]
        public async Task<ActionResult<AppointmentDto>> GetAll([FromQuery] AppoinmentSpecParams appParams)
        {
            var spec = new AppoinmentWithSpecification(appParams);
            var apps = await _appRepo.GetEntitiesWithSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Core.Models.Appointment>, IReadOnlyList<AppointmentDto>>(apps);
            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appDto)
        {
            if (appDto == null)
            {
                return BadRequest("appDto cannot be null");
            }
            if (string.IsNullOrWhiteSpace(appDto.PatientName))
            {
                throw new ArgumentException("patient name cannot be empty or whitespace.", nameof(appDto));
            }

            var appEntity = _mapper.Map<Core.Models.Appointment>(appDto);

            try
            {
                await _appRepo.AddAsync(appEntity);
            }
            catch (Exception ex)
            {
                throw;
            }

            var createdAppDto = _mapper.Map<AppointmentDto>(appEntity);

            return CreatedAtAction(nameof(GetById), new { id = createdAppDto.AppId }, createdAppDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentDto>> UpdateProduct(int id, AppointmentDto appDto)
        {
            // Map the ProductDto to a Product entity
            var appEntity = _mapper.Map<Core.Models.Appointment>(appDto);
            appEntity.Id = id;

            // Update the Product entity in the database
            await _appRepo.UpdateAsync(appEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            // Map the updated Product entity back to a ProductDto
            var updatedAppDto = _mapper.Map<AppointmentDto>(appEntity);

            return Ok(updatedAppDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Get the Product entity from the database
            var appEntity = await _appRepo.GetByIdAsync(id);

            if (appEntity == null)
            {
                return NotFound();
            }

            // Delete the Product entity from the database
            await _appRepo.DeleteAsync(appEntity);

            // Save changes to the database
            await _unitOfWork.Complete();

            return NoContent();
        }
    }
}
