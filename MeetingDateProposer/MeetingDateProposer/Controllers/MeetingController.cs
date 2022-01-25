using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.Domain.Models.AccountModels;
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
        private readonly IMapper _mapper;

        public MeetingController(IMeetingService meetingService, IMapper mapper)
        {
            _meetingService = meetingService;
            _mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<MeetingApiModel>> GetMeetingByIdAsync(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }

            var meetingViewModel = _mapper.Map<MeetingApiModel>(meeting);
            return Ok(meetingViewModel);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public async Task<ActionResult<MeetingApiModel>> CreateMeetingAsync(MeetingApiModel meetingApiModel)
        {
            var meeting = _mapper.Map<Meeting>(meetingApiModel);
            await _meetingService.AddMeetingToDbAsync(meeting);
            var meetingViewModel = _mapper.Map<MeetingApiModel>(meeting);
            return CreatedAtAction(nameof(GetMeetingByIdAsync), new { meetingId = meeting.Id }, meetingViewModel);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteMeetingAsync(Guid meetingId)
        {
            await _meetingService.DeleteMeetingAsync(meetingId);
            return Ok();
        }
    }
}
