using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.Models.ApplicationApiModels;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class UserMeetingInteractionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMeetingService _meetingService;
        private readonly ICalendarCalculator _calculator;
        private readonly IMapper _mapper;

        public UserMeetingInteractionController(
            IUserService userService,
            IMeetingService meetingService,
            ICalendarCalculator calculator, IMapper mapper)
        {
            _userService = userService;
            _meetingService = meetingService;
            _calculator = calculator;
            _mapper = mapper;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MeetingApiModel>> UpdateMeetingAsync(
            Guid meetingId, 
            Guid userId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            var user = await _userService.GetUserByIdFromDbAsync(userId);
            if (meeting == null || user == null)
            {
                return NotFound();
            }
            await _meetingService.AddUserToMeetingAsync(user, meeting);
            var meetingViewModel = _mapper.Map<MeetingApiModel>(meeting);
            return Ok(meetingViewModel);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CalendarApiModel>> CalculateMeetingTimeAsync(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            var calendar = _calculator.CalculateAvailableMeetingTime(meeting);
            var calendarViewModel = _mapper.Map<CalendarApiModel>(calendar);
            return Ok(calendarViewModel);
        }
    }
}
