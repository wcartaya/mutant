using Common.Constants;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mutants.Business.Dtos;
using Mutants.Business.IServices;
using Mutants.Business.Mutant;
using Mutants.DataAccess.Interfaces;
using Mutants.DataAccess.Repository;
using System;
using System.Threading.Tasks;

namespace Mutants.Business.Services
{
    public class MutantService : IMutantService
    {
        private readonly ILogger<MutantService> _logger;
        private readonly DnaValidator _dnaValidator;
        private readonly IDataRepository<Dna> _dataRepository;
        private readonly IStatsRepository _statsRepository;
        private readonly IMemoryCache _cache;
        public MutantService(
            ILogger<MutantService> logger, 
            DnaValidator dnaValidator,
            IDataRepository<Dna> dataRepository,
            IStatsRepository statsRepository,
            IMemoryCache cache)
        {
            _logger = logger;
            _dnaValidator = dnaValidator;
            _dataRepository = dataRepository;
            _statsRepository = statsRepository;
            _cache = cache;
        }

        public async Task<StatsDto> GetStats()
        {
            if (_cache.TryGetValue(CacheConstants.Stats, out StatsDto stats))
            {
                return stats;
            }
            stats = await _statsRepository.GetStats();
            _cache.Set(CacheConstants.Stats, stats, TimeSpan.FromSeconds(5));
            return stats;
        }

        public async Task<bool> IsMutant(string[] dna)
        {
            _logger.LogDebug("Validating dna.");            
            bool isMutant = await _dnaValidator.IsMutant(dna);
            _dataRepository.Add(new Dna() { DnaString = dna, IsMutant = isMutant });
            return isMutant;
        }        
    }     
}
