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
    public class ArticleControllerTest
    {
        private readonly ArticleController Controller = new ArticleController(new ArticleService());

        ArticleInput GetDemoArticles()
        {
            return new ArticleInput()
            {
                ArticleTitle = "Windows Update Changes and uses",
                Banner = "Done",
                ArticleCategory = "Done",
                ArticleCategoryId = 1
            };
        }

        [TestMethod]
        public void CreateArticle_ShouldReturnSameArticles()
        {
            var model = GetDemoArticles();

            var result = Controller.CreateNewArticle(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateArticle_ModelStateIsValid()
        {           
            var model = GetDemoArticles();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

      

        [TestMethod]
        public void UpdateArticle_ShouldFail_WhenDifferentID()
        {
            var update = new ArticleInput()
            {
                ArticleTitle = "Windows Update Changes and uses",
                Banner = "Done",
                ArticleCategory = "Done",
                ArticleCategoryId = 1

            };
            var result = Controller.UpdateArticle(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Article to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

     

        [TestMethod]
        public void GetAllArticles_ShouldReturnAllArticles()
        {

            var result = Controller.GetAllArticles();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteArticle_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromArticles(1) as Task<ActionResult<GenericResponse<Articles>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
