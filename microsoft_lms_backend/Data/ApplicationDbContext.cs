using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Models.v1.WebinarModels;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using microsoft_lms_backend.Models.v1.ProductUpload;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Models.v1.BusinessProfileModels;

namespace microsoft_lms_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        //  public DbSet<User> User { get; set; }
        public DbSet<BusinessExpertise> BusinessExpertise { get; set; }
        public DbSet<BusinessProfile> BusinessProfile { get; set; }
        public DbSet<BusinessContact> BusinessContact { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<OfferList> OfferList { get; set; }
        public DbSet<Templates> Templates { get; set; }
        public DbSet<SupportTickets> SupportTickets { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Webinar> Webinar { get; set; }
        public DbSet<WebinarAttendee> WebinarAttendee { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ArticleCategory> ArticleCategory { get; set; }
        public DbSet<CourseCategory> CourseCategory { get; set; }
        public DbSet<LearningTrack> LearningTrack { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<CourseModule> CourseModule { get; set; }
        public DbSet<ModuleListItem> ModuleListItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

    }
}
