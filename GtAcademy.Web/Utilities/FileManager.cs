using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace GtAcademy.Web.Utilities
{
    public static class FileManager
    {
        public static async Task<ErrorOr<string>> SaveFile(IFormFile file, string path, string fileName)
        {
            var result = IsFileValid(file);

            if (result.IsError)
                return result.Errors;

            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var filePath = Path.Combine(path, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return Error.Unexpected(description: ex.Message);
            }
        }

        public static ErrorOr<bool> IsFileValid(IFormFile file)
        {
            if (file.Length > 0)
                return true;

            return Error.Validation(code: "File", description: "فایل اپلود شده نامعتبر است");
        }

        public static string GenerateRandomFileName(string oldFileName)
        {
            return Path.GetRandomFileName() + Path.GetExtension(oldFileName);
        }
    }
}
