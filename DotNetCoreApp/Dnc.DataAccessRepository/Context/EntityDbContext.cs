using Dnc.Entities.Application;
using Dnc.Entities.Article;
using Dnc.Entities.Organization;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

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
                //optionsBuilder.UseSqlServer(
                //    @"Server=.;Initial Catalog=CPMD_Team20140207; uid=sa;pwd=~zjg10077854;MultipleActiveResultSets=True");
                //base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseMySql(@"Server=47.103.6.220;port=3306;database=cpmd_team20140207;uid=zjg;pwd=123456;charset=utf-8;SslMode=None");
                base.OnConfiguring(optionsBuilder);

            }
        }
    }
}
