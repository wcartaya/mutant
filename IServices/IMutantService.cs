using Mutants.Business.Dtos;
using System;
using System.Threading.Tasks;

namespace Mutants.Business.IServices
{
    public interface IMutantService
    {
        Task<bool> IsMutant(String[] dna);
        Task<StatsDto> GetStats();
    }
}
