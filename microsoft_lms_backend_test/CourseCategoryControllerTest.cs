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
    public class CourseCategoryControllerTest
    {
        private readonly CourseCategoryController Controller = new CourseCategoryController(new CourseService());

        CourseCategoryInput GetDemoCourseCategory()
        {
            return new CourseCategoryInput()
            {
                CategoryName = "Financial Law",
                CategoryBanner = "Done",
                CategoryId = 1

            };
        }

        [TestMethod]
        public void CreateNewCourseCategory_ShouldReturnSameCourseCategory()
        {
            var model = GetDemoCourseCategory();

            var result = Controller.CreateNewCourseCategory(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewCourseCategory_ModelStateIsValid()
        {           
            var model = GetDemoCourseCategory();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }


        [TestMethod]
        public void UpdateCourseCategory_ShouldFail_WhenDifferentID()
        {
            var update = new CourseCategoryInput()
            {
                CategoryName = "Financial Risk",
                CategoryBanner = "Done",
                CategoryId = 1

            };
            var result = Controller.UpdateCourseCategory(99, update);
            Assert.AreEqual(result.Result.Value.Message, "CourseCategory to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

       

        [TestMethod]
        public void GetAllCourseCategorys_ShouldReturnAllCourseCategorys()
        {

            var result = Controller.GetAllCourseCategories();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteCourseCategory_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromCourseCategory(1) as Task<ActionResult<GenericResponse<CourseCategory>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
