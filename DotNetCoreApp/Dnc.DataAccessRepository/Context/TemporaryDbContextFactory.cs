using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dnc.DataAccessRepository.Context
{
  // public class TemporaryDbContextFactory : DbContextOptionsBuilder<EntityDbContext>
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
    {

        /*public EntityDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<EntityDbContext>();
            builder.UseSqlServer("Server=SC-201703032024;Initial Catalog=CPMD_Team20140208; uid=sa;pwd=123;MultipleActiveResultSets=True");
            return new EntityDbContext(builder.Options);
        }*/

        public EntityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EntityDbContext>();
            builder.UseMySQL("Server=47.98.212.255;port=3306;database=cpmd_team20140208;uid=zjg;pwd=123456;SslMode=None");
            return new EntityDbContext(builder.Options);
        }
    }
}
