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
    public class CourseModuleModuleControllerTest
    {
        private readonly CourseModuleController Controller = new CourseModuleController(new ModuleService());

        CourseModuleInput GetDemoCourseModule()
        {
            return new CourseModuleInput()
            {
                ModuleTitle = "Financial Law",
                CourseId = 1

            };
        }

        [TestMethod]
        public void CreateNewCourseModule_ShouldReturnSameCourseModule()
        {
            var model = GetDemoCourseModule();

            var result = Controller.CreateNewCourseModule(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewCourseModule_ModelStateIsValid()
        {           
            var model = GetDemoCourseModule();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }


        [TestMethod]
        public void UpdateCourseModule_ShouldFail_WhenDifferentID()
        {
            var update = new CourseModuleInput()
            {
                ModuleTitle = "Financial Law",
                CourseId = 1
            };
            var result = Controller.UpdateCourseModule(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Course module to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }


        [TestMethod]
        public void GetAllCourseModules_ShouldReturnAllCourseModule()
        {

            var result = Controller.GetAllCourseModule();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteCourseModule_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromCourseModule(1) as Task<ActionResult<GenericResponse<CourseModule>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void GetAllModuleByCourseAsync_ShouldReturnSuccess()
        {
            var result = Controller.GetAllModuleByCourseAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
