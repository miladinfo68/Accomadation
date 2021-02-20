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
using Accomadation.Enums;
using Accomadation.Infrastructurs.Messages;
using Accomadation.DTOs;
using AutoMapper;

namespace Accomadation.Services
{
    public class BookingService : IBookingService
    {
        private string _conStr;
        private readonly IMapper _mapper;
        public BookingService(IConfiguration config, IMapper mapper)
        {
            _conStr = _conStr ?? config.GetConnectionString("Accomadation");
            _mapper = mapper;
        }

        public async Task<string> AddAsync(Booking item)
        {
            using var con = new SqlConnection(_conStr);
            var res = await con.ExecuteScalarAsync("dbo.sp_AddNewBook", item, commandType: CommandType.StoredProcedure);
            var message = "";
            switch (res)
            {
                case (int)ConstantValues.There_Is_No_Empty_Capacity:
                    message = Messages.There_Is_No_Empty_Capacity;
                    break;

                case (int)ConstantValues.StarDate_EndDate_Empty:
                    message = Messages.StarDate_EndDate_Empty;
                    break;

                case (int)ConstantValues.StartDate_GreaterThan_EndDate:
                    message = Messages.StartDate_GreaterThan_EndDate;
                    break;

                case (int)ConstantValues.Success:
                    message = Messages.SuccessMessage;
                    break;

                case (int)ConstantValues.Current_Room_Before_Reserved:
                    message = Messages.Current_Room_Before_Reserved;
                    break;

                case (int)ConstantValues.OverLapRangeDate_Current_And_Past:
                    message = Messages.OverLapRangeDate_Current_And_Past;
                    break;

                case (int)ConstantValues.InvalidSexuality:
                    message = Messages.InvalidSexuality;
                    break;

                default:
                    break;
            }
            return message;
        }

        public async Task<IEnumerable<BookingReadDto>> GetAsync()
        {
            const string sql = @"select distinct u.ID as UserId,u.FullName,case u.Gender when 0 then 'man' else 'woman'end Gender
                                ,r.ID as RoomId, r.Places as Capacity ,b.StartDate , b.EndDate
                                from [User] u
                                join Booking b on b.UserId=u.ID
                                join Room r on b.RoomId=r.ID
                                order by b.EndDate desc";
            using var con = new SqlConnection(_conStr);
            var res = await con.QueryAsync<BookingReadDto>(sql);
            var mappedList = _mapper.Map<IEnumerable<BookingReadDto>, IEnumerable<BookingReadDto>>(res);
            return mappedList.ToList();
        }
    }
}
