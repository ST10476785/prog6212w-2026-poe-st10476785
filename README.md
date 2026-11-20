RaceDay API - Formative 2 Part 2

Features & Controllers

This API uses BCrypt to securely hash and verify user passwords during the authentication process, alongside JWT (JSON Web Tokens) for securing endpoints.

CONTROLLERS

AuthController: Handles secure user registration and login/token generation

ActivityController: Manages the creation and retrieval of race events.

CategoryController: Manages the different categories of events.

EnrollmentController: Handles users registering to participate in specific events

ResultsController: Manages the results for events

UserController: Manages user profile data and roles

MODELS

Defines the data entities and relationships (such as users, events, categories, enrollments, and results) used by Entity Framework Core and the database.

MVC & UI

Includes RaceDayMVC, providing the web UI and views for interacting with events, categories, enrollments, and results.

WEB PAGE & HOW TO USE IT

Launch the web app (RaceDayMVC) in your browser to access the navigation bar (Home, Events, Categories, Enrollments, Results, and account login/registration). Register or log in first, then use the pages to create and edit race events (including banner images), add event categories, enroll users into categories using valid IDs, and record or update race results with valid enrollment IDs, finishing times, and finishing positions.

SERVER SETUP

SQL Server or SQL Server Express

Database Setup -

Before running the API, you must update the database connection string to match your local SQL Server environment.

Open appsettings.json in the root of the main project.

Locate the ConnectionStrings section.

Modify the Server= value to match your personal SQL Server instance name (e.g., Server=localhost\SQLEXPRESS, Server=YOUR-PC-NAME, or Server=(localdb)\MSSQLLocalDB).
This must be done in order for the database to function

Save the file.

Applying Migrations

To generate the local database and tables, open the Package Manager Console in Visual Studio and execute:

Update-Database

RUNNING THE APPLICATION

Press F5 in Visual Studio to launch the API.

Use the given local host value and add /swagger in your browser

Navigate to the /api/Auth/register endpoint to create a test account, then use /api/Auth/login to generate a JWT token.

Click the Authorize padlock icon at the top of the Swagger page  paste your token and click Authorize to unlock the secure endpoints.

UNIT TESTS

This project includes a test suite utilizing xUnit and an InMemory database.

In Visual Studio, navigate to Test > Test Explorer and click Run All Tests.

REFERENCES FOR THE PROJECT
// Code A Future.2025. How to Consume APIs in ASP.NET Core MVC (Step by Step Project). [video online] Available at <https://www.youtube.com/watch?v=oM568qeQ0wE&t=875s> [Accessed 18 November 2026].
// CodeWithGopi.2023. How to Consume Web Api in ASP.NET Core MVC | Read Data from ASP.NET Web API. [video online] Available at <https://www.youtube.com/watch?v=_TgiJR13cT4&t=547s> [Accessed 18 November 2026].

God,P, 2023 . .NET 7 WEB API 🔒 Create JSON Web Tokens (JWT) - User Registration / Login / Authentication. [video online] Available at https://www.youtube.com/watch?v=UwruwHl3BlU [Accessed 24 October 2026].
Saini,S, 2024. ASP.NET Web API CRUD Operations-.NET8 and Entity Framework Core Tutorial. [video online] Available at https://www.youtube.com/watch?v=6YIRKBsRWVI&t=2009s  [Accessed 22 October 2026].
The Independent Institute of Education, 2025. The IIE Harvard Anglia Style Reference  Guide. [pdf] Available athttps://www.coursehero.com/file/247795947/The-IIE-Harvard [Accessed 26 October 2026].
