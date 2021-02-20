
using Accomadation.DTOs;
using Accomadation.Models;
using System.Threading.Tasks;

namespace Accomadation.Infrastructures.Interfaces.IRepositories
{
    public interface IBookingRepository : IBaseRepository<BookingReadDto>
    {
        Task<string> AddAsync(Booking item);
    }
}
