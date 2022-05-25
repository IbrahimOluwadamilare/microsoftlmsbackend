using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;



namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ArticleCategoryController : ControllerBase
    {
        //Dependency injection of ArticleCategory service
        private readonly IArticle _articleCategoryService;

        //ArticleCategoryController constructor
        public ArticleCategoryController(IArticle articleService)
        {
            _articleCategoryService = articleService;
        }

        //Creating New ArticleCategory post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ArticleCategory>>> CreateNewArticleCategory([FromBody] ArticleCategoryInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var ArticleCategory = new ArticleCategory
                    {
                        CategoryName = input.CategoryName,
                        CategoryBanner = input.CategoryBanner,
                        Id = input.CategoryId
                    };
                    //creating ArticleCategory using the service
                    var newArticleCategory = await _articleCategoryService.CreateNewArticleCategoryAsync(ArticleCategory);
                    //var CategoryName = newArticleCategory.Data.Category.Name;

                    //checking for operation failure
                    if (newArticleCategory.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newArticleCategory);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<ArticleCategory>
                        {
                            Data = newArticleCategory.Data,
                            Message = newArticleCategory.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<ArticleCategory>
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
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All ArticleCategorys Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<ArticleCategory>>>> GetAllArticleCategories()
        {
            try
            {
                //Getting all ArticleCategory using the service
                var ArticleCategory = await _articleCategoryService.GetAllArticleCategoryAsync();

                //checks for operation failure
                if (ArticleCategory.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ArticleCategory);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<ArticleCategory>>
                    {
                        Data = ArticleCategory.Data,
                        Message = ArticleCategory.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<ArticleCategory>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single ArticleCategory get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<ArticleCategory>>> GetArticleCategoryById(int Id)
        {
            try
            {
                //Getting a single ArticleCategory by Id using the service
                var ArticleCategory = await _articleCategoryService.GetArticleCategorybyIdAsync(Id);

                //checks for operation failure
                if (ArticleCategory.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ArticleCategory);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = ArticleCategory.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<ArticleCategory>>> UpdateArticleCategory(int Id, ArticleCategoryInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting ArticleCategory using the Id
                    var ArticleCategoryFromDatabase = await _articleCategoryService.GetArticleCategorybyIdAsync(Id);

                    //checks for operation failure
                    if (!ArticleCategoryFromDatabase.Success)
                    {
                        return new GenericResponse<ArticleCategory>
                        {
                            Data = null,
                            Message = "Article Category to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the category to the new instance
                        ArticleCategoryFromDatabase.Data.CategoryName = Input.CategoryName;
                        ArticleCategoryFromDatabase.Data.CategoryBanner = Input.CategoryBanner;
                        ArticleCategoryFromDatabase.Data.Id = Input.CategoryId;

                        //Updating the Article Category using the service
                        var newArticleCategory = await _articleCategoryService.UpdateArticleCategoryAsync(Id, ArticleCategoryFromDatabase.Data);

                        //checks for operation failure
                        if (!newArticleCategory.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, ArticleCategoryFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<ArticleCategory>
                            {
                                Data = ArticleCategoryFromDatabase.Data,
                                Message = newArticleCategory.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //ArticleCategory Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<ArticleCategory>>> RemoveItemFromArticleCategory(int Id)
        {
            try
            {
                //deleting a ArticleCategory using the service
                var isDeleted = await _articleCategoryService.RemoveFromArticleCategoryAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<ArticleCategory>
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
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        

    }
}