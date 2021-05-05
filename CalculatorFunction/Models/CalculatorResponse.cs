using Newtonsoft.Json;

namespace CalculatorFunction.Models
{
    public class CalculatorResponse
    {
        [JsonProperty("Result", Required = Required.Always)]
        public float Result { get; set; }
    }
}
