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
    public class ArticleService : IArticle
    {
        public readonly ApplicationDbContext _dbcontext;

        public ArticleService()
        {

        }
        public ArticleService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<GenericResponse<ArticleCategory>> CreateNewArticleCategoryAsync(ArticleCategory Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if content category exist
                    var category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    var categoryName = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(w => w.CategoryName == Input.CategoryName);
                    if (category == null)
                    {
                        return new GenericResponse<ArticleCategory>
                        {
                            Data = Input,
                            Message = "Category Id does not exit",
                            Success = false
                        };
                    }
                    else if (categoryName != null)
                    {
                        return new GenericResponse<ArticleCategory>
                        {
                            Data = Input,
                            Message = $"Article Category name '{Input.CategoryName}' already not exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var ArticleCategoryUpload = new ArticleCategory
                        {
                            CategoryName = Input.CategoryName,
                            CategoryBanner = Input.CategoryBanner,
                            Category = category
                        };
                        //If input is not null, add it to database
                        await _dbcontext.ArticleCategory.AddAsync(ArticleCategoryUpload);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<ArticleCategory>
                        {
                            Data = ArticleCategoryUpload,
                            Message = "Article Category added successfully",
                            Success = true
                        };
                    }
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<ArticleCategory>>> GetAllArticleCategoryAsync()
        {
            try
            {
                //Getting all article category from database
                var article = await _dbcontext.ArticleCategory.ToListAsync();
                if (article.Count == 0)
                {
                    return new GenericResponse<IEnumerable<ArticleCategory>>
                    {
                        Data = null,
                        Message = "Article Category is empty",
                        Success = true
                    };
                }
                //returning all article category gotten from database
                return new GenericResponse<IEnumerable<ArticleCategory>>
                {
                    Data = article,
                    Message = $"successfully gets {article.Count} article(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<ArticleCategory>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }


        public async Task<GenericResponse<ArticleCategory>> GetArticleCategorybyIdAsync(int Id)
        {
            try
            {
                //find the article category by id from the database
                var articleCategoryFromDatabase = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(w => w.Id == Id);
                //var Category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Id == articleCategoryFromDatabase.Category.Id);
                //var articleCategory = new ArticleCategory
                //{
                //    Id = articleCategoryFromDatabase.Id,
                //    CategoryName = articleCategoryFromDatabase.CategoryName,
                //    CategoryBanner = articleCategoryFromDatabase.CategoryBanner,
                //    Category = Category
                //};
                //If not found
                if (articleCategoryFromDatabase == null)
                {
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = null,
                        Message = "Article Category does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article category
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = articleCategoryFromDatabase,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
      
        public async Task<GenericResponse<ArticleCategory>> UpdateArticleCategoryAsync(int Id, ArticleCategory Input)
        {
            try
            {
                //find the article category by it id from the database
                var articleCategory = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(w => w.Id == Id);
                var Category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Id == Input.Id);

                //If not found
                if (articleCategory == null)
                {
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = null,
                        Message = "Article category to be updated does not exist",
                        Success = false
                    };
                }
                else if (Category==null)
                {
                    return new GenericResponse<ArticleCategory>
                    {
                        Data = null,
                        Message = "Category Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var articleCategoryUpdate = new ArticleCategory
                    {
                        CategoryName =  Input.CategoryName,
                        CategoryBanner = Input.CategoryBanner,
                        Category = Category
                    };
                    //if both found, update with the new changes and save changes
                    var result = _dbcontext.ArticleCategory.Update(articleCategoryUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ArticleCategory>
                    {
                        Data = articleCategoryUpdate,
                        Message = "Article category Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<ArticleCategory>> RemoveFromArticleCategoryAsync(int Id)
        {
            try
            {
                //find the article category by id from the database
                var articleCategoryToBeRemoved = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(c => c.Id == Id);

                //If found, remove it from database and save changes
                if (articleCategoryToBeRemoved != null)
                {
                    _dbcontext.Remove(articleCategoryToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ArticleCategory>
                    {
                        Data = null,
                        Message = "article category has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<ArticleCategory>
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
                return new GenericResponse<ArticleCategory>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Article Service

        //Service implementation for creating new Article
        public async Task<GenericResponse<Articles>> CreateNewArticleAsync(Articles Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<Articles>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if article category exist
                    var articleCategory = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    var articleTitle = await _dbcontext.Articles.FirstOrDefaultAsync(w => w.ArticleTitle == Input.ArticleTitle);

                    if (articleCategory == null)
                    {
                        return new GenericResponse<Articles>
                        {
                            Data = Input,
                            Message = "Article Category Id does not exit",
                            Success = false
                        };
                    }
                    else if (articleTitle != null)
                    {
                        return new GenericResponse<Articles>
                        {
                            Data = Input,
                            Message = $"Article titled '{Input.ArticleTitle}' already exit",
                            Success = false
                        };
                    }
                    else
                    {
                        //If article category is not null, create a new instance or article from the input. 
                        var article = new Articles
                        {
                            ArticleTitle = Input.ArticleTitle,
                            Banner = Input.Banner,
                            PublicationDate = Input.PublicationDate,
                            DateCreated = DateTime.Now,
                            IsSaving = Input.IsSaving,
                            IsPublihing = Input.IsPublihing,
                            ArticleCategory = articleCategory
                        };
                        //add it to database
                        await _dbcontext.Articles.AddAsync(article);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<Articles>
                        {
                            Data = article,
                            Message = "Article added successfully",
                            Success = true
                        };
                    }
                   
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all Articles
        public async Task<GenericResponse<IEnumerable<Articles>>> GetAllArticleAsync()
        {
            try
            {
                //Getting all article from database
                var article = await _dbcontext.Articles.ToListAsync();
                if (article.Count == 0)
                {
                    return new GenericResponse<IEnumerable<Articles>>
                    {
                        Data = null,
                        Message = "Article is empty",
                        Success = true
                    };
                }
                //returning all article gotten from database
                return new GenericResponse<IEnumerable<Articles>>
                {
                    Data = article,
                    Message = $"successfully gets {article.Count} article(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Articles>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting Article by id
        public async Task<GenericResponse<Articles>> GetArticlebyIdAsync(int Id)
        {
            try
            {
                //find the article by id from the database
                var article = await _dbcontext.Articles.FirstOrDefaultAsync(w => w.Id == Id);

                //If not found
                if (article == null)
                {
                    return new GenericResponse<Articles>
                    {
                        Data = null,
                        Message = "Article does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<Articles>
                    {
                        Data = article,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Articles>> UpdateArticleAsync(int Id, Articles Input)
        {
            try
            {
                //find the article by it id from the database
                var article = await _dbcontext.Articles.FirstOrDefaultAsync(p => p.Id == Id);

                var articleCategory = await _dbcontext.ArticleCategory.FirstOrDefaultAsync(p => p.Id == Input.Id);

                //If not found
                if (article == null)
                {
                    return new GenericResponse<Articles>
                    {
                        Data = null,
                        Message = "Article to be updated does not exist",
                        Success = false
                    };
                }
                else if (articleCategory == null)
                {
                    return new GenericResponse<Articles>
                    {
                        Data = null,
                        Message = "Article Category Id does not exist",
                        Success = false
                    };
                }
                else
                {
                     var articleUpdate = new Articles
                     {
                         ArticleTitle = Input.ArticleTitle,
                         Banner = Input.Banner,
                         PublicationDate = Input.PublicationDate,
                         DateCreated = DateTime.Now,
                         IsSaving = Input.IsSaving,
                         IsPublihing = Input.IsPublihing,
                         ArticleCategory = articleCategory
                     };
                    //if found, update with the new changes and save changes
                    var result = _dbcontext.Articles.Update(articleUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Articles>
                    {
                        Data = articleUpdate,
                        Message = "Article Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for removing Article 
        public async Task<GenericResponse<Articles>> RemoveFromArticleAsync(int Id)
        {
            try
            {
                //find the article by id from the database
                var articleToBeRemoved = await _dbcontext.Articles.FirstOrDefaultAsync(c => c.Id == Id);

                //If found, remove it from database and save changes
                if (articleToBeRemoved != null)
                {
                    _dbcontext.Remove(articleToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Articles>
                    {
                        Data = null,
                        Message = "article has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<Articles>
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
                return new GenericResponse<Articles>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting Article by article's category
        public async Task<GenericResponse<IEnumerable<Articles>>> GetArticlesbyArticleCategoryAsync(int ArticleCategoryId)
        {
            try
            {
                //find the article by article category Id from the database
                var articles = await _dbcontext.Articles.Where(w => w.ArticleCategory.Id == ArticleCategoryId).ToListAsync();

                //If not found
                if (articles == null)
                {
                    return new GenericResponse<IEnumerable<Articles>>
                    {
                        Data = null,
                        Message = "There is no article for this article category",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<Articles>>
                    {
                        Data = articles,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
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
