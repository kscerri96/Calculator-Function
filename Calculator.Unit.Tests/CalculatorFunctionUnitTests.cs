using CalculatorFunction.Helpers;
using CalculatorFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Unit.Tests
{
    [TestClass]
    public class CalculatorFunctionUnitTests
    {

        [Fact]
        public async Task InvalidRequest_InstructionFileEmpty()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns(string.Empty);

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(400, objectResult?.StatusCode.Value);
        }
        
        [Theory]
        [InlineData("add 2\r\nmultiply 3\r\napply 3")]
        [InlineData("add 10\r\nmultiply 2\r\nsubtract 5\r\napply 2")]
        public async Task ValidRequest(string instruction)
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns(instruction);

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);

        }

        [Fact]
        public async Task ResultasExpected()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();

            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add 2\r\nmultiply 3\r\napply 3");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var request = new CalculatorResponse
            {
                Result = 15
            };

            var objectResult = result as ObjectResult;
            var value = objectResult.Value as CalculatorResponse;

            Xunit.Assert.Equal(value.Result, request.Result);

        }

        [Fact]
        public async Task InvalidRequest_KeyWordNotValid()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instruction.txt")).Returns("round 10\r\nmultiply 3\r\nsubtract 5\r\napply 2");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(400, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task InvalidRequest_InstructionFileNotFound()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instruction.txt"));

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(400, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task HandleDevideBy0()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add 10\r\nmultiply 3\r\nsubtract 5\r\ndivide 0\r\napply 2");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);
            float zero = 0;

            var request = new CalculatorResponse
            {
                Result = 1 / zero
            };

            var objectResult = result as ObjectResult;
            var value = objectResult.Value as CalculatorResponse;

            Xunit.Assert.Equal(value.Result, request.Result);
            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task HandleCapitalKeyword()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();

            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("ADD 2\r\nmultiply 3\r\napply 3");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var request = new CalculatorResponse
            {
                Result = 15
            };

            var objectResult = result as ObjectResult;
            var value = objectResult.Value as CalculatorResponse;

            Xunit.Assert.Equal(value.Result, request.Result);
            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task HandleInstructionWithNoNumber()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();

            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add 2\r\nmultiply 3\r\napply");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(400, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task HandleExtraSpacesBetweenKeyworAndNumber()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();

            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add  10\r\nmultiply 2\r\nsubtract 5\r\napply 2");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var request = new CalculatorResponse
            {
                Result = 19
            };

            var objectResult = result as ObjectResult;

            var value = objectResult.Value as CalculatorResponse;

            Xunit.Assert.Equal(value.Result, request.Result);
            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);
        }

        [Fact]
        public async Task ApplyKeywordMissing()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();

            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add 10\r\nmultiply 2\r\nsubtract 5\r\nadd 2");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var request = new CalculatorResponse
            {
                Result = 21
            };

            var objectResult = result as ObjectResult;

            var value = objectResult.Value as CalculatorResponse;

            Xunit.Assert.Equal(value.Result, request.Result);
            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);
        }
    }
}
