using CalculatorFunction;
using CalculatorFunction.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;


[assembly: WebJobsStartup(typeof(StartUp))]
namespace CalculatorFunction
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class StartUp : IWebJobsStartup
    {
        public async void Configure(IWebJobsBuilder builder)
        {          
            builder.Services.AddSingleton<IHelpers, FunctionsHelpers>(); 
        }
    }
}
