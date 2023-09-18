using dotnetapp.Controllers;
using dotnetapp.Exceptions;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BookingControllerTests
    {
        private ApplicationDbContext _context;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            // Set up the test database context
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            _controller = new BookingController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the test database context
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void BatchEnrollmentForm_ValidBatchId_ReturnsView()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 5 };

            // var batch = new List<Batch>
            // {
            //     new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 5 },
            //     new Batch { BatchID = 2, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3), Capacity = 5 }
            // };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            // Act
            var result = _controller.BatchEnrollmentForm(batch.BatchID) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(batch, result.Model);
        }

        [Test]
        public void BatchEnrollmentForm_InvalidBatchId_ReturnsNotFound()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 5 };

            // Act
            var result = _controller.BatchEnrollmentForm(batch.BatchID) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BatchEnrollmentForm_ValidData_CreatesStudentAndRedirects()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 1 };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            // Act
            var result = _controller.BatchEnrollmentForm(batch.BatchID, "John Doe", "john@example.com") as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

            // // Check if the student was created and added to the database
            // var student = _context.Students.SingleOrDefault(s => s.BatchID == batch.BatchID);
            // Assert.IsNotNull(student);
            // Assert.AreEqual("John Doe", student.Name);
            // Assert.AreEqual("john@example.com", student.Email);
        }

        [Test]
        public void BatchEnrollmentForm_ValidData_CreatesStudent()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 1 };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            // Act
            var result = _controller.BatchEnrollmentForm(batch.BatchID, "John Doe", "john@example.com") as RedirectToActionResult;

            // Assert
            // Assert.IsNotNull(result);
            // Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

            // // Check if the student was created and added to the database
            var student = _context.Students.SingleOrDefault(s => s.BatchID == batch.BatchID);
            Assert.IsNotNull(student);
            Assert.AreEqual("John Doe", student.Name);
            Assert.AreEqual("john@example.com", student.Email);
        }

        [Test]
        public void BatchEnrollmentForm_BatchFull_ThrowsException()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 0 };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            // Assert
            Assert.Throws<FrenchTuitionBookingException>(() =>
            {
                // Act
                _controller.BatchEnrollmentForm(batch.BatchID, "John Doe", "john@example.com");
            });
        }

        [Test]
        public void BatchEnrollmentForm_BatchFull_ThrowsException_with_message()
        {
            // Arrange
            var batch = new Batch { BatchID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Capacity = 0 };
            _context.Batches.Add(batch);
            _context.SaveChanges();

            var exception=Assert.Throws<FrenchTuitionBookingException>(() => _controller.BatchEnrollmentForm(batch.BatchID, "John Doe", "john@example.com"));

            Assert.AreEqual("Maximum number reached", exception.Message);
        }

    

        [Test]
        public void EnrollmentConfirmation_NonexistentStudentId_ReturnsNotFound()
        {
            // Arrange
            var studentId = 1;

            // Act
            var result = _controller.EnrollmentConfirmation(studentId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        
        [Test]
        public void StudentClassExists()
        {
            var student = new Student();
        
            Assert.IsNotNull(student);
        }
        
        [Test]
        public void BatchClassExists()
        {
            var batch = new Batch();
        
            Assert.IsNotNull(batch);
        }
        
        [Test]
        public void ApplicationDbContextContainsDbSetBatchProperty()
        {
            // using (var context = new ApplicationDbContext(_dbContextOptions))
            //         {
            // var context = new ApplicationDbContext();
        
            var propertyInfo = _context.GetType().GetProperty("Batches");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Batch>), propertyInfo.PropertyType);
                    // }
        }
        
        [Test]
        public void ApplicationDbContextContainsDbSetStudentProperty()
        {
            // using (var context = new ApplicationDbContext(_dbContextOptions))
            //         {
            // var context = new ApplicationDbContext();
        
            var propertyInfo = _context.GetType().GetProperty("Students");
        
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Student>), propertyInfo.PropertyType);
        }
        [Test]
        public void Batch_Properties_BatchID_ReturnExpectedDataTypes()
        {
            // Arrange
            Batch batch = new Batch();
            Assert.That(batch.BatchID, Is.TypeOf<int>());
        }
        [Test]
        public void Batch_Properties_StartTime_ReturnExpectedDataTypes()
        {
            // Arrange
            Batch batch = new Batch();
            Assert.That(batch.StartTime, Is.TypeOf<DateTime>());
        }
        [Test]
        public void Batch_Properties_EndTime_ReturnExpectedDataTypes()
        {
            // Arrange
            Batch batch = new Batch();            
            Assert.That(batch.EndTime, Is.TypeOf<DateTime>());
        }

        [Test]
        public void Batch_Properties_Capacity_ReturnExpectedDataTypes()
        {
            // Arrange
            Batch batch = new Batch();
            Assert.That(batch.Capacity, Is.TypeOf<int>());
        }

        [Test]
        public void Batch_Properties_BatchID_ReturnExpectedValues()
        {
            // Arrange
            int expectedBatchID = 1;

            Batch batch = new Batch
            {
                BatchID = expectedBatchID
            };
            Assert.AreEqual(expectedBatchID, batch.BatchID);
        }

        [Test]
        public void Batch_Properties_StartTime_ReturnExpectedValues()
        {
            DateTime expectedStartTime = new DateTime(2023, 7, 1, 9, 0, 0);

            Batch batch = new Batch
            {
                StartTime = expectedStartTime
            };
            Assert.AreEqual(expectedStartTime, batch.StartTime);
        }

        [Test]
        public void Batch_Properties_EndTime_ReturnExpectedValues()
        {
            DateTime expectedEndTime = new DateTime(2023, 7, 1, 10, 30, 0);

            Batch batch = new Batch
            {
                EndTime = expectedEndTime
            };
            Assert.AreEqual(expectedEndTime, batch.EndTime);
        }

        [Test]
        public void Batch_Properties_Capacity_ReturnExpectedValues()
        {
            int expectedCapacity = 20;
            Batch batch = new Batch
            {
                Capacity = expectedCapacity
            };
            Assert.AreEqual(expectedCapacity, batch.Capacity);
        }

        [Test]
        public void Student_Properties_StudentID_ReturnExpectedDataTypes()
        {
            // Arrange
            Student student = new Student();
            Assert.That(student.StudentID, Is.TypeOf<int>());
        }
        [Test]
        public void Student_Properties_Name_ReturnExpectedDataTypes()
        {
            Student student = new Student();
            student.Name = "";
            Assert.That(student.Name, Is.TypeOf<string>());
        }

        [Test]
        public void Student_Properties_Email_ReturnExpectedDataTypes()
        {
            // Arrange
            Student student = new Student();
            student.Email = "";
            Assert.That(student.Email, Is.TypeOf<string>());
        }
        [Test]
        public void Student_Properties_BatchID_ReturnExpectedDataTypes()
        {
            // Arrange
            Student student = new Student();
            Assert.That(student.BatchID, Is.TypeOf<int>());
        }

        [Test]
        public void Student_Properties_Email_ReturnExpectedValues()
        {
            string expectedEmail = "john@example.com";

            Student student = new Student
            {
                Email = expectedEmail
            };
            Assert.AreEqual(expectedEmail, student.Email);
        }
        [Test]
        public void Student_Properties_Returns_Batch_ExpectedValues()
        {
            Batch expectedBatch = new Batch();

            Student student = new Student
            {
                Batch = expectedBatch
            };
            Assert.AreEqual(expectedBatch, student.Batch);
        }


    }
}
