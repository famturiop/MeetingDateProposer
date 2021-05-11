//using Microsoft.AspNetCore.Mvc;
//using UnitTestApp.Controllers;
using Xunit;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Tests.Utilities;

namespace UnitTestApp.Tests
{
    public class MyTest
    {
        [Fact]
        public void MyTestName()
        {
            // Arrange
            Meeting myMeeting = new Meeting();
            var myCalendar = new Calendar(); 
            MeetingGenerator tmp = new MeetingGenerator();

            // Act
            myMeeting = tmp.GenerateMeeting(5, 5);
            myCalendar = tmp.GenerateCalendar(5);

            // Assert
            Assert.True(myCalendar.UserCalendar.Count == 5);
            Assert.True(myMeeting.ConnectedUsers.Count == 5);
        }

    }
}