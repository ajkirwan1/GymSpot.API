using AutoMapper;
using GymSpot.API.Models.Domain;
using GymSpot.API.Models.DTOs.UserDTOs;
using GymSpot.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GymSpot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAllUsers()
        {
            var userEntity = await _userRepository.GetAllAsync();

            if (userEntity.Count == 0)
            {
                return NotFound();
            }

            var usersDtoList = _mapper.Map<List<UserDTO>>(userEntity);

            return Ok(usersDtoList);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDTO>(userEntity);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserRequestDTO addUserRequestDTO)
        {
            if (addUserRequestDTO == null)
            {
                return BadRequest();
            }
            if (addUserRequestDTO.Name.IsNullOrEmpty() || addUserRequestDTO.Password.IsNullOrEmpty() || addUserRequestDTO.Email.IsNullOrEmpty() || addUserRequestDTO.Role.IsNullOrEmpty())
            {
                return BadRequest();
            };

            var userEntity = _mapper.Map<User>(addUserRequestDTO);

            userEntity = await _userRepository.CreateAsync(userEntity);

            var userDto = _mapper.Map<UserDTO>(userEntity);

            return Ok(userDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var userEntity = _mapper.Map<User>(updateUserDTO);

            var userDomain = await _userRepository.UpdateAsync(id, userEntity);

            if (userDomain == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDTO>(userDomain);

            return Ok(userDto);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var userEntity = await _userRepository.DeleteAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<UserDTO>(userEntity);

            return Ok(regionDto);

        }
    }
}
