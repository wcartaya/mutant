using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Mutants.Business.Mutant;
using Mutants.Business.Services;
using Mutants.DataAccess.Interfaces;
using System.Threading.Tasks;
using Test.Util.Stub;
using Test.Util.Stub.Entitys;
using Xunit;

namespace Services.Test
{
    public class MutantServiceTest
    {
        private readonly MutantService mutantService;
        private readonly Mock<ILogger<MutantService>> mockLogger;
        private readonly Mock<IDnaValidator> mockDnaValidator;
        private readonly Mock<IDataRepository<Dna>> mockDataRepository;
        private readonly Mock<IStatsRepository> mockStatsRepository;
        private readonly Mock<IMemoryCache> mockMemoryCache;

        public MutantServiceTest()
        {
            mockLogger = new Mock<ILogger<MutantService>>();
            mockDnaValidator = new Mock<IDnaValidator>();
            mockDataRepository = new Mock<IDataRepository<Dna>>();
            mockStatsRepository = new Mock<IStatsRepository>();
            mockMemoryCache = new Mock<IMemoryCache>();

            mutantService = new MutantService(
                mockLogger.Object,
                mockDnaValidator.Object,
                mockDataRepository.Object,
                mockStatsRepository.Object,
                mockMemoryCache.Object
                );
        }

        [Fact]
        public void Is_Mutant_shloud_return_true()
        {
            // Arrange
            Dna dnaStub = new DnaStub().GetOnPosition(0);
            string[] dnaString = new stringArrayStub().GetOnPosition(0);
            mockDataRepository.Setup(x => x.Add(dnaStub));
            mockDnaValidator.Setup(x => x.IsMutant(dnaString)).Returns(Task.FromResult(true));
            //Act
            var result = mutantService.IsMutant(dnaString);
            //Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void Is_Mutant_shloud_return_false()
        {
            // Arrange
            string[] dnaString = new stringArrayStub().GetOnPosition(1);
            //Act
            var result = mutantService.IsMutant(dnaString);
            //Assert
            Assert.False(result.Result);
        }
    }
}
