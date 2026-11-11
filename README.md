RaceDay Event Management System
Project: PROG6212 Portfolio of Evidence – Part 3
Platform: ASP.NET Core MVC, ASP.NET Core Web API, SQL Server, Entity Framework Core and Azure Blob Storage
1. Project overview
RaceDay is an event management system designed for road-running, walking and cycling events. It provides a web interface for participants to discover events, enrol in events and view their results. Event organisers can manage their events, create event categories, view participant enrolments and record race results.

The solution uses a separate MVC application and REST API. The MVC application communicates with the API through an API client. The API handles database operations and image uploads.
2. Main features
Participants
- Register and log in.
- Browse and filter available events.
- View event details and categories.
- Enrol in an event and view personal enrolments.
- Cancel an enrolment.
- View personal race results.
- Update profile information and upload a profile image.
Organisers
- Register and log in with the Organiser role.
- View the organiser dashboard.
- Create, edit and delete events.
- Upload event banner images.
- Create and manage event categories.
- View enrolments for their events.
- Capture race results.
System functionality
- Role-based navigation for Organisers and Participants.
- Session-based authentication between the MVC application and API.
- SQL Server data storage through Entity Framework Core.
- Azure Blob Storage integration for event banners and profile images.
- Swagger documentation for API testing in the Development environment.
- GitHub Actions workflow for restoring and building the solution, running API tests and building the MVC Docker image.
3. Technology stack
Technology	Purpose
C#	Main programming language
.NET 8 / ASP.NET Core	Web application and REST API framework
ASP.NET Core MVC	User interface
ASP.NET Core Web API	Application endpoints and business operations
Entity Framework Core	Database access and migrations
SQL Server	Relational database
Azure Blob Storage	Storage for uploaded images
Swagger / OpenAPI	API exploration and testing
Docker	Containerisation of the MVC application
GitHub Actions	Continuous integration workflow
