using MeetingDateProposer.BusinessLayer.DbInteractionServices;
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
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MeetingController(
            IMeetingService meetingService, 
            IMapper mapper, 
            IUserService userService)
        {
            _meetingService = meetingService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ActionName("GetMeetingById")]
        [AllowAnonymous]
        public async Task<ActionResult<MeetingApiModel>> GetMeetingById(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingAsync(meetingId);
            if (meeting == null)
                return NotFound();

            var meetingApiModel = _mapper.Map<MeetingApiModel>(meeting);

            return Ok(meetingApiModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<MeetingApiModel>> CreateMeeting(
            MeetingApiModel meetingApiModel)
        {
            var meeting = _mapper.Map<Meeting>(meetingApiModel);
            await _meetingService.CreateMeetingAsync(meeting);
            meetingApiModel = _mapper.Map<MeetingApiModel>(meeting);

            return CreatedAtAction(nameof(GetMeetingById), new { meetingId = meeting.Id }, meetingApiModel);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<MeetingApiModel>> AddUserToMeeting(
            Guid meetingId,
            ApplicationUserApiModel applicationUserApiModel)
        {
            var meeting = await _meetingService.GetMeetingAsync(meetingId);
            var userFromBody = _mapper.Map<ApplicationUser>(applicationUserApiModel);
            var user = await _userService.GetUserAsync(userFromBody.Id);

            if (meeting == null)
                return NotFound();

            if (meeting.ConnectedUsers.Exists(u => u.Id == userFromBody.Id))
                return BadRequest();

            if (user == null)
            {
                await _userService.CreateUserAsync(userFromBody);
                user = userFromBody;
            }

            await _meetingService.AddUserToMeetingAsync(user, meeting);
            var meetingApiModel = _mapper.Map<MeetingApiModel>(meeting);

            return Ok(meetingApiModel);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteMeeting(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingAsync(meetingId);
            if (meeting == null)
                return NotFound();

            await _meetingService.DeleteMeetingAsync(meetingId);

            return NoContent();
        }
    }
}
