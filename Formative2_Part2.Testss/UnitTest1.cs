using Formative2_Part2.Controllers;
using Formative2_Part2.Data;
using Formative2_Part2.DTOs;
using Formative2_Part2.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Xunit;

namespace Formative2_Part2.Testss
{
    public class RaceDayTests
    {
        private ApplicationDBcontext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBcontext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDBcontext(options);
        }

        private IConfiguration GetTestConfiguration()
        {
            
            var inMemorySettings = new Dictionary<string, string?> { //  authorizatio test for JWT token generation
                {"Jwt:Key", "ThisIsAVerySecretKeyForTestingPurposes1234567890"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"}
            };
            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        //   User Registration Test
        [Fact]
        public void Register_ValidRequest_ReturnsOkAndSavesUser()
        {
            var context = GetInMemoryDbContext();
            var config = GetTestConfiguration();
            var controller = new AuthController(context, config);

            var request = new UserRegisterDto
            {
                Name = "Merlin",
                Surname = "Test",
                Email = "merlin@test.com",
                Password = "password123",
                UserTypeId = 2
            };

            var result = controller.Register(request);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, context.Users.Count());
        }

                //  Event Management Test
        [Fact]
        public void AddNewEvent_ValidData_SavesEventToDatabase()
        {
            var context = GetInMemoryDbContext();
            var controller = new ActivityController(context); 

            var newEvent = new Event
            {
                EventName = "5K Run",
                Location = "Campus",
                EventType = "Running", 
                Distance = 5
            };

            var result = controller.AddNewEvent(newEvent); 

            Assert.Equal(1, context.Events.Count());
        }

        // Authorization Test
        [Fact]
        public void AddNewEvent_RequiresAdminRole_RejectsUnauthorizedUsers()
        {
            var methodInfo = typeof(ActivityController).GetMethod("AddNewEvent");
            var authorizeAttribute = methodInfo?.GetCustomAttribute<AuthorizeAttribute>();

            Assert.NotNull(authorizeAttribute);
            Assert.Equal("1", authorizeAttribute.Roles);
        }

        //  Enrollment Test
        [Fact]
        public void AddNewEnrollment_ValidParticipant_RecordsEventEnrollment()
        {
            var context = GetInMemoryDbContext();
            var controller = new EnrollmentController(context); 

            var enrollment = new EventEnrollment
            {
                Status = "Registered", 
                UserId = 1,
                CategoryId = 1 
            };

            var result = controller.AddNewEnrollment(enrollment); 

            Assert.Equal(1, context.EventEnrollments.Count());
        }
    }
}