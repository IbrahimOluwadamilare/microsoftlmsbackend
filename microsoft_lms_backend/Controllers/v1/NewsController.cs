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
    public class NewsController : ControllerBase
    {
        //Dependency injection of News service
        private readonly INews _NewsService;

        //NewsController constructor
        public NewsController(INews NewsService)
        {
            _NewsService = NewsService;
        }

        //Creating New News post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<News>>> CreateNewNews([FromBody] NewsInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var News = new News
                    {
                        NewsTitle = input.NewsTitle,
                        NewsBanner = input.NewsBanner,
                        NewsCategory = input.NewsCategory,
                        PublishedBy = input.PublishedBy,
                        PublishingDate = input.PublishingDate,
                        IsSaving= input.IsSaving,
                        IsPublihing = input.IsPublihing,
                        DateUpdated = DateTime.Now,
                        Id = input.CategoryId
                    };
                    //creating News using the service
                    var newNews = await _NewsService.CreateNewsAsync(News);

                    //checking for operation failure
                    if (newNews.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newNews);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<News>
                        {
                            Data = newNews.Data,
                            Message = newNews.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<News>
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
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All Newss Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<News>>>> GetAllNews()
        {
            try
            {
                //Getting all News using the service
                var News = await _NewsService.GetAllNewsAsync();

                //checks for operation failure
                if (News.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, News);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<News>>
                    {
                        Data = News.Data,
                        Message = News.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<News>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //single News get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<News>>> GetNewsById(int Id)
        {
            try
            {
                //Getting a single News by Id using the service
                var News = await _NewsService.GetNewsbyIdAsync(Id);

                //checks for operation failure
                if (News.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, News);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<News>
                    {
                        Data = News.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<News>>> UpdateNews(int Id, NewsInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting News using the Id
                    var NewsFromDatabase = await _NewsService.GetNewsbyIdAsync(Id);

                    //checks for operation failure
                    if (!NewsFromDatabase.Success)
                    {
                        return new GenericResponse<News>
                        {
                            Data = null,
                            Message = "News to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the News to the new instance
                        NewsFromDatabase.Data.NewsTitle = Input.NewsTitle;
                        NewsFromDatabase.Data.NewsBanner = Input.NewsBanner;
                        NewsFromDatabase.Data.NewsCategory = Input.NewsCategory;
                        NewsFromDatabase.Data.PublishedBy = Input.PublishedBy;
                        NewsFromDatabase.Data.PublishingDate = Input.PublishingDate;
                        NewsFromDatabase.Data.IsSaving = Input.IsSaving;
                        NewsFromDatabase.Data.IsPublihing = Input.IsPublihing;
                        NewsFromDatabase.Data.DateUpdated = DateTime.Now;
                        NewsFromDatabase.Data.Id = Input.CategoryId;

                        //Updating the News using the service
                        var newNews = await _NewsService.UpdateNewsAsync(Id, NewsFromDatabase.Data);

                        //checks for operation failure
                        if (!newNews.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, NewsFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<News>
                            {
                                Data = NewsFromDatabase.Data,
                                Message = newNews.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //News Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<News>>> RemoveItemFromNews(int Id)
        {
            try
            {
                //deleting a News using the service
                var isDeleted = await _NewsService.RemoveFromNewsAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<News>
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
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }
    }
}