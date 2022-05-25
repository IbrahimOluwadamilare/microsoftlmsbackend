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
    public class ModuleListItemControllerTest
    {
        private readonly ModuleListItemController Controller = new ModuleListItemController(new ModuleService());

        ModuleListItemInput GetDemoModuleListItem()
        {
            return new ModuleListItemInput()
            {
                Title = "Financial Law",
                Detail = "Financial Law",
                CourseVideo = "video link",
                CourseModuleId = 1
            };
        }

        [TestMethod]
        public void CreateNewModuleListItem_ShouldReturnSameModuleListItem()
        {
            var model = GetDemoModuleListItem();

            var result = Controller.CreateNewModuleListItem(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewModuleListItem_ModelStateIsValid()
        {           
            var model = GetDemoModuleListItem();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

        

        [TestMethod]
        public void UpdateModuleListItem_ShouldFail_WhenDifferentID()
        {
            var update = new ModuleListItemInput()
            {
                Title = "Financial Law",
                Detail = "Financial Law",
                CourseVideo = "link to video",
                CourseModuleId = 1

            };
            var result = Controller.UpdateModuleListItem(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Module list item to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

     

        [TestMethod]
        public void GetAllModuleListItems_ShouldReturnAllModuleListItem()
        {

            var result = Controller.GetAllModuleListItems();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteModuleListItem_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromModuleListItem(1) as Task<ActionResult<GenericResponse<ModuleListItem>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void GetAllModuleListItemByModule_ShouldReturnSuccess()
        {
            var result = Controller.GetAllModuleListItemByModule(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
