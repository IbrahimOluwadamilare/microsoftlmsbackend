using Microsoft.VisualStudio.TestTools.UnitTesting;
using microsoft_lms_backend.Controllers.v1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using microsoft_lms_backend.Helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Services.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;


namespace microsoft_lms_backend_test
{
    [TestClass]
    public class NewsControllerTest
    {
        private readonly NewsController Controller = new NewsController(new NewsService());

        NewsInput GetDemoNews()
        {
            return new NewsInput()
            {
                NewsTitle = "Windows Update",
                NewsBanner = "Done",
                NewsCategory = "Done",
                PublishedBy = "Done",
                CategoryId = 1
            };
        }

       


        [TestMethod]
        public void CreateNews_ModelStateIsValid()
        {           
            var model = GetDemoNews();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

      

        [TestMethod]
        public void UpdateNews_ShouldFail_WhenDifferentID()
        {
            var update = new NewsInput()
            {
                NewsTitle = "Financial Law",
                NewsBanner = "Done",
                NewsCategory = "Done",
                PublishedBy = "Done",
                CategoryId = 1

            };
            var result = Controller.UpdateNews(99, update);
            Assert.AreEqual(result.Result.Value.Message, "News to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }


        [TestMethod]
        public void GetAllNews_ShouldReturnAllNews()
        {

            var result = Controller.GetAllNews();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteNews_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromNews(1) as Task<ActionResult<GenericResponse<News>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
