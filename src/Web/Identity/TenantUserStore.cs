using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tenancy_Contract.DataAccessLayer;
using TenancyContract.Entities;
using System.Data.SqlClient;
using Dapper;

namespace Web.Identity
{
    public class TenantUserStore : IUserStore<Tenant>, IUserPasswordStore<Tenant>
    {
        //Responsible for storing data in database for tenant
        private readonly TenancyContractDbContext _db;
        public Tenant tenant = new Tenant();
        public TenantUserStore(TenancyContractDbContext db)
        {
            _db = db;
        }
        //Create Tenant
        public async Task<IdentityResult> CreateAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            //TODO: Converted into raw SQL but NULL problem

            tenant.Id = user.Id;
            tenant.Name = user.Name;
            tenant.Email = user.Email;
            tenant.Mobile = user.Mobile;
            tenant.NID = user.NID;
            tenant.PasswordHash = user.PasswordHash;
            _db.Tenants.Add(tenant);
            await _db.SaveChangesAsync();
            return IdentityResult.Success;

            /*Raw Sql
            using(var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "insert into Tenants([Id]," +
                    "[Name]," +
                    "[Mobile]," +
                    "[Email]," +
                    "[NID]," +
                    "[PasswordHash]) " +
                    "Values(@id,@name,@mobile,@email,@nid,@passwordhash)",
                    new
                    {
                        id = user.Id,
                        name = user.Name,
                        mobile = user.Mobile,
                        email = user.Email,
                        nid = user.NID,
                        passwordhash = user.PasswordHash

                    }
                    );
            }
            return IdentityResult.Success;*/
        }
             
        public Task<IdentityResult> DeleteAsync(Tenant user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public async Task<Tenant> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Tenant>(
                   "select * From Tenants where Id = @id",
                   new {id = userId });
            }
        }

        public async Task<Tenant> FindByNameAsync(string userNid, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Tenant>(
                   "select * From Tenants where NID = @nid",
                   new { nid = userNid });
            }

        }
        //Email Find
        public async Task<Tenant> FindByEmailAsync(string userEmail, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Tenant>(
                   "select * From Tenants where Email = @email",
                   new { email = userEmail });
            }

        }
        //Set Email
        public Task SetUserEmailAsync(Tenant user, string userEmail, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            user.Email = userEmail;
            return Task.CompletedTask;
        }

        public Task<string> GetUserEmailAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult<string>(null);
        }

        public Task<string> GetUserIdAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.NID);
        }

        public Task SetNormalizedUserNameAsync(Tenant user, string normalizedName, CancellationToken cancellationToken)
        {
            //return Task.FromResult<string>(null);
            return Task.FromResult<string>(null);
        }


        public Task SetUserNameAsync(Tenant user, string userNid, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            user.NID = userNid;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Tenant user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;" +
                "Database=TenancyContract;" +
                "Trusted_Connection=True;");
            connection.Open();
            return connection;
            //"Data Source=(localdb)\\MSSQLLocalDB;Database=TenancyContract;Integrated Security=True;Trusted_Connection=True;"
        }

        public Task SetPasswordHashAsync(Tenant user, string passwordHash, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Tenant user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}