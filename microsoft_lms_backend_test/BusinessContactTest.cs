using Microsoft.VisualStudio.TestTools.UnitTesting;
using microsoft_lms_backend.Controllers.v1;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;
using microsoft_lms_backend.Services.v1;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace microsoft_lms_backend_test
{
    [TestClass]
    public class BusinessContactTest
    {
        private readonly BusinessContactController TestBusinessContactController = new BusinessContactController(new BusinessContactService());

        BusinessContactInput DemoBusinessContact()
        {
            return new BusinessContactInput()
            {
                Longitude = 0.0F,
                Latitude = 0.0F,
                BusinessEmail = "",
                BusinessPhoneNumber = 0,
                DateCreated = DateTime.Now

            };
        }
        [TestMethod]
        public void CreateBusinessContact_ShouldReturnSameProfile()
        {
            //Arrange  
            var contact = DemoBusinessContact();

            //Act
            var result = TestBusinessContactController.CreateNewBusinessContact(contact);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void CreateNewBusinessContact_ModelStateIsValid()
        {
            //Arrange
            var contact = DemoBusinessContact();

            //Act
            var validationContext = new ValidationContext(contact, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(contact, validationContext, results, true);

            //Assert 
            Assert.IsTrue(isModelStateValid);
        }
        [TestMethod]
        public void UpdateContact_ShouldReturnSuccess()
        {
            //Arrange
          
            var contact = DemoBusinessContact();
            var create = TestBusinessContactController.CreateNewBusinessContact(contact);
            var Id = 3;
            var editedContact = new BusinessContactInput()
            {

                Longitude = 10.0F,
                Latitude = 0.0F,
                BusinessEmail = "",
                BusinessPhoneNumber = 0,
                DateCreated = DateTime.Now


            };

            //Act
            var result = TestBusinessContactController.UpdateBusinessContact(Id, editedContact);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void UpdateContact_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 5;
            var editedContact = new BusinessContactInput()
            {
                Longitude = 20.0F,
                Latitude = 0.0F,
                BusinessEmail = "",
                BusinessPhoneNumber = 0,
                DateCreated = DateTime.Now

            };

            //Act
            var result = TestBusinessContactController.UpdateBusinessContact(Id, editedContact);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Data, null);
        }

        [TestMethod]
        public void GetContactById_ShouldReturnContactWithSameID()
        {
            //Arrange
            var Id = 3;
            var profile = DemoBusinessContact();

            //Act
            var result = TestBusinessContactController.GetBusinessContactById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllContact_ShouldReturnAllContacts()
        {
            //Act 
            var result = TestBusinessContactController.GetAllBusinessContacts();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteContact_ShouldReturnSuccess()
        {
            //Act
            var result = TestBusinessContactController.DeleteBusinessContact(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 