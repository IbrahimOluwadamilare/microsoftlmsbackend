using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using microsoft_lms_backend.Helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;
using microsoft_lms_backend.Controllers.v1;
using microsoft_lms_backend.Services.v1;

namespace microsoft_lms_backend_test
{
    [TestClass]
    public class CategoryControllerTest
    {
        private readonly CategoryController Controller = new CategoryController(new CategoryService());

        CategoryInput GetDemoCategory()
        {
            return new CategoryInput()
            {
                Name = "None",
                Description = "Done"
            };
        }

        [TestMethod]
        public void CreateNewCategory_ShouldReturnSameCategory()
        {
            var model = GetDemoCategory();

            var result = Controller.CreateNewCategory(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewCategory_ModelStateIsValid()
        {           
            var model = GetDemoCategory();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

     

        [TestMethod]
        public void UpdateCategory_ShouldFail_WhenDifferentID()
        {
            var update = new CategoryInput()
            {
                Name = "Finance",
                Description = "This category deals with finance topics"
            };
            var result = Controller.UpdateCategory(90, update);
            Assert.AreEqual(result.Result.Value.Message, "Category category to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }


        [TestMethod]
        public void GetAllCategorys_ShouldReturnAllCategorys()
        {

            var result = Controller.GetAllCategory();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteCategory_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromCategory(1) as Task<ActionResult<GenericResponse<Category>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
