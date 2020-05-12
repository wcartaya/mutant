using Microsoft.AspNetCore.Mvc;
using Moq;
using Mutants.Business.Dtos;
using Mutants.Business.IServices;
using Mutants.Controllers;
using System;
using System.Threading.Tasks;
using Test.Util.Stub.Dtos;
using Xunit;

namespace API.Test
{
    public class MutantsControllerTest
    {
        private Mock<IMutantService> mutantServiceMock;
        private MutantsController mutantController;
        public MutantsControllerTest()
        {
            mutantServiceMock = new Mock<IMutantService>();
            mutantController = new MutantsController(mutantServiceMock.Object);
        }

        [Fact]
        public async Task Post_StatusCode200_Is_Mutant()
        {
            // Arrange
            DnaDto dnaDto = new DnaDtoStub().GetOnPosition(0);
            mutantController.ModelState.Clear(); // Sets ModelState.IsValid = true
            mutantServiceMock.Setup(t => t.IsMutant(dnaDto.dna)).ReturnsAsync(true);

            // Act
            var actionResult = await mutantController.Post(dnaDto);

            // Assert 
            Assert.NotNull(actionResult);
            Assert.Equal(typeof(OkResult), actionResult.GetType());
            var okResult = actionResult as OkResult;
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Post_StatusCode403_Is_Human()
        {
            // Arrange
            DnaDto dnaDto = new DnaDtoStub().GetOnPosition(1);
            mutantController.ModelState.Clear(); // Sets ModelState.IsValid = true
            mutantServiceMock.Setup(t => t.IsMutant(dnaDto.dna)).ReturnsAsync(false);

            // Act
            var actionResult = await mutantController.Post(dnaDto);

            // Assert 
            Assert.NotNull(actionResult);
            var result = (Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult)actionResult;            
            Assert.Equal("403", result.StatusCode.ToString());
        }

        [Fact]
        public async Task Post_ModelState_False_BadRequest()
        {
            // Arrange
            DnaDto dnaDto = new DnaDtoStub().GetOnPosition(0);
            mutantServiceMock.Setup(t => t.IsMutant(dnaDto.dna)).ReturnsAsync(true);

            mutantController.ModelState.AddModelError("test", "test"); // Sets ModelState.IsValid = false
            mutantServiceMock.Setup(t => t.IsMutant(dnaDto.dna)).ReturnsAsync(true);

            // Act
            var actionResult = await mutantController.Post(dnaDto);

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(typeof(BadRequestObjectResult), actionResult.GetType());
            var badRequest = actionResult as BadRequestObjectResult;
            Assert.Equal(400, badRequest.StatusCode);
        }
    }
}
