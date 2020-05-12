using Mutants.Business.Dtos;
using System.Threading.Tasks;

namespace Mutants.DataAccess.Interfaces
{
    public interface IStatsRepository
    {
        Task<StatsDto> GetStats();
    }
}
