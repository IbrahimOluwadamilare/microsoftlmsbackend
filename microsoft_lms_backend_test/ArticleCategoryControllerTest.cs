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
    public class ArticleCategoryControllerTest
    {
        private readonly ArticleCategoryController Controller = new ArticleCategoryController(new ArticleService());

        ArticleCategoryInput GetDemoArticleCategoryInput()
        {
            return new ArticleCategoryInput()
            {
                CategoryName = "Technology",
                CategoryBanner = "Done",
                CategoryId = 1
            };
        }

        [TestMethod]
        public void CreateArticleCategory_ShouldReturnSameArticleCategoryInput()
        {
            var model = GetDemoArticleCategoryInput();

            var result = Controller.CreateNewArticleCategory(model);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateArticleCategory_ModelStateIsValid()
        {           
            var model = GetDemoArticleCategoryInput();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

   

        [TestMethod]
        public void UpdateArticleCategory_ShouldFail_WhenDifferentID()
        {
            var update = new ArticleCategoryInput()
            {
                CategoryName = "Windows Update",
                CategoryBanner = "Done",
                CategoryId = 1
            };
            var result = Controller.UpdateArticleCategory(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Article Category to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }


        [TestMethod]
        public void GetAllArticleCategory_ShouldReturnAllArticleCategoryInput()
        {

            var result = Controller.GetAllArticleCategories();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteArticleCategory_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromArticleCategory(1) as Task<ActionResult<GenericResponse<ArticleCategory>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
