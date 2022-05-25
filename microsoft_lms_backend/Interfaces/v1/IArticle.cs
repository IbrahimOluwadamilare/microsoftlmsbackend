using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IArticle
    {
        //Article category
        Task<GenericResponse<ArticleCategory>> CreateNewArticleCategoryAsync(ArticleCategory Input);
        Task<GenericResponse<IEnumerable<ArticleCategory>>> GetAllArticleCategoryAsync();
        Task<GenericResponse<ArticleCategory>> GetArticleCategorybyIdAsync(int Id);
        Task<GenericResponse<ArticleCategory>> UpdateArticleCategoryAsync(int Id, ArticleCategory Input);
        Task<GenericResponse<ArticleCategory>> RemoveFromArticleCategoryAsync(int Id);


        //Article
        Task<GenericResponse<Articles>> CreateNewArticleAsync(Articles Input);
        Task<GenericResponse<IEnumerable<Articles>>> GetAllArticleAsync();
        Task<GenericResponse<Articles>> GetArticlebyIdAsync(int Id);
        Task<GenericResponse<Articles>> UpdateArticleAsync(int Id, Articles Input);
        Task<GenericResponse<Articles>> RemoveFromArticleAsync(int Id);
        Task<GenericResponse<IEnumerable<Articles>>> GetArticlesbyArticleCategoryAsync(int ArticleCategoryId);
    }
}
