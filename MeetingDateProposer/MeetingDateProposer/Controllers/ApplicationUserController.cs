using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
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
        private readonly ICalendarProvider _userCalendar;

        public ApplicationUserController(IUserService userService, ICalendarProvider userCalendar)
        {
            _userService = userService;
            _userCalendar = userCalendar;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdFromDbAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> CreateUserAsync(string name)
        {
            var user = new ApplicationUser
            {
                Name = name
            };
            _userCalendar.GetCalendar(user);
            await _userService.AddUserToDbAsync(user);
            return Ok(user);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApplicationUser>> DeleteUserAsync(Guid userId)
        {
            await _userService.RemoveUserFromDbAsync(userId);
            return Ok();
        }
    }
}
