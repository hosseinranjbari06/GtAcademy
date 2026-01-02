using GtAcademy.Application.Tools.RandomCodeGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Tools.Persistence
{
    public class CodeGenerator : ICodeGenerator
    {
        public string GenerateFiveDigitCode()
        {
            var rnd = new Random();
            return rnd.Next(10).ToString() +
                   rnd.Next(10).ToString() +
                   rnd.Next(10).ToString() +
                   rnd.Next(10).ToString() +
                   rnd.Next(10).ToString();
        }
    }
}
