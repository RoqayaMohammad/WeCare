using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppEmp> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _uow;
        public AdminController(UserManager<AppEmp> userManager, IUnitOfWork uow, IMapper mapper,
            IPhotoService photoService)
        {
            _userManager = userManager;
            _uow = uow;
            _photoService = photoService;
            _mapper = mapper;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("emps-with-roles")]
        public async Task<ActionResult> GetEmpssWithRoles()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.EmpRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        //[HttpPost("add-photo")]
        //public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        //{
        //    var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

        //    if (user == null) return NotFound();

        //    var result = await _photoService.AddPhotoAsync(file);

        //    if (result.Error != null) return BadRequest(result.Error.Message);

        //    var photo = new Photo
        //    {
        //        Url = result.SecureUrl.AbsoluteUri,
        //        PublicId = result.PublicId
        //    };

        //    if (user.Photos.Count == 0) photo.IsMain = true;

        //    user.Photos.Add(photo);

        //    if (await _uow.Complete())
        //    {
        //        return CreatedAtAction(nameof(GetUser),
        //            new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
        //    }

        //    return BadRequest("Problem adding photo");
        //}

        //[HttpPut("set-main-photo/{photoId}")]
        //public async Task<ActionResult> SetMainPhoto(int photoId)
        //{
        //    var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

        //    if (user == null) return NotFound();

        //    var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        //    if (photo == null) return NotFound();

        //    if (photo.IsMain) return BadRequest("this is already your main photo");

        //    var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        //    if (currentMain != null) currentMain.IsMain = false;
        //    photo.IsMain = true;

        //    if (await _uow.Complete()) return NoContent();

        //    return BadRequest("Problem setting the main photo");
        //}

        //[HttpDelete("delete-photo/{photoId}")]
        //public async Task<ActionResult> DeletePhoto(int photoId)
        //{
        //    var user = await _uow.UserRepository.GetUserByUsernameAsync(User.GetUsername());

        //    var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        //    if (photo == null) return NotFound();

        //    if (photo.IsMain) return BadRequest("You cannot delete your main photo");

        //    if (photo.PublicId != null)
        //    {
        //        var result = await _photoService.DeletePhotoAsync(photo.PublicId);
        //        if (result.Error != null) return BadRequest(result.Error.Message);
        //    }

        //    user.Photos.Remove(photo);

        //    if (await _uow.Complete()) return Ok();

        //    return BadRequest("Problem deleting photo");
        //}
    }
}

