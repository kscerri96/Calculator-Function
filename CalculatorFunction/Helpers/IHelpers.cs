using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CalculatorFunction.Helpers
{
    public interface IHelpers
    {
        string LoadInstructionsfromFile(Assembly assembly, string Filename);
    }
}
