using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CalculatorFunction.Helpers;
using System.Reflection;
using CalculatorFunction.Models;
using System;

namespace CalculatorFunction
{
    public class CalculatorFunction
    {
        private readonly IHelpers _functionHelpers;

        public CalculatorFunction(IHelpers functionHelpers)
        {
            _functionHelpers = functionHelpers;
        }

        [FunctionName("Calculator")]
        public async Task<IActionResult> Calculator([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var calculatorInstructions = _functionHelpers.LoadInstructionsfromFile(Assembly.GetExecutingAssembly(), "CalculatorFunction.Instructions.txt");

                if (string.IsNullOrEmpty(calculatorInstructions))
                    return new BadRequestObjectResult("Instruction File is empty or not found.");

                var instructionList = Instructions.GetInstructionList(calculatorInstructions);

                var result = float.Parse(instructionList[instructionList.Count - 1].Number);

                foreach (var instruct in instructionList)
                {
                    switch (instruct.Keyword.ToLower())
                    {
                        case "add":
                            result += float.Parse(instruct.Number);
                            break;
                        case "subtract":
                            result -= float.Parse(instruct.Number);
                            break;
                        case "multiply":
                            result *= float.Parse(instruct.Number);
                            break;
                        case "divide":
                            result /= float.Parse(instruct.Number);
                            break;
                        case "apply":
                            break;
                        default:
                            return new BadRequestObjectResult("Keyword in instruction file is not valid.");
                    }
                }

                CalculatorResponse response = new CalculatorResponse() { Result = result };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("An error has occurred.");
            }
        }
    }
}
