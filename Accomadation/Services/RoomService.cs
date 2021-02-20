using Accomadation.DTOs;
using Accomadation.Infrastructurs.Connectins;
using Accomadation.Infrastructurs.Interfaces.IServices;
using Accomadation.Models;
using AutoMapper;
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
    public class RoomService : IRoomService
    {
        private string _conStr ;
        private readonly IMapper _mapper;
        public RoomService(IConfiguration config , IMapper mapper)
        {            
            _conStr = _conStr ?? config.GetConnectionString("Accomadation");
            _mapper = mapper;
        }

        public async Task<IEnumerable<Room>> GetAsync()
        {
            const string sql = @"Select * from Room";
            using var con = new SqlConnection(_conStr);
            var res = await con.QueryAsync<Room>(sql);
            return res.ToList();
        }

        public async Task<IEnumerable<RoomReadDto>> GetByDateAsync(string date = null)
        {
            using var con = new SqlConnection(_conStr);
            var res = await con.QueryAsync<RoomReadDto>("dbo.sp_freeRooms", date, commandType: CommandType.StoredProcedure);
            var mappedList = _mapper.Map<IEnumerable<RoomReadDto>, IEnumerable<RoomReadDto>>(res);
            return mappedList;
        }
        

    }
}
