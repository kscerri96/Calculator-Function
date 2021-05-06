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
                var instructions = calculatorInstructions.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<Instructions> instructionList = new List<Instructions>();

                foreach(var instruction in instructions)
                {
                    var inst = instruction.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                    instructionList.Add(new Instructions { Keyword = inst[0], Number = inst[1] });
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
