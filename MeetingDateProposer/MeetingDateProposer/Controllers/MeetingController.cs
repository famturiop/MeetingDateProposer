using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<Meeting>> GetMeetingByIdAsync(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> CreateMeetingAsync(string name)
        {
            Meeting meeting = new Meeting {Name = name};
            await _meetingService.AddMeetingToDbAsync(meeting);
            return CreatedAtAction(nameof(GetMeetingByIdAsync), new {meetingId = meeting.Id}, meeting);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApplicationUser>> DeleteMeetingAsync(Guid meetingId)
        {
             await _meetingService.DeleteMeetingAsync(meetingId);
             return Ok();

        }
    }
}
