namespace Junto.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Junto.Domain;
    using Junto.Domain.Model;
    using Junto.Domain.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.JsonWebTokens;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _objIUserService;

        public UserController(IUserService objIUserService)
        {
            _objIUserService = objIUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _objIUserService.GetAllAsNoTrackingAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _objIUserService.GetByIdAsNoTrackingAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return Ok(await _objIUserService.CreateAsync(user));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            return Ok(await _objIUserService.UpdateAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _objIUserService.RemoveAsync(id);
            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> PostToken([FromBody] User user)
        {
            return Ok(await _objIUserService.GenerateTokenAsync(user));
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> PostChangePassword([FromBody] ChangePasswordDto objDto)
        {
            var id = long.Parse(User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value);

            await _objIUserService.ChangePasswordAsync(id, objDto);

            return Ok();
        }
    }
}