using System.IO;
using System.Reflection;

namespace CalculatorFunction.Helpers
{
    public class FunctionsHelpers : IHelpers
    {
        public string LoadInstructionsfromFile(Assembly assembly, string Filename)
        {
            if (string.IsNullOrWhiteSpace(Filename))
                return null;

            using (Stream stream = assembly.GetManifestResourceStream(Filename))
            {
                if (stream == null)
                    return null;

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
