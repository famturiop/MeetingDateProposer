using AutoMapper;
using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Models.ApplicationApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalendarCalculator _calculator;
        private readonly IMapper _mapper;

        public CalculatorController(
            ICalendarCalculator calculator,
            IMapper mapper)
        {
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
