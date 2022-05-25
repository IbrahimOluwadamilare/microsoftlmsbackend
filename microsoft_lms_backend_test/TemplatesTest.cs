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
    public class TemplatesTests
    {
        private readonly TemplateController TestTemplateController = new TemplateController(new TemplatesService());

        TemplateInput DemoTemplate()
        {
            return new TemplateInput()
            {
               // Id = 3,
                Title = "",
                Description = "",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
        }
        
        [TestMethod]
        public void CreateTemplate_ShouldReturnSameTemplate()
        {
            //Arrange  
            var template = DemoTemplate();

            //Act
            var result = TestTemplateController.CreateTemplate(template);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void ModelStateIsValid()
        {
            //Arrange
            var template = DemoTemplate();

            //Act
            var validationContext = new ValidationContext(template, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(template, validationContext, results, false);

            //Assert 
            Assert.IsTrue(isModelStateValid);
        }
        [TestMethod]
        public void EditTemplate_ShouldReturnSuccess()
        {
            //Arrange
            var Id = 3;
            var template = DemoTemplate();
           // var templateId =  template.Id;
            var editedTemplate = new TemplateInput()
            {
              //  Id = templateId,
                Description = "trial balance",
                Title = "book keeping",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            //Act
            var result = TestTemplateController.EditTemplate(Id, editedTemplate);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
        [TestMethod]
        public void EditTemplate_ShouldFail_WhenDifferentID()
        {
            //Arrange
            var Id = 2;
            var editedTemplate = new TemplateInput()
            {
                //Id = 10,
                Description = "trial balance",
                Title = "book keeping",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            //Act
            var result = TestTemplateController.EditTemplate(Id, editedTemplate);

           //Assert
            Assert.AreEqual(result.Result.Value.Success, false);
            Assert.AreEqual(result.Result.Value.Message, "Template not updated");
        }

        [TestMethod]
        public void GetTemplateById_ShouldReturnTemplateWithSameID()
        {
            //Arrange
            var Id = 2;
            var template = DemoTemplate();
           // var templateId = template.Id;

            //Act
            var result = TestTemplateController.GetTemplateById(Id);

            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllTemplates_ShouldReturnAllTemplates()
        {
            //Act 
            var result = TestTemplateController.GetAllTemplates();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteTemplate_ShouldReturnSuccess()
        {
            //Act
            var result = TestTemplateController.DeleteTemplate(5);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
 