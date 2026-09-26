using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GtAcademy.Web.Utilities
{
    public class FileStorageOptions
    {
        public string RootPath { get; set; } = "App_Data/PrivateUploads";
        public long MaxFileSizeInMb { get; set; } = 100;
        public string[] AllowedExtensions { get; set; } = [".zip", ".rar", ".pdf", ".mp4", ".mp3", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png", ".webp"];
    }
}
