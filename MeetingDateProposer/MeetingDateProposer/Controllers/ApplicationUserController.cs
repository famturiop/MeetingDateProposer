using AutoMapper;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Models.AccountApiModels;
using MeetingDateProposer.Models.ApplicationApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ApplicationUserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
                return NotFound();

            var userApiModel = _mapper.Map<ApplicationUserApiModel>(user);

            return Ok(userApiModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> CreateUser(
            ApplicationUserApiModel userApiModel)
        {
            var user = _mapper.Map<ApplicationUser>(userApiModel);
            await _userService.CreateUserAsync(user);
            userApiModel = _mapper.Map<ApplicationUserApiModel>(user);

            return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, userApiModel);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
                return NotFound();

            await _userService.DeleteUserAsync(userId);

            return Ok();
        }
    }
}
