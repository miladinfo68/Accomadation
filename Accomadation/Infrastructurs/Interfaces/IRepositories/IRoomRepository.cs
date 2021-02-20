
using Accomadation.DTOs;
using Accomadation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accomadation.Infrastructures.Interfaces.IRepositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<IEnumerable<RoomReadDto>> GetByDateAsync(string date=null);
    }
}
