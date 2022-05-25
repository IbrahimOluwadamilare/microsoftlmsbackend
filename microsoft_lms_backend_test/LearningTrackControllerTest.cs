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
    public class LearningTrackControllerTest
    {
        private readonly LearningTrackController Controller = new LearningTrackController(new LearningTrackService());

        LearningTrackInput GetDemoLearningTrack()
        {
            return new LearningTrackInput()
            {
                TrackName = "Financial Law",
                TrackDescription = "Done",
                TrackBanner = "picture",
                CourseCategoryId = 1
            };
        }

        [TestMethod]
        public void CreateNewLearningTrack_ShouldReturnSameLearningTrack()
        {
            var model = GetDemoLearningTrack();

            var result = Controller.CreateNewLearningTrack(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewLearningTrack_ModelStateIsValid()
        {           
            var model = GetDemoLearningTrack();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

        

        [TestMethod]
        public void UpdateLearningTrack_ShouldFail_WhenDifferentID()
        {
            var update = new LearningTrackInput()
            {
                TrackName = "Financial Law",
                TrackDescription = "Done",
                TrackBanner = "picture",
                CourseCategoryId = 1

            };
            var result = Controller.UpdateLearningTrack(99, update);
            Assert.AreEqual(result.Result.Value.Message, "learning track to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

     

        [TestMethod]
        public void GetAllLearningTrack_ShouldReturnAllLearningTrack()
        {

            var result = Controller.GetAllLearningTracks();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteLearningTrack_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromLearningTrack(1) as Task<ActionResult<GenericResponse<LearningTrack>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void GetAllLearningTrackByCourseCategory_ShouldReturnSuccess()
        {
            var result = Controller.GetAllLearningTrackByCourseCategory(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
