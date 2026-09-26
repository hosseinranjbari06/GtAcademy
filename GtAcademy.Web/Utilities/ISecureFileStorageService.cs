using Microsoft.AspNetCore.Http;

namespace GtAcademy.Web.Utilities
{
    public interface ISecureFileStorageService
    {
        Task<(bool IsSuccess, string? StoredRelativePath, string? ErrorMessage)> SaveAsync(IFormFile file, string relativeFolder, CancellationToken cancellationToken = default);
        Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default);
        string GetAbsolutePath(string relativeFolder);
    }
}
