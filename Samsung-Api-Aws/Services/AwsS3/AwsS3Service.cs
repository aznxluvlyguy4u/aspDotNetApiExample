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
        private readonly string _s3ImageBucketPrefix;
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
            _s3ImageBucketPrefix = configuration.GetSection("AWS")["S3-image-bucket-prefix"];
        }

        public async Task<IImage> UploadImageByUser(IImage image, ClaimsPrincipal user)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            var userId = _userManager.GetUserId(user);
            string userFolderPrefix = userId + "/";
            byte[] bytes = Convert.FromBase64String(image.Body);

            // 1. Put object-specify only key name for the new object.
            var putRequest = new PutObjectRequest
            {
                BucketName = _s3Bucket,
                CannedACL = S3CannedACL.PublicRead,
                Key = _s3ImageBucketPrefix + userFolderPrefix + _profilePictureKeyName + "." + image.FileExtension
            };

            using (var ms = new MemoryStream(bytes))
            {
                putRequest.InputStream = ms;
                PutObjectResponse response = await _client.PutObjectAsync(putRequest);

                //if (response)
                //{
                //    _client.  getResourceUrl("your-bucket", "some-path/some-key.jpg");
                //}
                //var fileTransferUtility = new TransferUtility(_client);
                //await fileTransferUtility.UploadAsync(ms, _s3Bucket, _s3ImageBucketPrefix + _profilePictureKeyName + "." + image.);
            }


            var result = new Image();
            return result;
        }

        public Task GetProfilePictureByUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}