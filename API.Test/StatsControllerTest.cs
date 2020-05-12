using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mutants.Business.Dtos;
using Mutants.Business.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Util.Stub.Dtos;
using Xunit;

namespace API.Test
{
    public class StatsControllerTest
    {
        private Mock<IMutantService> mutantServiceMock;
        private StatsController statsController;
        public StatsControllerTest()
        {
            mutantServiceMock = new Mock<IMutantService>();
            statsController = new StatsController(mutantServiceMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAll_ShouldReturnStatics()
        {
            // Arrange
            StatsDto statsDto = new StatsDtoStub().GetOnPosition(0);
            mutantServiceMock.Setup(x => x.GetStats()).ReturnsAsync(statsDto);

            // Act
            IActionResult result = await statsController.All();

            // Assert
            Assert.Equal(typeof(OkObjectResult), result.GetType());
            OkObjectResult okResult = (OkObjectResult)result;
            Assert.Equal(typeof(StatsDto), okResult.Value.GetType());
            var resultValues = (StatsDto)okResult.Value;
            Assert.Equal(resultValues, statsDto);
        }
    }
}
