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
    public class CourseControllerTest
    {
        private readonly CourseController Controller = new CourseController(new CourseService());

        CourseInput GetDemoCourse()
        {
            return new CourseInput()
            {
                CourseTitle = "Financial Law",
                CourseDescription = "Done",
                LearningTrackId = 1

            };
        }



        [TestMethod]
        public void CreateNewCourse_ModelStateIsValid()
        {           
            var model = GetDemoCourse();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

     

        [TestMethod]
        public void UpdateCourse_ShouldFail_WhenDifferentID()
        {
            var update = new CourseInput()
            {
                CourseTitle = "Financial Law",
                CourseDescription = "Done",
                LearningTrackId = 1
            };
            var result = Controller.UpdateCourse(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Course to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

       

        [TestMethod]
        public void GetAllCourses_ShouldReturnAllCourse()
        {

            var result = Controller.GetAllCourses();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteCourse_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromCourses(1) as Task<ActionResult<GenericResponse<Courses>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void GetAllCourseByLearningTrack_ShouldReturnSuccess()
        {
            var result = Controller.GetAllCourseByLearningTrack(1) ;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
