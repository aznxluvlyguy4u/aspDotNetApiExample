using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.AwsS3
{
    public interface IAwsS3Service
    {
        Task<IImage> UploadImageByUser(IImage image, ClaimsPrincipal user);

        Task GetProfilePictureByUserAsync(ClaimsPrincipal user);

    }
}