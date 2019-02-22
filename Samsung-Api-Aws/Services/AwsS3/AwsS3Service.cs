using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using samsung.api.DataSource.Models;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;

namespace samsung.api.Services.AwsS3
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly string _s3Bucket;
        private readonly string _s3LambdaBucketPrefix;
        private readonly string _s3UserFilesPrefix;
        public const string _profilePictureKeyName = "profileImage";

        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;

        public AwsS3Service(IAmazonS3 client, ILogger logger, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _s3Bucket = configuration.GetSection("AWS")["S3-bucket"];
            _s3LambdaBucketPrefix = configuration.GetSection("AWS")["S3-lambda-bucket-prefix"];
            _s3UserFilesPrefix = configuration.GetSection("AWS")["S3-user-files-prefix"];
        }

        public async Task<IImage> UploadImageByUser(IImage image, string appUserId)
        {
            using (_client)
            {
                if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
                {
                    throw new AmazonS3Exception("S3 Bucket does not exist");
                }

                string userIdPrefix = appUserId + "/";
                byte[] bytes = Convert.FromBase64String(image.Body);
                var key = _s3UserFilesPrefix + userIdPrefix + _profilePictureKeyName + "." + image.FileExtension;

                using (var ms = new MemoryStream(bytes))
                {
                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = _s3Bucket,
                        CannedACL = S3CannedACL.PublicRead,
                        Key = key,
                        InputStream = ms
                    };

                    var fileTransferUtility = new TransferUtility(_client);
                    await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                }
                
                image.S3Url = GetPreSignedUrl(key);

                return image;
            }
        }

        public Task GetProfilePictureByUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        private string GetPreSignedUrl(string key)
        {
            GetPreSignedUrlRequest preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _s3Bucket,
                Key = key,
                Protocol = Protocol.HTTPS,
                Expires = DateTime.Now.AddMonths(1)
            };

            return _client.GetPreSignedURL(preSignedUrlRequest);
        }
    }
}