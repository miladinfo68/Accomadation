using Accomadation.Infrastructurs.Connectins;
using Accomadation.Infrastructurs.Interfaces.IServices;
using Accomadation.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Accomadation.Services
{
    public class UserService : IUserService
    {
        private string _conStr ;       
        public UserService(IConfiguration config)
        {            
            _conStr = _conStr ?? config.GetConnectionString("Accomadation");
        }
        public async Task<IEnumerable<User>> GetAsync()
        {
            const string sql = @"Select * from [User]";
            using var con = new SqlConnection(_conStr);
            var res=await con.QueryAsync<User>(sql);
            return res.ToList();
        }
    }
}
