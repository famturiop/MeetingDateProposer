using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using MeetingDateProposer.Domain.Utilities;

namespace MeetingDateProposer.Tests.nUnit
{
    public class CalendarCalculatorTest
    {
        [Test]
        public void CalendarCalculatorTest1()
        {
            // Arrange
            Meeting Meeting1 = new Meeting();
            DateTime EvStart1 = new DateTime(2021, 6, 5);
            DateTime EvEnd1 = new DateTime(2021, 6, 7);
            DateTime EvStart2 = new DateTime(2021, 6, 5);
            DateTime EvEnd2 = new DateTime(2021, 6, 8);
            CalendarEvent Ev1 = new CalendarEvent() {EventStart = EvStart1, EventEnd = EvEnd1 };
            CalendarEvent Ev2 = new CalendarEvent() {EventStart = EvStart2, EventEnd = EvEnd2 };
            Calendar Calendar1 = new Calendar() {UserCalendar = new List<CalendarEvent>() {Ev1} };
            Calendar Calendar2 = new Calendar() {UserCalendar = new List<CalendarEvent>() {Ev2} };
            User User1 = new User() { Credentials = null, Calendars = new List<Calendar>() { Calendar1 }, Id = 1};
            User User2 = new User() { Credentials = null, Calendars = new List<Calendar>() { Calendar2 }, Id = 2};
            Meeting1.ConnectedUsers = new List<User>() { User1, User2 };
            CalendarEvent testEvent1 = new CalendarEvent() { EventStart = DateTime.MinValue, EventEnd = EvStart1 };
            CalendarEvent testEvent2 = new CalendarEvent() { EventStart = EvEnd2, EventEnd = DateTime.MaxValue };
            Calendar testCalendar = new Calendar() { UserCalendar = new List<CalendarEvent>() {testEvent1, testEvent2} };
            CalendarCalculator myCalenCalc = new CalendarCalculator();
            ///Act
            Calendar Result = myCalenCalc.CalculateAvailableMeetingTime(Meeting1);
            ///Assert
            Assert.True(ObjectEquivalence.CalendarCheck(Result, testCalendar));
        }
    }
}