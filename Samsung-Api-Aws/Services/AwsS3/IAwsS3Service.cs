using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.AwsS3
{
    public interface IAwsS3Service
    {
        Task<IImage> UploadProfileImageByUserAsync(IImage image, string appUserId);

        Task<IImage> UploadLinkImageAsync(IImage image, int linkId);

        Task<IImage> GetProfileImageByUserAsync(string appUserId);

        Task<IImage> GetLinkImageByIdAsync(int linkId);
    }
}