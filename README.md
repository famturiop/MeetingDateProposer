<h2 align="center">About</h2>

This webapp creates a meeting that compares participants calendars and displays their common available time. The purpose of the app is to provide a simple, convinient and fast way to schedule a meeting, without registering an account and without excessive amount of settings. Only Google calendar is supported now, Outlook calendar and manual calendar input are planned to be supported in the future.

The app is currently in development. It is being made using Angular for ClientApp SPA, ASP.NET Core for backend WebApi, Microsoft Sql Server for database. The main functionality is done, and it's example is presented in the section below.

<h2 align="center">How it works</h2>

The basic example of a webapp usage.

1. A user creates a meeting. No account registration required.

<img src="https://i.imgur.com/DwkSxw2.png">

2. A user adds himself to the meeting.

<img src="https://i.imgur.com/iodCgYF.png">

3. A user gives the app access to his calendar.

<img src="https://i.imgur.com/FAtvD7T.png">

4. A user shares the meeting link with his colleague, with whom he is planning to meet.

<img src="https://i.imgur.com/sxYvBzV.png">

5. The user's colleague uses the link to add himself to the meeting and gives the app access to his calendar in the same way as in steps 2 and 3.

<img src="https://i.imgur.com/A1gHmF9.png">

6. The app provides a very simple view of time when both user and his colleague have spare time in their calendars and are free to meet.

<img src="https://i.imgur.com/fEkGX5y.png">