using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tools.RandomCodeGenerator
{
    public interface ICodeGenerator
    {
        string GenerateFiveDigitCode();

        string GenerateReferralCode(int length = 8);
    }
}
