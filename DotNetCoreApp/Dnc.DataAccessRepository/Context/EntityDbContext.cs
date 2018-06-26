using Dnc.Entities.Application;
using Dnc.Entities.Article;
using Dnc.Entities.Organization;
using Microsoft.EntityFrameworkCore;

namespace Dnc.DataAccessRepository.Context
{
    public class EntityDbContext:DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options){}

        public DbSet<Person> Persons { get; set; }
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<NewsArticle> Articles { get; set; }
        public DbSet<Label> Label { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
               /* optionsBuilder.UseSqlServer(
                    @"Server=SC-201804102054;Initial Catalog=CPMD_Team20140208; uid=sa;pwd=123;MultipleActiveResultSets=True");
                base.OnConfiguring(optionsBuilder);*/
                optionsBuilder.UseMySQL(@"Server=47.98.212.255;port=3306;database=cpmd_team20140206;uid=zjg;pwd=123456;SslMode=None");
                base.OnConfiguring(optionsBuilder);

            }
        }
    }
}
