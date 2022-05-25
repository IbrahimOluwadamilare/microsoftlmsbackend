using Microsoft.VisualStudio.TestTools.UnitTesting;
using microsoft_lms_backend.Controllers.v1;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Services.v1;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace microsoft_lms_backend_test
{
    [TestClass]
    public class OfferTests
    {
        private readonly OfferController TestOfferController = new OfferController(new OfferService());

         OfferInput DemoOffer()
        {
            return new OfferInput()
            {
              
                OfferName = "",
                Description = "",
               
            };
        }
        
        [TestMethod]
        public void CreateOffer_ShouldReturnSameOffer()
        {
            //Arrange  
            var offer = DemoOffer();

            //Act
            var result = TestOfferController.CreateOffer(offer);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void ModelStateIsValid()
        {
            //Arrange
            var offer = DemoOffer();

            //Act
            var validationContext = new ValidationContext(offer, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(offer, validationContext, results, false);

            //Assert 
            Assert.IsTrue(isModelStateValid);
        }
        [TestMethod]
        public void EditOffer_ShouldReturnSuccess()
        {
            //Arrange
            var Id = 3;
            var offer = DemoOffer();
            var editedOffer = new OfferInput()
            {
              
                Description = "HR and business development bundle",
                OfferName = "sme bundle"
            };

            //Act
            var result = TestOfferController.EditOffer(Id, editedOffer);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void EditOffer_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 2;
            var editedOffer = new OfferInput()
            {
               
                Description = "facebook and instagram marketing",
                OfferName = "social media bundle"

            };

            //Act
            var result = TestOfferController.EditOffer(Id, editedOffer);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Message, "Offer not updated");
        }
        

       [TestMethod]
        public void GetOfferById_ShouldReturnOfferWithSameID()
        {
            //Arrange
            var Id = 2;
            var offer = DemoOffer();
           

            //Act
            var result = TestOfferController.GetOfferById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllOffers_ShouldReturnAllOffers()
        {
            //Act 
            var result = TestOfferController.GetAllOffers();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true); 
        }

        [TestMethod]
        public void DeleteOffer_ShouldReturnSuccess()
        {
            //Act
            var result = TestOfferController.DeleteOffer(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 