using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.Models.AccountApiModels;
using MeetingDateProposer.Models.ApplicationApiModels;

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
        private readonly IMapper _mapper;

        public ApplicationUserController(IUserService userService, ICalendarProvider userCalendar, IMapper mapper)
        {
            _userService = userService;
            _userCalendar = userCalendar;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> GetUserByIdAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdFromDbAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userViewModel = _mapper.Map<ApplicationUserApiModel>(user);
            return Ok(userViewModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> CreateUserAsync(ApplicationUserApiModel userApiModel)
        {
            var user = _mapper.Map<ApplicationUser>(userApiModel);
            await _userService.AddUserToDbAsync(user);
            var userViewModel = _mapper.Map<ApplicationUserApiModel>(user);
            return Ok(userViewModel);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.RemoveUserFromDbAsync(userId);
            return Ok();
        }
    }
}
