using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class NewsService : INews
    {
        public readonly ApplicationDbContext _dbcontext;

        public NewsService()
        {

        }
        public NewsService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Service implementation for creating news
        public async Task<GenericResponse<News>> CreateNewsAsync(News Input)
        {

            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<News>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if category exit
                    var category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    if (category == null)
                    {
                        return new GenericResponse<News>
                        {
                            Data = Input,
                            Message = "Category Id does not exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var news = new News
                        {
                            NewsTitle = Input.NewsTitle,
                            NewsBanner = Input.NewsBanner,
                            NewsCategory = Input.NewsCategory,
                            PublishedBy = Input.PublishedBy,
                            PublishingDate = Input.PublishingDate,
                            IsSaving = Input.IsSaving,
                            IsPublihing = Input.IsPublihing,
                            DateUpdated = DateTime.Now,
                            Category = category
                        };
                        //If input is not null, add it to database
                        await _dbcontext.News.AddAsync(news);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<News>
                        {
                            Data = news,
                            Message = "news added successfully",
                            Success = true
                        };
                    }
                    
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<News>
                {
                    Data = Input,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all news
        public async Task<GenericResponse<IEnumerable<News>>> GetAllNewsAsync()
        {
            try
            {
                //Getting all news from database
                var news = await _dbcontext.News.ToListAsync();
                if (news.Count == 0)
                {
                    return new GenericResponse<IEnumerable<News>>
                    {
                        Data = null,
                        Message = "news is empty",
                        Success = true
                    };
                }
                //returning all news gotten from database
                return new GenericResponse<IEnumerable<News>>
                {
                    Data = news,
                    Message = $"successfully gets {news.Count} news",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<News>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single news
        public async Task<GenericResponse<News>> GetNewsbyIdAsync(int Id)
        {
            try
            {
                //find the news by id from the database
                var news = await _dbcontext.News.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (news == null)
                {
                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "news does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the news
                    return new GenericResponse<News>
                    {
                        Data = news,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a news
        public async Task<GenericResponse<News>> UpdateNewsAsync(int Id, News Input)
        {
            try
            {
                //find the news by it id from the database
                var news = await _dbcontext.News.FirstOrDefaultAsync(x => x.Id == Id);
                //find the category by provided id from the database
                var category = await _dbcontext.Category.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If news not found
                if (news == null)
                {
                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "news to be updated does not exist",
                        Success = false
                    };
                }
                else if (category == null)
                {
                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "Category Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var newsUpdate = new News
                    {
                        NewsTitle = Input.NewsTitle,
                        NewsBanner = Input.NewsBanner,
                        NewsCategory = Input.NewsCategory,
                        PublishedBy = Input.PublishedBy,
                        PublishingDate = Input.PublishingDate,
                        IsSaving = Input.IsSaving,
                        IsPublihing = Input.IsPublihing,
                        DateUpdated = DateTime.Now,
                        Category = category
                    };
                    //if both found, update with the new changes and save changes
                    var result = _dbcontext.News.Update(newsUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<News>
                    {
                        Data = newsUpdate,
                        Message = "news Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<News>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a news
        public async Task<GenericResponse<News>> RemoveFromNewsAsync(int Id)
        {
            try
            {
                //find the news by id from the database
                var newsToBeRemoved = await _dbcontext.News.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (newsToBeRemoved != null)
                {
                    _dbcontext.Remove(newsToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "news has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<News>
                    {
                        Data = null,
                        Message = "Not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
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
