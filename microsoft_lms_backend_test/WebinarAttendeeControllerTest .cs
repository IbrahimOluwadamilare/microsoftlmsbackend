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
    public class WebinarAttendeeControllerTest
    {
        private readonly WebinarAttendeeController Controller = new WebinarAttendeeController(new WebinarAttendeeService());

        WebinarAttendeeInput GetDemoWebinarAttendee()
        {
            return new WebinarAttendeeInput()
            {
                AttendeeName = "None",
                AttendeePhoneNumber = "Done",
                AttendeeEmail = "",
                AttendeeOcupation = "",
                WebinarId= 1
            };
        }

        [TestMethod]
        public void CreateNewWebinarAttendee_ShouldReturnSameWebinarAttendee()
        {
            var model = GetDemoWebinarAttendee();

            var result = Controller.CreateNewWebinarAttendee(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewWebinarAttendee_ModelStateIsValid()
        {           
            var model = GetDemoWebinarAttendee();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }


      

        [TestMethod]
        public void GetAllWebinarAttendees_ShouldReturnAllWebinarAttendees()
        {

            var result = Controller.GetAllWebinarAttendees();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteWebinarAttendee_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromWebinarAttendee(1) as Task<ActionResult<GenericResponse<WebinarAttendee>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
