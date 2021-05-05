using System;
using System.Collections.Generic;

namespace CalculatorFunction.Models
{
    public class Instructions
    {
        public string Keyword { get; set; }
        public string Number { get; set; }

        public static List<Instructions> GetInstructionList(string calculatorInstructions)
        {
            try
            {
                var instructions = calculatorInstructions.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);
                List<Instructions> instructionList = new List<Instructions>();
                for (int i = 0; i < instructions.Length; i++)
                {
                    var inst = instructions[i].Split(" ");
                    if (i != 0)
                    {
                        var newValue = instructions[i - 1].Split(" ")[0];
                        instructionList.Add(new Instructions { Keyword = newValue, Number = inst[1] });
                    }
                    else
                    {
                        instructionList.Add(new Instructions { Keyword = "apply", Number = inst[1] });
                    }
                }

                return instructionList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
