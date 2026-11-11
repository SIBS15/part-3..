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

- YOUTUBE PRESENTATION
- https://youtu.be/wpKrt-LoAM4
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
4. Solution structure
RaceDay_Part2/
├── RaceDay.sln
├── RaceDay.Api/
│   ├── Controllers/
│   ├── Contracts/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
├── RaceDay.Mvc/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Views/
│   ├── wwwroot/
│   ├── Dockerfile
│   ├── Program.cs
│   └── appsettings.json
├── RaceDay.Api.Tests/
├── Part1_Reference/
├── docs/
└── .github/
    └── workflows/
        └── ci.yml
- RaceDay.Api: REST API, authentication/session handling, database access and Azure Blob Storage upload service.
- RaceDay.Mvc: web interface for Participants and Organisers.
- RaceDay.Api.Tests: automated API/model tests included with the project.
- Part1_Reference: database design and supporting Part 1 material.
- docs: Part 3 rubric checklist and evidence plan.
- .github/workflows: GitHub Actions CI workflow.
5. Requirements
Install the following software before running the project:
- Visual Studio 2022 with the ASP.NET and web development workload, or the .NET 8 SDK.
- .NET 8 SDK.
- SQL Server or SQL Server Express.
- SQL Server Management Studio (SSMS), recommended for database management.
- Docker Desktop with Linux containers and WSL 2, for the Docker demonstration.
- An Azure Storage account, if you want to test image uploads to Blob Storage.
- Git, if you want to use the GitHub workflow from your computer.
6. Configuration
6.1 SQL Server connection
Open RaceDay.Api/appsettings.json and update ConnectionStrings:DefaultConnection to match your SQL Server instance.

The included example uses SQL Server Express:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=RaceDayDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
For LocalDB, the server part can be:
Server=(localdb)\MSSQLLocalDB
Use the SQL Server instance that is actually installed on your computer. Do not commit database passwords or production credentials to GitHub.
6.2 Azure Blob Storage
The API reads its Blob Storage configuration from the AzureBlobStorage settings section. Keep the connection string secret; do not place a real connection string in a committed appsettings.json file.

From the solution directory, initialise User Secrets for the API and add your own Azure Storage connection string:
dotnet user-secrets init --project RaceDay.Api
dotnet user-secrets set "AzureBlobStorage:ConnectionString" "<YOUR AZURE STORAGE CONNECTION STRING>" --project RaceDay.Api
dotnet user-secrets set "AzureBlobStorage:ContainerName" "raceday-images" --project RaceDay.Api
Replace the placeholder with your own connection string. The Azure Storage account and container must exist or be created by the configured application process.
6.3 MVC API URL
The MVC application reads the API address from RaceDay.Mvc/appsettings.json:
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000/"
  }
}
For local development, this address should match the URL and port on which the API is running. When MVC runs in Docker while the API runs on the Windows host, use http://host.docker.internal:5000/ as the MVC API base URL.
7. Database setup
The API uses Entity Framework Core migrations.

Open a terminal in the folder containing RaceDay.sln, then run:
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet restore
dotnet ef database update --project RaceDay.Api
If the dotnet-ef tool is already installed, you do not need to install it again. Make sure the SQL Server connection string is correct before applying the migration.
8. Run the application locally
Step 1: Start the API
From the solution directory:
dotnet run --project RaceDay.Api --urls http://0.0.0.0:5000
In Development, Swagger should be available at:
http://localhost:5000/swagger
Step 2: Start the MVC application
Open a second terminal in the solution directory and run:
dotnet run --project RaceDay.Mvc
Open the local URL printed in the terminal. Ensure that RaceDay.Mvc/appsettings.json points to the running API.
Step 3: Test both user roles
1. Register a Participant account and sign in.
2. Browse events, open an event and enrol in an available category.
3. Check the Participant's event list and results page.
4. Register an Organiser account and sign in.
5. Create an event and its categories.
6. Upload an event banner if Azure Blob Storage is configured.
7. View enrolments and record a result.
8. Confirm that each role sees the relevant pages and actions.
Use test data only. Verify the actual API responses and database records during testing.
9. Docker: run the MVC application
The included Dockerfile builds and publishes the MVC application using .NET 8 images. The API is not included in this MVC image; it must be running separately and reachable by the MVC container.

Run these commands from the solution root (the directory containing RaceDay.sln):
Build the image
docker build -f RaceDay.Mvc/Dockerfile -t raceday-mvc:part3 .
Run the container
docker run -d --name raceday-mvc -p 8080:8080 -e ApiSettings__BaseUrl=http://host.docker.internal:5000/ raceday-mvc:part3
Open:
http://localhost:8080
Keep the API running on the host at port 5000. From inside the MVC container, host.docker.internal is used to reach a service running on the host computer. Using localhost inside the container would refer to the container itself.
Useful Docker commands
docker ps
docker logs raceday-mvc
docker stop raceday-mvc
docker rm -f raceday-mvc
After changing the MVC source code, rebuild the image and recreate the container:
docker rm -f raceday-mvc
docker build -f RaceDay.Mvc/Dockerfile -t raceday-mvc:part3 .
docker run -d --name raceday-mvc -p 8080:8080 -e ApiSettings__BaseUrl=http://host.docker.internal:5000/ raceday-mvc:part3
10. Testing
Run the following commands from the solution directory:
dotnet restore
dotnet build
dotnet test
The project includes an API test project. Before submission, also test the complete application manually, including:
- Successful and unsuccessful registration/login.
- Participant and Organiser access.
- Event creation, editing and deletion.
- Category creation and enrolment.
- Enrolment cancellation.
- Result capture and viewing.
- Profile and event image uploads.
- Image display after the API returns the stored image URL.
- MVC-to-API communication when MVC runs inside Docker.
A successful build alone does not prove that every feature works. Record the actual results of your tests and resolve any errors before submission.
11. GitHub and continuous integration
The repository includes a GitHub Actions workflow at:
.github/workflows/ci.yml
To push the project to your own GitHub repository, open a terminal in the solution directory and use:
git init
git add .
git commit -m "Initial RaceDay project"
git branch -M main
git remote add origin https://github.com/YOUR-USERNAME/YOUR-REPOSITORY.git
git push -u origin main
Replace the remote URL with the URL of your own repository. If Git is already initialised or a remote already exists, check the current configuration first:
git status
git remote -v
After pushing, open the repository's Actions tab and inspect the workflow result. Only report that CI passed if the workflow completed successfully.
12. Security and submission notes
- Do not commit Azure Storage connection strings, passwords, access tokens or other secrets.
- Use User Secrets or environment variables for local secret configuration.
- Test role restrictions and ownership rules with separate Participant and Organiser accounts.
- Add genuine screenshots of the running application, Swagger, Docker container and successful test/CI results where required by the assessment.
- Add the actual GitHub repository URL and any required demonstration-video link before submitting.
- Keep a meaningful Git commit history. Do not create false commits or claim tests were successful if they were not run.
13. Known verification limitation
This README describes the supplied source structure and its intended setup. The source archive has not been independently verified here by running it against your SQL Server database, Azure Storage account or Docker installation. You must run the build, migrations, tests, image upload and Docker steps on your own computer and document the actual outcomes.
14. Project status
The supplied Part 3 source contains the MVC front end, API integration, image-upload service, EF Core migrations, Dockerfile and CI workflow. Actual runtime success depends on the local database, Azure configuration, API URL, Docker environment and test execution.
