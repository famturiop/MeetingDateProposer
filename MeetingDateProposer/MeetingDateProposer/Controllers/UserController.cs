using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DatabaseServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userProvider;
        private readonly ICalendarProvider _userCalendar;

        public UserController(IUserService userProvider, ICalendarProvider userCalendar)
        {
            _userProvider = userProvider;
            _userCalendar = userCalendar;
        }


        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> GetUserById(Guid userId)
        {
            var user = _userProvider.GetUserbyIdFromDb(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> CreateUser(string name)
        {
            User user = new User { Name = name };
            _userCalendar.GetCalendar(user);
            _userProvider.AddUserToDb(user);
            return Ok(user);
        }

    }
}
