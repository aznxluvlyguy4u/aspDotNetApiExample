using System;
using System.IO;
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
using SamsungApiAws.DataSource.Models;

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
        private readonly string _s3LinksImagesPrefix;
        private readonly string _s3ProfileImagePrefix;
        private const string _profilePictureKeyName = "profileImage";

        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        public AwsS3Service(IAmazonS3 client, ILogger logger, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _s3Bucket = configuration.GetSection("AWS")["S3-bucket"];
            _s3LambdaBucketPrefix = configuration.GetSection("AWS")["S3-lambda-bucket-prefix"];
            _s3UserFilesPrefix = configuration.GetSection("AWS")["S3-user-files-prefix"];
            _s3LinksImagesPrefix = configuration.GetSection("AWS")["S3-links-images-prefix"];
            _s3ProfileImagePrefix = configuration.GetSection("AWS")["S3-profile-image-prefix"];
        }

        /// <summary>
        /// UploadProfileImageByUserAsync
        /// </summary>
        /// <param name="image"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public async Task<IImage> UploadProfileImageByUserAsync(IImage image, string appUserId)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            byte[] bytes = Convert.FromBase64String(image.Body);
            var key = GenerateProfileImageKey(image, appUserId);

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

        /// <summary>
        /// GetProfileImageByUserAsync
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        public async Task<IImage> GetProfileImageByUserAsync(string appUserId)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            string userIdPrefix = appUserId + "/";
            var prefix = _s3UserFilesPrefix + userIdPrefix +  _s3ProfileImagePrefix + _profilePictureKeyName;

            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = _s3Bucket,
                Prefix = prefix
            };
            var response = await _client.ListObjectsV2Async(listObjectsRequest);

            if (response.S3Objects.Count > 0)
            {
                IImage image = new Image();
                image.S3Url = GetPreSignedUrl(response.S3Objects[0].Key);
                return image;
            }

            return null;
        }

        /// <summary>
        /// UploadLinkImageAsync
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<IImage> UploadLinkImageAsync(IImage image, int linkId)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            byte[] bytes = Convert.FromBase64String(image.Body);
            var key = GenerateLinkImageKey(image, linkId);

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

        /// <summary>
        /// GetLinkImageByIdAsync
        /// </summary>
        /// <param name="linkId"></param>
        /// <returns></returns>
        public async Task<IImage> GetLinkImageByIdAsync(int linkId)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            var prefix = _s3LinksImagesPrefix + linkId;

            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = _s3Bucket,
                Prefix = prefix
            };
            var response = await _client.ListObjectsV2Async(listObjectsRequest);

            if (response.S3Objects.Count > 0)
            {
                IImage image = new Image();
                image.S3Url = GetPreSignedUrl(response.S3Objects[0].Key);
                return image;
            }

            return null;
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

        private string GenerateProfileImageKey(IImage image, string appUserId)
        {
            string userIdPrefix = appUserId + "/";
            var key = _s3UserFilesPrefix + userIdPrefix + _s3ProfileImagePrefix + _profilePictureKeyName + "." + image.FileExtension;

            return key;
        }

        private string GenerateLinkImageKey(IImage image, int linkId)
        {
            var key = _s3LinksImagesPrefix + linkId + "." + image.FileExtension;

            return key;
        }

        private string GetProfileImageFolderPath(string appUserId)
        {
            string userIdPrefix = appUserId + "/";
            var prefix = _s3UserFilesPrefix + userIdPrefix + _s3ProfileImagePrefix;

            return prefix;
        }
    }
}