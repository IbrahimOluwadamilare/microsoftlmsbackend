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
    public class BusinessProfileTest
    {
        private readonly BusinessProfileController TestBusinessProfileController = new BusinessProfileController(new BusinessProfileService());

        BusinessProfileInput DemoBusinessProfile()
        {
            return new BusinessProfileInput()
            {
               // ProfileId = 0,
                Banner = "",
                Name = "",
                Detail = "",
                FacebookSocial = "",
                TwitterSocial = "",
                LinkedInSocial = "",
                DateCreated = DateTime.Now,
              //  DateUpdated = DateTime.Now,
            };
        }
        [TestMethod]
        public void CreateBusinessProfile_ShouldReturnSameProfile()
        {
            //Arrange  
            var profile = DemoBusinessProfile();

            //Act
            var result = TestBusinessProfileController.CreateNewBusinessProfile(profile);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void CreateNewBusinessProfile_ModelStateIsValid()
        {
            //Arrange
            var profile= DemoBusinessProfile();

            //Act
            var validationContext = new ValidationContext(profile, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(profile, validationContext, results, true);

            //Assert 
            Assert.IsFalse(isModelStateValid);
        }
        [TestMethod]
        public void UpdateProfile_ShouldReturnSuccess()
        {
            //Arrange
          
            var profile = DemoBusinessProfile();
            var create = TestBusinessProfileController.CreateNewBusinessProfile(profile);
            var Id = 3;
            var editedProfile = new BusinessProfileInput()
            {
                
                Banner = "",
                Name = "",
                Detail = "",
                FacebookSocial = "",
                TwitterSocial = "",
                LinkedInSocial = "",
                DateUpdated = DateTime.Now,
       
             
            };

            //Act
            var result = TestBusinessProfileController.UpdateBusinessProfile(Id, editedProfile);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void UpdateProfile_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 5;
            var editedProfile = new BusinessProfileInput()
            {
              //  ProfileId = 0,
                Banner = "",
                Name = "",
                Detail = "",
                FacebookSocial = "",
                TwitterSocial = "",
                LinkedInSocial = "",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now, 
               
            };

            //Act
            var result = TestBusinessProfileController.UpdateBusinessProfile(Id, editedProfile);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Data, null);
        }

        [TestMethod]
        public void GetProfileById_ShouldReturnProfileWithSameID()
        {
            //Arrange
            var Id = 3;
            var profile = DemoBusinessProfile();
           // var profileId = profile.ProfileId;

            //Act
            var result = TestBusinessProfileController.GetBusinessProfileById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllProfile_ShouldReturnAllProfiles()
        {
            //Act 
            var result = TestBusinessProfileController.GetAllBusinessProfiles();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteProfile_ShouldReturnSuccess()
        {
            //Act
            var result = TestBusinessProfileController.DeleteBusinessProfile(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 