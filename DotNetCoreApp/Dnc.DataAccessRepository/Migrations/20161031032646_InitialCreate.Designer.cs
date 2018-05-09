using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dnc.DataAccessRepository.Context;

namespace Dnc.DataAccessRepository.Migrations
{
    [DbContext(typeof(EntityDbContext))]
    [Migration("20161031032646_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dnc.Entities.Application.ApplicationGroup", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Decription")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("ID");

                    b.ToTable("ApplicationGroups");
                });

            modelBuilder.Entity("Dnc.Entities.Application.ApplicationUser", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApplicationGroupID");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Password")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("ID");

                    b.HasIndex("ApplicationGroupID");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Dnc.Entities.Article.Article", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ArticleTypeID");

                    b.Property<string>("Content");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("PublishDateTime");

                    b.Property<string>("SubName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("TitleImage")
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("ID");

                    b.HasIndex("ArticleTypeID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Dnc.Entities.Article.ArticleType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Decription")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("SortCode")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.ToTable("ArticleTypes");
                });

            modelBuilder.Entity("Dnc.Entities.Organization.Person", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<bool>("Sex");

                    b.Property<string>("SortCode")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Dnc.Entities.Application.ApplicationUser", b =>
                {
                    b.HasOne("Dnc.Entities.Application.ApplicationGroup", "Group")
                        .WithMany()
                        .HasForeignKey("ApplicationGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dnc.Entities.Article.Article", b =>
                {
                    b.HasOne("Dnc.Entities.Article.ArticleType", "ArticleType")
                        .WithMany()
                        .HasForeignKey("ArticleTypeID");
                });
        }
    }
}
