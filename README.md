# MeetingDateProposer

[![Deploy container to Azure](https://github.com/famturiop/MeetingDateProposer/actions/workflows/azure-deploy-workflow.yaml/badge.svg)](https://github.com/famturiop/MeetingDateProposer/actions/workflows/azure-deploy-workflow.yaml)
[![Build and Test](https://github.com/famturiop/MeetingDateProposer/actions/workflows/build-and-test-workflow.yaml/badge.svg)](https://github.com/famturiop/MeetingDateProposer/actions/workflows/build-and-test-workflow.yaml)
[![codecov](https://codecov.io/gh/famturiop/MeetingDateProposer/branch/develop/graph/badge.svg?token=5KEPOQ7GZV)](https://codecov.io/gh/famturiop/MeetingDateProposer)


## About

This webapp creates a meeting that compares participants' calendars and displays their common available time. The purpose of the app is to provide a simple, convinient and fast way to schedule a meeting, without registering an account and without excessive amount of settings. Only Google calendar is supported now, Outlook calendar and manual calendar input are planned to be supported in the future.

The app is currently in development. It is being made using Angular for ClientApp SPA, ASP.NET Core for backend WebApi, Microsoft Sql Server for database. The main functionality has been done, and it's example is presented in the section below.

Current version of the app is available at https://meetingdateproposer.azurewebsites.net

## Features

- Registration is not required to use the app
- Very simple and straightforward calendar view
- Only Google Calendar is supported at the moment
- If the meeting has a lot of participants, you may choose which participant's calendars to compare and exclude very busy participants from the meeting

## The webapp usage example

>1. You message to your friend with whom you want to meet.

<img src="https://i.imgur.com/xjph9Zf.png" style="border: solid">
<br><br>

>2. You create the meeting in the app. No account registration required.

<img src="https://i.imgur.com/J9Y4lmc.png" style="border: solid" width=500px>
<br><br>

>3. You add yourself to the meeting, and give the app a readonly access to your calendar.

<img src="https://i.imgur.com/u7Kbvi2.png" style="border: solid" width=500px>
<br><br>

>4. You share the meeting link with your friend.

<img src="https://i.imgur.com/EIzjJHw.png" style="border: solid">
<br><br>

>5. Your friend uses the link to add himself to the meeting and gives the app access to his calendar.

<img src="https://i.imgur.com/pfiYUVY.png" style="border: solid" width=500px>
<br><br>

>6. The app compares both calendars and presents you with a very simple view of time when both of you have spare time in your calendars and are free to meet.

<img src="https://i.imgur.com/IjXHVRr.png" style="border: solid" width=500px>
<br><br>

>7. You and your friend arrange the exact meeting time via a chosen communication service using the information provided by the app.

<img src="https://i.imgur.com/UePZheW.png" style="border: solid">
<br><br>

## Technologies and Packages

- **SDK:** `.NET 6`
- **Frameworks:** `ASP .NET`, `Angular`
- **Persistence:**
    - Database: `MS SQL Server 15`
    - ORM: `Entity Framework Core 6.0`
- **Authentication and Authorization:** `ASP .NET Core Identity`
- **Business Logic:**
    - `AutoMapper`
    - `Google.Apis.Calendar.v3 1.57`
- **Presentation:**
    - Frontend Development: `Angular 14`
    - Calendar view: `angular-calendar 0.29`
- **Unit Testing:** `XUnit`
- **Containerization:** `Docker`
- **Continuous Delivery:** `GitHub Actions`, `Azure`

## Installation guide

To build and run the project you would need the following:
- [Git](https://git-scm.com/download)
- [.Net 6.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [NodeJs](https://nodejs.org/en/) (Latest LTS)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

1. Clone the repo:

   `git clone https://github.com/famturiop/MeetingDateProposer.git`

2. Change directory:

   `cd MeetingDateProposer/MeetingDateProposer`

3. Restore packages:

   `dotnet restore MeetingDateProposer.sln`

4. Install npm packages:

   - `cd MeetingDateProposer/ClientApp`
   - `npm install`

5. Set the following enviroment variables:

   - `ConnectionStrings__DefaultConnection`: Database connection string in MsSQL server format
   - `SeededUsers__Admin__Name`: Admin username
   - `SeededUsers__Admin__Email`: Admin email
   - `SeededUsers__Admin__Password`: Admin password
   - `Calendars__GoogleCalendar__ClientId` and `Calendars__GoogleCalendar__ClientSecret`:
   Google OAuth client ID credentials that are used to authenticate as an end user and access their data after said user gave his consent (You can learn how to get them [here](https://developers.google.com/workspace/guides/get-started))

6. If you are running for the first time, install dev certificates using command:
   
   `dotnet dev-certs https --trust`

7. Run the project:

   - `cd ..`
   - `dotnet run` 

