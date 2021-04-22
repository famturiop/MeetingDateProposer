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
        private readonly GoogleCalendar tmpCalendar;
        private readonly User user;

        public ValuesController()
        {
            tmpCalendar = new GoogleCalendar();
        }

        [HttpGet]
        [Route("T")]
        public void Test()
        {
            tmpCalendar.GetCalendar(user);
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
