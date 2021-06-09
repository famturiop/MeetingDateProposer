using System;
using MeetingDateProposer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MeetingDateProposer.BusinessLayer.Providers;

namespace MeetingDateProposer.Controllers
{
    public class DbController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly User _user;

        public DbController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
            _user = new User();
        }

        [HttpGet]
        [Route("d")]
        public void Test()
        {
            //_user.Id = 1;
            _user.Credentials = null;
            _user.Calendars = new List<Calendar>()
            {
                new Calendar()
                {
                    //Id = 1,
                    //UserId = 1,
                    UserCalendar = new List<CalendarEvent>()
                    {
                        new CalendarEvent()
                        {
                            //Id = 1,
                            //EventStart = DateTime.MinValue,
                            //EventEnd = DateTime.MaxValue
                        }
                    }
                }
            };
            
            _userProvider.AddUserToDb(_user);
        }
    }
}
