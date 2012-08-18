using System.Web.Mvc;
using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactManager.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private Mock<IContactManagerService> _service;


        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<IContactManagerService>();
        }

        [TestMethod]
        public void CreateValidContact()
        {
            // Arrange
            var contact = new Contact();
            _service.Setup(s => s.CreateContact(1, contact)).Returns(true);
            var controller = new ContactController(_service.Object);
        
            // Act
            var result = (RedirectToRouteResult)controller.Create(1, contact);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void CreateInvalidContact()
        {
            // Arrange
            var contact = new Contact();
            contact.Group = new Group();
            _service.Setup(s => s.CreateContact(1, contact)).Returns(false);
            var controller = new ContactController(_service.Object);

            // Act
            var result = (ViewResult)controller.Create(1, contact);

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }


    }
}
