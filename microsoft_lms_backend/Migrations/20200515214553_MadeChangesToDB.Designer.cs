// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microsoft_lms_backend.Data;

namespace microsoft_lms_backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200515214553_MadeChangesToDB")]
    partial class MadeChangesToDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("microsoft_lms_backend.Models.Bundles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.OfferList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OfferDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("OfferList");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.Offers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfferName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Billing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.SupportTickets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CaseOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateResolved")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Response")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SupportTickets");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.Templates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.ArticleCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("ArticleCategory");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.Articles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArticleCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ArticleCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ArticleTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublihing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSaving")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArticleCategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CourseCategory");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoursesId")
                        .HasColumnType("int");

                    b.Property<string>("ModuleTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseModule");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublihing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSaving")
                        .HasColumnType("bit");

                    b.Property<int?>("LearningTrackId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LearningTrackId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.LearningTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("TrackBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseCategoryId");

                    b.ToTable("LearningTrack");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.ModuleListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseModuleId")
                        .HasColumnType("int");

                    b.Property<string>("CourseVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseModuleId");

                    b.ToTable("ModuleListItem");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublihing")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSaving")
                        .HasColumnType("bit");

                    b.Property<string>("NewsBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewsCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewsTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.ProductUpload.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.ProductUpload.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductTypeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.WebinarModels.Webinar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Webinar");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.WebinarModels.WebinarAttendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttendeeEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendeeOcupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendeePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WebinarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WebinarId");

                    b.ToTable("WebinarAttendee");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.OfferList", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.Offers", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.Offers", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.Products", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.ArticleCategory", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.Articles", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.ArticleCategory", "ArticleCategory")
                        .WithMany()
                        .HasForeignKey("ArticleCategoryId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseCategory", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseModule", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.Courses", "Courses")
                        .WithMany()
                        .HasForeignKey("CoursesId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.Courses", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.LearningTrack", "LearningTrack")
                        .WithMany()
                        .HasForeignKey("LearningTrackId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.LearningTrack", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseCategory", "CourseCategory")
                        .WithMany()
                        .HasForeignKey("CourseCategoryId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.ModuleListItem", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.CourseModule", "CourseModule")
                        .WithMany()
                        .HasForeignKey("CourseModuleId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.CategoryManagementModel.News", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.CategoryManagementModel.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.ProductUpload.Product", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.ProductUpload.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId");
                });

            modelBuilder.Entity("microsoft_lms_backend.Models.v1.WebinarModels.WebinarAttendee", b =>
                {
                    b.HasOne("microsoft_lms_backend.Models.v1.WebinarModels.Webinar", "Webinar")
                        .WithMany()
                        .HasForeignKey("WebinarId");
                });
#pragma warning restore 612, 618
        }
    }
}
