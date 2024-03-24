using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using MotoProject.Controllers;
using MotoProject.Data;
using MotoProject.Models;
using NUnit.Framework;
using System;
using System.Drawing;
using Moq;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task asd()
        {
            var mockContext = new Mock<MotoProjectContext>();
            var Motors = new List<Motors>
    {
        new Motors {Id = 1, brand = "BMW", model = "Boom", date = "11.12.2002" , hp = 999, color = "grey"}
    }.AsQueryable();


            var mockSet = new Mock<DbSet<Motors>>();
            mockSet.As<IQueryable<Motors>>().Setup(m => m.Provider).Returns(Motors.Provider);
            mockSet.As<IQueryable<Motors>>().Setup(m => m.Expression).Returns(Motors.Expression);
            mockSet.As<IQueryable<Motors>>().Setup(m => m.ElementType).Returns(Motors.ElementType);
            mockSet.As<IQueryable<Motors>>().Setup(m => m.GetEnumerator()).Returns(Motors.GetEnumerator());


            mockContext.Setup(m => m.Motors).Returns(mockSet.Object);

            var controller = new MotorsController(mockContext.Object);
            var result = await controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<Motors>;
            Assert.That(model.Count(), Is.EqualTo(1));
        }
    }
}