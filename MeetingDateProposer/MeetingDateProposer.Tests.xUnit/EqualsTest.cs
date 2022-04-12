using System;
using System.Collections.Generic;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Xunit;

namespace MeetingDateProposer.Tests.xUnit
{
    public class EqualsTest
    {
        [Fact]
        public void MeetingEqualsTest()
        {
            var meeting0 = new Meeting();
            var meeting = new Meeting
            {
                ConnectedUsers = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>(),
                        Credentials = null,
                        Id = Guid.NewGuid()
                    },
                    new ApplicationUser
                    {
                        Calendars = new List<Calendar>(),
                        Credentials = null,
                        Id = Guid.NewGuid()
                    }
                },
                Id = Guid.NewGuid()
            };
            try
            {
                var tmp = meeting.Equals(meeting0);
                Assert.False(tmp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}