using API.Dtos;
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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppEmp> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppEmp> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AppEmpDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken");

            var emp = _mapper.Map<AppEmp>(registerDto);

            emp.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(emp, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(emp, "Employee");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new AppEmpDto
            {
                Username = emp.UserName,
                Token = await _tokenService.CreateToken(emp),
                Email = emp.Email
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppEmpDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                //.Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username || x.Email == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid password");

            return new AppEmpDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                //PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                Email = user.Email
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower() || x.Email == username);
        }


    }
}
