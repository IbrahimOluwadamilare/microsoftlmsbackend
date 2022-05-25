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
    public class ProductUploadControllerTest
    {
        private readonly ProductUploadController Controller = new ProductUploadController(new ProductUploadService());

        ProductInput GetDemoProductUpload()
        {
            return new ProductInput()
            {
                ProductName = "None",
                ProductDescription = "Done",
                Price = 7,
                ProductTypeId = 4,

            };
        }

        [TestMethod]
        public void CreateNewProductUpload_ShouldReturnSameProductUpload()
        {
            var model = GetDemoProductUpload();

            var result = Controller.CreateNewProduct(model);

            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void CreateNewProductUpload_ModelStateIsValid()
        {           
            var model = GetDemoProductUpload();
         
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
            Assert.IsTrue(isModelStateValid);
        }


        [TestMethod]
        public void UpdateProductUpload_ShouldFail_WhenDifferentID()
        {
            var update = new ProductInput()
            {
                ProductName = "LEAP",
                ProductDescription = "Done",
                Price = 7,
                ProductTypeId = 1
            };
            var result = Controller.UpdateProduct(99, update);
            Assert.AreEqual(result.Result.Value.Message, "Product to be updated does not exist");
            Assert.AreEqual(result.Result.Value.Success, false);

        }

       

        [TestMethod]
        public void GetAllProductUploads_ShouldReturnAllProductUploads()
        {

            var result = Controller.GetAllProducts();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }

        [TestMethod]
        public void DeleteProductUpload_ShouldReturnSuccess()
        {
            var result = Controller.RemoveItemFromProduct(1) as Task<ActionResult<GenericResponse<Product>>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }


        [TestMethod]
        public void GetArticlesbyArticleCategory_ShouldReturnSuccess()
        {
            var result = Controller.GetArticlesbyArticleCategory(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IsCompletedSuccessfully, true);
        }
    }
}
