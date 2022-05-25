using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ArticleController : ControllerBase
    {
        //Dependency injection of Articles service
        private readonly IArticle _articlesService;

        //ArticlesController constructor
        public ArticleController(IArticle articlesService)
        {
            _articlesService = articlesService;
        }

        //Creating New Articles post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Articles>>> CreateNewArticle([FromBody] ArticleInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var Articles = new Articles
                    {
                        ArticleTitle = input.ArticleTitle,
                        ArticleContent =input.ArticleContent,
                        Banner = input.Banner,
                        PublicationDate = input.PublicationDate,
                        IsSaving = input.IsSaving,
                        IsPublihing = input.IsPublihing,
                        DateCreated = DateTime.Now,
                        Id = input.ArticleCategoryId
                    };
                    //creating Articles using the service
                    var newArticles = await _articlesService.CreateNewArticleAsync(Articles);

                    //checking for operation failure
                    if (newArticles.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newArticles);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<Articles>
                        {
                            Data = newArticles.Data,
                            Message = newArticles.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<Articles>
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
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Articless Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Articles>>>> GetAllArticles()
        {
            try
            {
                //Getting all Articles using the service
                var articles = await _articlesService.GetAllArticleAsync();

                //checks for operation failure
                if (articles.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, articles);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Articles>>
                    {
                        Data = articles.Data,
                        Message = articles.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Articles>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single Articles get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<Articles>>> GetArticleById(int Id)
        {
            try
            {
                //Getting a single Articles by Id using the service
                var articles = await _articlesService.GetArticlebyIdAsync(Id);

                //checks for operation failure
                if (articles.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, articles);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<Articles>
                    {
                        Data = articles.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Articles>>> UpdateArticle(int Id, ArticleInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<Articles>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting Articles using the Id
                    var articlesFromDatabase = await _articlesService.GetArticlebyIdAsync(Id);

                    //checks for operation failure
                    if (!articlesFromDatabase.Success)
                    {
                        return new GenericResponse<Articles>
                        {
                            Data = null,
                            Message = "Article to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the category to the new instance
                        articlesFromDatabase.Data.ArticleTitle = Input.ArticleTitle;
                        articlesFromDatabase.Data.ArticleContent = Input.ArticleContent;
                        articlesFromDatabase.Data.Banner = Input.Banner;
                        articlesFromDatabase.Data.PublicationDate = Input.PublicationDate;
                        articlesFromDatabase.Data.DateUpdated = DateTime.Now;
                        articlesFromDatabase.Data.IsSaving = Input.IsSaving;
                        articlesFromDatabase.Data.IsPublihing = Input.IsPublihing;
                        articlesFromDatabase.Data.Id = Input.ArticleCategoryId;

                        //Updating the Articles using the service
                        var newArticles = await _articlesService.UpdateArticleAsync(Id, articlesFromDatabase.Data);

                        //checks for operation failure
                        if (!newArticles.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, articlesFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<Articles>
                            {
                                Data = articlesFromDatabase.Data,
                                Message = newArticles.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Articles Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<Articles>>> RemoveItemFromArticles(int Id)
        {
            try
            {
                //deleting a Articles using the service
                var isDeleted = await _articlesService.RemoveFromArticleAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<Articles>
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
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
        //Articles by article category Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Articles>>>> GetArticlesbyArticleCategory(int ArticleCategoryId)
        {
            try
            {
                //Getting all Articles by article category Id using the service
                var articles = await _articlesService.GetArticlesbyArticleCategoryAsync(ArticleCategoryId);

                //checks for operation failure
                if (articles.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, articles);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<Articles>>
                    {
                        Data = articles.Data,
                        Message = articles.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<Articles>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }
    }
}