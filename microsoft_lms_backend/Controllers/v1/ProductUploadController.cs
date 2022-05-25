using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.ProductUploadInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ProductUpload;

namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ProductUploadController : ControllerBase
    {
        //Dependency injection of Product service
        private readonly IProductUpload _productUploadService;

        //ProductController constructor
        public ProductUploadController(IProductUpload productUploadService)
        {
            _productUploadService = productUploadService;
        }

        //Creating New Product post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Product>>> CreateNewProduct([FromBody] ProductInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var product = new Product
                    {
                        ProductName = input.ProductName,
                        ProductDescription = input.ProductDescription,
                        Price = input.Price,
                        DateCreated = DateTime.Now,
                        Id = input.ProductTypeId,
                    };
                    //creating Product using the service
                    var newProduct = await _productUploadService.CreateNewProductAsync(product);

                    //checking for operation failure
                    if (newProduct.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newProduct);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<Product>
                        {
                            Data = newProduct.Data,
                            Message = newProduct.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Products Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Product>>>> GetAllProducts()
        {
            try
            {
                //Getting all Product using the service
                var product = await _productUploadService.GetAllProductAsync();

                //checks for operation failure
                if (product.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, product);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Product>>
                    {
                        Data = product.Data,
                        Message = product.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Product>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Product get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Product>>> GetProductById(int Id)
        {
            try
            {
                //Getting a single Product by Id using the service
                var product = await _productUploadService.GetProductbyIdAsync(Id);

                //checks for operation failure
                if (product.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, product);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<Product>
                    {
                        Data = product.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Product>>> UpdateProduct(int Id, ProductInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Product using the Id
                    var productFromDatabase = await _productUploadService.GetProductbyIdAsync(Id);

                    //checks for operation failure
                    if (!productFromDatabase.Success)
                    {
                        return new GenericResponse<Product>
                        {
                            Data = null,
                            Message = "Product to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the product to the new instance
                        productFromDatabase.Data.ProductName = Input.ProductName;
                        productFromDatabase.Data.ProductDescription = Input.ProductDescription;
                        productFromDatabase.Data.Price = Input.Price;
                        productFromDatabase.Data.DateCreated = productFromDatabase.Data.DateCreated;
                        productFromDatabase.Data.DateUpdated = DateTime.Now;
                        productFromDatabase.Data.Id = Input.ProductTypeId;


                        //Updating the Product using the service
                        var newProduct = await _productUploadService.UpdateProductAsync(Id, productFromDatabase.Data);

                        //checks for operation failure
                        if (!newProduct.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, productFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<Product>
                            {
                                Data = productFromDatabase.Data,
                                Message = newProduct.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Product Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Product>>> RemoveItemFromProduct(int Id)
        {
            try
            {
                //deleting a Product using the service
                var isDeleted = await _productUploadService.RemoveFromProductAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<Product>
                    {
                        Data = isDeleted.Data,
                        Message = isDeleted.Message,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        //Articles by article category Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Product>>>> GetArticlesbyArticleCategory(int ProductTypeId)
        {
            try
            {
                //Getting all Articles by article category Id using the service
                var products = await _productUploadService.GetAllProductByProductTypeAsync(ProductTypeId);

                //checks for operation failure
                if (products.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, products);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Product>>
                    {
                        Data = products.Data,
                        Message = products.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Product>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }
    }
}