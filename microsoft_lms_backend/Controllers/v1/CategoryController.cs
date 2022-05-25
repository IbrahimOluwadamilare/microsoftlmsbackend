using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;

namespace microsoft_lms_backend.Controllers.v1
{
    //NOTE: 'Category' and 'Category Category' are used interchangeably. It refer to same thing 
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //Dependency injection of Category service
        private readonly ICategory _categoryService;

        //CategoryController constructor
        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        //Creating New Category catecory post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Category>>> CreateNewCategory([FromBody] CategoryInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var Category = new Category
                    {
                        Name = input.Name,
                        Description = input.Description

                    };
                    //creating Category category using the service
                    var newCategory = await _categoryService.CreateNewCategoryAsync(Category);

                    //checking for operation failure
                    if (newCategory.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newCategory);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<Category>
                        {
                            Data = newCategory.Data,
                            Message = newCategory.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<Category>
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
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All categories Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Category>>>> GetAllCategory()
        {
            try
            {
                //Getting all Category category using the service
                var Category = await _categoryService.GetAllCategoryAsync();

                //checks for operation failure
                if (Category.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, Category);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Category>>
                    {
                        Data = Category.Data,
                        Message = Category.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Category>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Category category get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Category>>> GetCategoryById(int Id)
        {
            try
            {
                //Getting a single category by Id using the service
                var Category = await _categoryService.GetCategorybyIdAsync(Id);

                //checks for operation failure
                if (Category.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, Category);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<Category>
                    {
                        Data = Category.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Category>>> UpdateCategory(int Id, CategoryInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<Category>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Category category using the Id
                    var categoryFromDatabase = await _categoryService.GetCategorybyIdAsync(Id);

                    //checks for operation failure
                    if (!categoryFromDatabase.Success)
                    {
                        return new GenericResponse<Category>
                        {
                            Data = null,
                            Message = "Category category to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the category to the new instance
                        categoryFromDatabase.Data.Name = Input.Name;
                        categoryFromDatabase.Data.Description = Input.Description;
                        //Updating the category using the service
                        var newCategory = await _categoryService.UpdateCategoryAsync(categoryFromDatabase.Data);

                        //checks for operation failure
                        if (!newCategory.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, categoryFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<Category>
                            {
                                Data = categoryFromDatabase.Data,
                                Message = newCategory.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Category Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Category>>> RemoveItemFromCategory(int Id)
        {
            try
            {
                //deleting a Category ctegory using the service
                var isDeleted = await _categoryService.RemoveFromCategoryAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<Category>
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
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
    }
}