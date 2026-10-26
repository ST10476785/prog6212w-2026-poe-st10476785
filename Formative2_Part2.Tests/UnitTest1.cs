using Formative2_Part2.Controllers;
using Formative2_Part2.Data;
using Formative2_Part2.DTOs;
using Formative2_Part2.Entities;
// Add your specific model and DTO using statements here (e.g., using Formative2_Part2.Models;)
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Xunit;

namespace Formative2_Part2.Tests
{
    public class RaceDayTests
    {
        // Generates a fresh In-Memory database for every test to prevent data leakage
        private ApplicationDBcontext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBcontext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDBcontext(options);
        }

        // 1. User Registration Test
        [Fact]
        public void Register_ValidRequest_ReturnsOkAndSavesUser()
        {
            var context = GetInMemoryDbContext();
            var controller = new AuthController(context);
            var request = new UserRegisterDto
            {
                Name = "Merlin",
                Email = "test@test.com",
                Password = "password123",
                UserTypeId = 2
            };

            var result = controller.Register(request);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, context.Users.Count());
        }

        // 2. Event Management Test
        [Fact]
        public void CreateEvent_ValidData_SavesEventToDatabase()
        {
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);
            var newEvent = new Event
            {
                EventName = "5K Run",
                Distance = 5,
                Location = "Campus"
            };

            var result = controller.PostEvent(newEvent);

            Assert.Equal(1, context.Events.Count());
        }

        // 3. Role-Based Access Control Test
        [Fact]
        public void CreateEvent_RequiresAdminRole_RejectsUnauthorizedUsers()
        {
            var methodInfo = typeof(EventsController).GetMethod("PostEvent");
            var authorizeAttribute = methodInfo.GetCustomAttribute<AuthorizeAttribute>();

            Assert.NotNull(authorizeAttribute);
            Assert.Equal("1", authorizeAttribute.Roles);
        }

        // 4. Enrollment Test
        [Fact]
        public void Enroll_ValidParticipant_RecordsEnrollment()
        {
            var context = GetInMemoryDbContext();
            var controller = new EnrollmentsController(context);
            var enrollment = new Enrollment
            {
                UserId = 1,
                EventId = 1
            };

            var result = controller.CreateEnrollment(enrollment);

            Assert.Equal(1, context.Enrollments.Count());
        }
    }
}