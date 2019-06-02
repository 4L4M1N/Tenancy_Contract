using Microsoft.EntityFrameworkCore;
using TenancyContract.Entities;
using Web.Entities;

namespace Tenancy_Contract.DataAccessLayer
{
    //Db context Class. Responsible for accessing data
    public class TenancyContractDbContext : DbContext
    {
        public TenancyContractDbContext()
        {
        }

        public TenancyContractDbContext(DbContextOptions<TenancyContractDbContext> options)
            : base(options)
        {

        }
      
        public DbSet<LogInInfo> LogInInfos { get; set; }
        public DbSet<HouseOwner> HouseOwners { get; set; }
         public DbSet<Moderator> Moderators { get; set; }
        
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AbuseReport> AbuseReports { get; set; }
        public DbSet<Tenant> Tenants {get; set;}
        public DbSet<House> Houses { get; set; }
        public DbSet<Division> Divisions {get; set;}
        public DbSet<District> Districts {get; set;}
        public DbSet<Thana> Thanas {get; set;}
        public DbSet<Notification> Notifications {get;set;} 
        public DbSet<NotificationApplicationUser> NotificationApplicationUsers {get;set;}
        
    }
}