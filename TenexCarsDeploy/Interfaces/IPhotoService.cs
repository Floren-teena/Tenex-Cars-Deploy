using CloudinaryDotNet.Actions;

namespace TenexCarsDeploy.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<RawUploadResult> AddDocumentAsync(IFormFile document);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> UpdatePhotoAsync(string id, IFormFile file);
    }
}
