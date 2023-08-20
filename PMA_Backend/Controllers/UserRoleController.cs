using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMA_Core.DTOs;
using PMA_Core.Models;
using PMA_Services.Services;

namespace PMA_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _roleService;
        private readonly IMapper _mapper;

        public UserRoleController(UserRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMA_UserRole>>> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PMA_UserRole>> GetRoleAsync(int id)
        {
            var role = await _roleService.GetRoleAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleDTO>> CreateRoleAsync(UserRoleDTO roleDTO)
        {
            if (roleDTO == null)
            {
                return BadRequest();
            }
            var role = _mapper.Map<PMA_UserRole>(roleDTO);
            var createdRole = await _roleService.CreateRoleAsync(role);

            return CreatedAtAction(nameof(GetRoleAsync), new { id = createdRole.RoleID }, createdRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(int id, PMA_UserRole role)
        {
            if (id != role.RoleID)
            {
                return BadRequest();
            }

            var updatedRole = await _roleService.UpdateRoleAsync(role);

            if (updatedRole == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
