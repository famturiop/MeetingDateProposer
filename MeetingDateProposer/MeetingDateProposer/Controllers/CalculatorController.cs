using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
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
    public class CalculatorController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ICalendarCalculator _calculator;
        private readonly IMapper _mapper;

        public CalculatorController(
            IMeetingService meetingService,
            ICalendarCalculator calculator, 
            IMapper mapper)
        {
            _meetingService = meetingService;
            _calculator = calculator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CalendarApiModel>> CalculateMeetingTimeAsync(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingAsync(meetingId);
            if (meeting == null)
                return NotFound();
            
            var calendar = _calculator.CalculateAvailableMeetingTime(meeting);
            var calendarApiModel = _mapper.Map<CalendarApiModel>(calendar);

            return Ok(calendarApiModel);
        }
    }
}
