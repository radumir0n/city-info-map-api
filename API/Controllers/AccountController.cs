using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody]AccountDto accountDto)
        {
            if (await UserExists(accountDto.Username))
            {
                return BadRequest("Username is already taken");
            }

            using var hmac = new HMACSHA512();

            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountDto.Password));
            var user = new AppUser 
            {
                UserName = accountDto.Username.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]AccountDto accountDto)
        {
             var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == accountDto.Username);

             if (user is null) 
             {
                return Unauthorized("Username is invalid");
             }

             using var hmac = new HMACSHA512(user.PasswordSalt);

             var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accountDto.Password));

             for (var i = 0; i < computedHash.Length; ++i) 
             {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Password is invalid");
                }
             }

             return new UserDto
             {
                Id = user.Id,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
             };
        }

        private async Task<bool> UserExists(string username) 
        {
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}