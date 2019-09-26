using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dnc.DataAccessRepository.Context
{
    public class TemporaryDbContextFactory : DbContextOptionsBuilder<EntityDbContext>
    //public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
    {

        //public EntityDbContext Create(DbContextFactoryOptions options)
        //{
        //    var builder = new DbContextOptionsBuilder<EntityDbContext>();
        //    builder.UseSqlServer("Server=.;Initial Catalog=CPMD_Team20140207; uid=sa;pwd=~zjg10077854;MultipleActiveResultSets=True");
        //    return new EntityDbContext(builder.Options);
        //}

        public EntityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EntityDbContext>();
            builder.UseMySql("Server=47.103.6.220;port=3306;database=cpmd_team20140207;charset=utf-8;uid=zjg;pwd=123456;SslMode=None");
            return new EntityDbContext(builder.Options);
        }
    }
}
