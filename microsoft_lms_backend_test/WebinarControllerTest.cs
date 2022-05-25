using Microsoft.VisualStudio.TestTools.UnitTesting;
using microsoft_lms_backend.Controllers.v1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using microsoft_lms_backend.Models.v1.WebinarModels;
using microsoft_lms_backend.InputModels.v1.WebinarInputModel;
using microsoft_lms_backend.Helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Services.v1;

namespace microsoft_lms_backend_test
{
    [TestClass]
    public class WebinarControllerTest
    {
        private readonly WebinarController Controller = new WebinarController(new WebinarService());

        WebinarInput GetDemoWebinar()
        {
            return new WebinarInput()
            {
                EventTitle = "None",
                EventDescription = "Done",
                EventDate = new DateTime(2021, 1, 18)
            };
        }

        


        [TestMethod]
        public void CreateNewWebinar_ModelStateIsValid()
        {           
            var model = GetDemoWebinar();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

      

        [TestMethod]
        public void UpdateWebinar_ShouldFail_WhenDifferentID()
        {
            var update = new WebinarInput()
            {
                EventTitle = "LEAP",
                EventDescription = "Done",
                EventDate = new DateTime(2021, 1, 18)
            };
            var result = Controller.UpdateWebinar(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Webinar to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

       

        [TestMethod]
        public void GetAllWebinars_ShouldReturnAllWebinars()
        {

            var result = Controller.GetAllWebinars();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteWebinar_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromWebinar(1) as Task<ActionResult<GenericResponse<Webinar>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
