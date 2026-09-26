using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GtAcademy.Web.Utilities
{
    public class SecureFileStorageService : ISecureFileStorageService
    {
        private readonly string _rootPath;
        private readonly long _maxFileSizeInBytes;
        private readonly HashSet<string> _allowedExtensions;

        public SecureFileStorageService(IWebHostEnvironment environment, IOptions<FileStorageOptions> options)
        {
            var configuredRoot = options.Value.RootPath;
            _rootPath = string.IsNullOrWhiteSpace(configuredRoot)
                ? Path.Combine(environment.ContentRootPath, "App_Data", "PrivateUploads")
                : Path.Combine(environment.ContentRootPath, configuredRoot);

            _maxFileSizeInBytes = options.Value.MaxFileSizeInMb * 1024 * 1024;
            _allowedExtensions = new HashSet<string>(options.Value.AllowedExtensions.Select(ext => ext.Trim().ToLowerInvariant()), StringComparer.OrdinalIgnoreCase);
        }

        public string GetAbsolutePath(string relativeFolder)
        {
            var normalizedFolder = relativeFolder
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar)
                .TrimStart(Path.DirectorySeparatorChar);

            var path = Path.Combine(_rootPath, normalizedFolder);
            return path;
        }

        public async Task<(bool IsSuccess, string? StoredRelativePath, string? ErrorMessage)> SaveAsync(IFormFile file, string relativeFolder, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                return (false, null, "فایلی برای ذخیره‌سازی انتخاب نشده است.");
            }

            if (file.Length > _maxFileSizeInBytes)
            {
                return (false, null, $"حجم فایل باید کمتر از {_maxFileSizeInBytes / (1024 * 1024)} مگابایت باشد.");
            }

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !_allowedExtensions.Contains(extension.ToLowerInvariant()))
            {
                return (false, null, "نوع فایل مجاز نیست. لطفاً فرمت فایل را بررسی کنید.");
            }

            var normalizedFolder = relativeFolder
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar)
                .TrimStart(Path.DirectorySeparatorChar);

            var directoryPath = Path.Combine(_rootPath, normalizedFolder);
            Directory.CreateDirectory(directoryPath);

            var safeFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(directoryPath, safeFileName);

            try
            {
                await using var stream = System.IO.File.Create(fullPath);
                await file.CopyToAsync(stream, cancellationToken);
            }
            catch (Exception ex)
            {
                return (false, null, $"خطا در ذخیره فایل: {ex.Message}");
            }

            var relativePath = Path.Combine(normalizedFolder, safeFileName)
                .Replace('\\', '/');

            return (true, relativePath, null);
        }

        public async Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return;
            }

            try
            {
                var fullPath = Path.Combine(_rootPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(fullPath))
                {
                    await Task.Run(() => System.IO.File.Delete(fullPath), cancellationToken);
                }
            }
            catch
            {
                // Intentionally silent for cleanup operations.
            }
        }
    }
}
