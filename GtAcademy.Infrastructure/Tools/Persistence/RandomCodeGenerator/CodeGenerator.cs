using GtAcademy.Application.Tools.RandomCodeGenerator;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GtAcademy.Infrastructure.Tools.Persistence.RandomCodeGenerator
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

        public string GenerateReferralCode(int length = 8)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var bytes = RandomNumberGenerator.GetBytes(length);
            var result = new char[length];

            for (int i = 0; i < length; i++)
                result[i] = chars[bytes[i] % chars.Length];

            return new string(result);
        }
    }
}
