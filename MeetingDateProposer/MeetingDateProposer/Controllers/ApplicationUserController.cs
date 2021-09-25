using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICalendarProvider _userCalendar;

        public ApplicationUserController(IUserService userService, ICalendarProvider userCalendar)
        {
            _userService = userService;
            _userCalendar = userCalendar;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<ApplicationUser> GetUserById(Guid userId)
        {
            var user = _userService.GetUserByIdFromDb(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<ApplicationUser> CreateUser(string name)
        {
            ApplicationUser user = new ApplicationUser { Name = name };
            _userCalendar.GetCalendar(user);
            _userService.AddUserToDb(user);
            return Ok(user);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApplicationUser> DeleteUser(Guid userId)
        {
            _userService.RemoveUserFromDb(userId);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            return Ok();
        }
    }
}
