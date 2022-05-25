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
    public class BusinessExpertiseTest
    {
        private readonly BusinessExpertiseController TestBusinessExpertiseController = new BusinessExpertiseController(new BusinessExpertiseService());

        BusinessExpertiseInput DemoBusinessExpertise()
        {
            return new BusinessExpertiseInput()
            {
                Expertises = "",
                DateCreated = DateTime.Now
                
            };
        }
        [TestMethod]
        public void CreateBusinessExpertise_ShouldReturnSameExpertise()
        {
            //Arrange  
            var expertise = DemoBusinessExpertise();

            //Act
            var result = TestBusinessExpertiseController.CreateNewBusinessExpertise(expertise);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void CreateNewBusinessExpertise_ModelStateIsValid()
        {
            //Arrange
            var expertise = DemoBusinessExpertise();

            //Act
            var validationContext = new ValidationContext(expertise, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(expertise, validationContext, results, true);

            //Assert 
            Assert.IsTrue(isModelStateValid);
        }
        [TestMethod]
        public void UpdateExpertise_ShouldReturnSuccess()
        {
            //Arrange
          
            var expertise = DemoBusinessExpertise();
            var create = TestBusinessExpertiseController.CreateNewBusinessExpertise(expertise);
            var Id = 3;
            var editedExpertise = new BusinessExpertiseInput()
            {

                Expertises = "human rsourse",
                DateCreated = DateTime.Now,

            };

            //Act
            var result = TestBusinessExpertiseController.UpdateBusinessExpertise(Id, editedExpertise);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void UpdateExpertise_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 5;
            var editedExpertise = new BusinessExpertiseInput()
            {
                Expertises = "busiess development",
                DateCreated = DateTime.Now,
               

            };

            //Act
            var result = TestBusinessExpertiseController.UpdateBusinessExpertise(Id, editedExpertise);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Data, null);
        }

        [TestMethod]
        public void GetExpertiseById_ShouldReturnExpertiseWithSameID()
        {
            //Arrange
            var Id = 3;
            var profile = DemoBusinessExpertise();

            //Act
            var result = TestBusinessExpertiseController.GetBusinessExpertiseById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllExpertise_ShouldReturnAllExpertises()
        {
            //Act 
            var result = TestBusinessExpertiseController.GetAllBusinessExpertises();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteExpertise_ShouldReturnSuccess()
        {
            //Act
            var result = TestBusinessExpertiseController.DeleteBusinessExpertise(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 