using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tenancy_Contract.DataAccessLayer;
using TenancyContract.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Threading;
using System.Data.Common;

namespace TenancyContract.Identity
{
    public class HouseOwnerUserStore : IUserStore<HouseOwner>, IUserPasswordStore<HouseOwner>
    {
        private readonly TenancyContractDbContext _db;
        public HouseOwner houseOwner = new HouseOwner();
        public HouseOwnerUserStore(TenancyContractDbContext db)
        {
            _db = db;
        }
 
        public async Task<IdentityResult> CreateAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            //TODO: Convert into Raw SQL
            houseOwner.Id = user.Id;
            houseOwner.Name = user.Name;
            houseOwner.Email = user.Email;
            houseOwner.Mobile = user.Mobile;
            houseOwner.NID = user.NID;
            houseOwner.PasswordHash = user.PasswordHash;
            _db.HouseOwners.Add(houseOwner);
           await _db.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<HouseOwner> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<HouseOwner>(
                   "select * From HouseOwners where Id = @id",
                   new { id = userId });
            }
        }

        public async Task<HouseOwner> FindByNameAsync(string userNid, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<HouseOwner>(
                   "select * From HouseOwners where NID = @nid",
                   new { nid = userNid });
            }
        }
        //Email Find
        public async Task<HouseOwner> FindByEmailAsync(string userEmail, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<HouseOwner>(
                   "select * From HouseOwners where Email = @email",
                   new { email = userEmail });
            }

        }
        //Set Email
        public Task SetUserEmailAsync(HouseOwner user, string userEmail, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            user.Email = userEmail;
            return Task.CompletedTask;
        }

        public Task<string> GetUserEmailAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(null);
        }

        public Task<string> GetPasswordHashAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NID);
        }

        public Task<bool> HasPasswordAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null); ;
        }

        public Task SetNormalizedUserNameAsync(HouseOwner user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(null);
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
        public Task SetPasswordHashAsync(HouseOwner user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(HouseOwner user, string userNid, CancellationToken cancellationToken)
        {
            user.NID = userNid;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(HouseOwner user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
