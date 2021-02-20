using Accomadation.DTOs;
using AutoMapper;
using System;

namespace Accomadation.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            
            CreateMap<BookingReadDto, BookingReadDto>()
                 .ForMember(b=>b.StartDate,opt=>opt.MapFrom(br=> Convert.ToDateTime(br.StartDate).Date.ToString("d")))         
                 .ForMember(b => b.EndDate, opt => opt.MapFrom(br => Convert.ToDateTime(br.EndDate).Date.ToString("d")));

            CreateMap<RoomReadDto, RoomReadDto>()
                .ForMember(r => r.StartDate, opt => opt.MapFrom(rm => string.IsNullOrEmpty(rm.StartDate) ? null: Convert.ToDateTime(rm.StartDate).Date.ToString("d")))
                .ForMember(r => r.EndDate, opt => opt.MapFrom(rm => string.IsNullOrEmpty(rm.EndDate) ? null :  Convert.ToDateTime(rm.EndDate).Date.ToString("d")));

        }
    }
}
