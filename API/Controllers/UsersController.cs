using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDataDto>>> GetUsers() 
        {
            var users = await _userRepository.GetUsersAsync();

            var usersResult = _mapper.Map<IEnumerable<UserDataDto>>(users);

            return Ok(usersResult);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDataDto>> GetUser(int id) 
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return _mapper.Map<UserDataDto>(user);
        }
    }
}