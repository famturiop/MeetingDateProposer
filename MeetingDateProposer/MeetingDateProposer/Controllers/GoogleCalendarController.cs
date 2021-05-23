using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;


namespace MeetingDateProposer.Controllers
{
    [ApiController]
    public class GoogleCalendarController : ControllerBase
    {

        private readonly GoogleCalendarProvider _googleCalendar;
        private readonly User _user;

        public GoogleCalendarController()
        {
            _googleCalendar = new GoogleCalendarProvider();
            _user = new User();
        }

        [HttpGet]
        [Route("t")]
        public void Test()
        {
            _user.Id = 56;
            _user.Calendars = new List<Calendar>();
            _googleCalendar.GetCalendar(_user);
        }
    }
}
