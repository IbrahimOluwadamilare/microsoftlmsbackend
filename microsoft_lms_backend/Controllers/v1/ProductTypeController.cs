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
    public class ProductTypeController : ControllerBase
    {
        //Dependency injection of ProductType service
        private readonly IProductUpload _ProductTypeUploadService;

        //ProductTypeController constructor
        public ProductTypeController(IProductUpload ProductTypeUploadService)
        {
            _ProductTypeUploadService = ProductTypeUploadService;
        }

        //Creating New Product Type post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ProductType>>> CreateNewProductType([FromBody] ProductTypeInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var productType = new ProductType
                    {
                        ProductTypeName = input.ProductTypeName,
                        ProductTypeDescription = input.ProductTypeDescription
                    };
                    //creating Product type using the service
                    var newProductType = await _ProductTypeUploadService.CreateNewProductTypeAsync(productType);

                    //checking for operation failure
                    if (newProductType.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newProductType);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<ProductType>
                        {
                            Data = newProductType.Data,
                            Message = newProductType.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<ProductType>
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
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All ProductTypes Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<ProductType>>>> GetAllProductTypes()
        {
            try
            {
                //Getting all Product type using the service
                var productType = await _ProductTypeUploadService.GetAllProductTypeAsync();

                //checks for operation failure
                if (productType.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, productType);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<ProductType>>
                    {
                        Data = productType.Data,
                        Message = productType.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<ProductType>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single ProductType get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<ProductType>>> GetProductTypeById(int Id)
        {
            try
            {
                //Getting a single Product type by Id using the service
                var ProductType = await _ProductTypeUploadService.GetProductTypebyIdAsync(Id);

                //checks for operation failure
                if (ProductType.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ProductType);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<ProductType>
                    {
                        Data = ProductType.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ProductType>>> UpdateProductType(int Id, ProductTypeInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Product type using the Id
                    var productTypeFromDatabase = await _ProductTypeUploadService.GetProductTypebyIdAsync(Id);

                    //checks for operation failure
                    if (!productTypeFromDatabase.Success)
                    {
                        return new GenericResponse<ProductType>
                        {
                            Data = null,
                            Message = "Product type to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the product type to the new instance
                        productTypeFromDatabase.Data.ProductTypeName = Input.ProductTypeName;
                        productTypeFromDatabase.Data.ProductTypeDescription = Input.ProductTypeDescription;


                        //Updating the Product type using the service
                        var newProductType = await _ProductTypeUploadService.UpdateProductTypeAsync(Id, productTypeFromDatabase.Data);

                        //checks for operation failure
                        if (!newProductType.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, productTypeFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<ProductType>
                            {
                                Data = productTypeFromDatabase.Data,
                                Message = newProductType.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Product type Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<ProductType>>> RemoveItemFromProductType(int Id)
        {
            try
            {
                //deleting a ProductType using the service
                var isDeleted = await _ProductTypeUploadService.RemoveFromProductTypeAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<ProductType>
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
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

    }
}