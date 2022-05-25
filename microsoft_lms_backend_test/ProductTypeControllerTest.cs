using Microsoft.VisualStudio.TestTools.UnitTesting;
using microsoft_lms_backend.Controllers.v1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using microsoft_lms_backend.Models.v1.ProductUpload;
using microsoft_lms_backend.InputModels.v1.ProductUploadInputModel;
using microsoft_lms_backend.Helper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Services.v1;

namespace microsoft_lms_backend_test
{
    [TestClass]
    public class ProductTypeControllerTest
    {
        private readonly ProductTypeController Controller = new ProductTypeController(new ProductUploadService());

        ProductTypeInput GetDemoProductType()
        {
            return new ProductTypeInput()
            {
                ProductTypeName = "None",
                ProductTypeDescription = "Done"
            };
        }

        [TestMethod]
        public void CreateNewProductType_ShouldReturnSameProductType()
        {
            var model = GetDemoProductType();

            var result = Controller.CreateNewProductType(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewProductType_ModelStateIsValid()
        {           
            var model = GetDemoProductType();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }

       

        [TestMethod]
        public void UpdateProductType_ShouldFail_WhenDifferentID()
        {
            var update = new ProductTypeInput()
            {
                ProductTypeName = "LEAP",
                ProductTypeDescription = "Done"
            };
            var result = Controller.UpdateProductType(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Product type to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

      

        [TestMethod]
        public void GetAllProductTypes_ShouldReturnAllProductTypes()
        {

            var result = Controller.GetAllProductTypes();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteProductType_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromProductType(1) as Task<ActionResult<GenericResponse<ProductType>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
