using CalculatorFunction.Helpers;
using CalculatorFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Threading.Tasks;

namespace Calculator.Unit.Tests
{
    [TestClass]
    public class CalculatorFunctionUnitTests
    {

        [TestMethod]
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

        [TestMethod]
        public async Task ValidRequest()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockLogger = new Mock<ILogger>();
            var mockFileWrapper = new Mock<IHelpers>();
            var assembly = Assembly.LoadFrom("CalculatorFunction");

            mockFileWrapper.Setup(m => m.LoadInstructionsfromFile(assembly, "CalculatorFunction.Instructions.txt")).Returns("add 2\r\nmultiply 3\r\napply 3");

            var function = new CalculatorFunction.CalculatorFunction(mockFileWrapper.Object);
            var result = await function.Calculator(mockRequest.Object, mockLogger.Object);

            var objectResult = result as ObjectResult;

            Xunit.Assert.Equal(200, objectResult?.StatusCode.Value);

        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

    }
}
