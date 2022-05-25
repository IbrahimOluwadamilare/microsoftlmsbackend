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
    public class OfferListTests
    {
        private readonly OfferListController TestOfferListController = new OfferListController(new OfferListService());

        OfferListInput DemoOfferList()
        {
            return new OfferListInput()
            {
              
                OfferDetail = "",
               
            };
        }
        
        [TestMethod]
        public void CreateOfferList_ShouldReturnSameOfferList()
        {
            //Arrange  
            var offerList = DemoOfferList();

            //Act
            var result = TestOfferListController.CreateOfferList(offerList);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void ModelStateIsValid()
        {
            //Arrange
            var offerList = DemoOfferList();

            //Act
            var validationContext = new ValidationContext(offerList, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(offerList, validationContext, results, false);

            //Assert 
            Assert.IsTrue(isModelStateValid);
        }
        [TestMethod]
        public void EditOfferList_ShouldReturnSuccess()
        {
            //Arrange
            var Id = 3;
            var offer = DemoOfferList();
            var editedOfferList = new OfferListInput()
            {
             
                OfferDetail = "sme productivity"
            };

            //Act
            var result = TestOfferListController.EditOfferList(Id, editedOfferList);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void EditOfferList_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 2;
            var editedOfferList = new OfferListInput()
            {
                OfferDetail = "updated version of sme"

            };

            //Act
            var result = TestOfferListController.EditOfferList(Id, editedOfferList);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Message, "Offer List not updated");
        }
        

       [TestMethod]
        public void GetOfferListById_ShouldReturnOfferListWithSameID()
        {
            //Arrange
            var Id = 2;
            var offerList = DemoOfferList();
          
            //Act
            var result = TestOfferListController.GetOfferListById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllOfferLists_ShouldReturnAllOfferLists()
        {
            //Act 
            var result = TestOfferListController.GetAllOfferLists();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true); 
        }

        [TestMethod]
        public void DeleteOfferList_ShouldReturnSuccess()
        {
            //Act
            var result = TestOfferListController.DeleteOfferList(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 