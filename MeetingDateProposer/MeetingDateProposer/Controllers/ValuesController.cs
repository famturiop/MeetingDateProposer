using System;
using MeetingDateProposer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MeetingDateProposer.BusinessLayer.Providers;

namespace MeetingDateProposer.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly GoogleCalendar _tmpCalendar;
        private readonly User _user;

        public ValuesController()
        {
            _tmpCalendar = new GoogleCalendar();
            _user = new User();
        }

        [HttpGet]
        [Route("t")] // attributes? what do they do in our case?
        public void Test()
        {
            
            _user.UserId = 56;
            _tmpCalendar.GetCalendar(_user);
        }



        //[HttpGet]
        //[Route("api/Values/GetAllGooses")]
        //public List<Goose> GetAllGooses()
        //{
        //    return _gooseProvider.GetAllGooses();
        //}

        //[HttpGet]
        //[Route("api/Values/GetGooseCount")]
        //public int GetGooseCount()
        //{
        //    return _gooseProvider.GetGooseCount();
        //}

        //[HttpGet]
        //[Route("api/Values/SaveRandomGoose")]
        //public void SaveRandomGoose()
        //{
        //    _gooseProvider.SaveGoose(new Goose
        //    {
        //        Name = Guid.NewGuid().ToString()
        //    });
        //}
    }
}
