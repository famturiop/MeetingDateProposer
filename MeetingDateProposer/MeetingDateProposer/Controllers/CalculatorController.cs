using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.Domain.Models.ApplicationModels;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CalendarApiModel> CalculateMeetingTime(List<CalendarApiModel> calendarsApiModel)
        {
            var calendars = new List<Calendar>();

            foreach (var calendarModel in calendarsApiModel)
            {
                calendars.Add(_mapper.Map<Calendar>(calendarModel));
            }

            var calendar = _calculator.CalculateAvailableMeetingTime(calendars);
            var calendarApiModel = _mapper.Map<CalendarApiModel>(calendar);

            return Ok(calendarApiModel);
        }
    }
}
