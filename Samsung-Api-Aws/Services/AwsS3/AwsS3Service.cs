using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _env;
        private readonly string _s3Bucket;
        private readonly string _s3LambdaBucketPrefix;
        private readonly string _s3UserFilesPrefix;
        private readonly string _s3LinksImagesPrefix;
        private readonly string _s3ProfileImagePrefix;
        private readonly string _envPrefix;
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
        public AwsS3Service(IAmazonS3 client, ILogger logger, IConfiguration configuration, UserManager<AppUser> userManager, IHostingEnvironment env)
        {
            _client = client;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _env = env;
            _s3Bucket = configuration.GetSection("AWS")["S3-bucket"];
            _s3LambdaBucketPrefix = configuration.GetSection("AWS")["S3-lambda-bucket-prefix"];
            _s3UserFilesPrefix = configuration.GetSection("AWS")["S3-user-files-prefix"];
            _s3LinksImagesPrefix = configuration.GetSection("AWS")["S3-links-images-prefix"];
            _s3ProfileImagePrefix = configuration.GetSection("AWS")["S3-profile-image-prefix"];
            _envPrefix = env.EnvironmentName + "/";
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
                
            image.Url = GetPreSignedUrl(key);

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

            var prefix = GetProfileImageKey(appUserId);

            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = _s3Bucket,
                Prefix = prefix
            };
            var response = await _client.ListObjectsV2Async(listObjectsRequest);

            if (response.S3Objects.Count > 0)
            {
                IImage image = new Image();
                image.Url = GetPreSignedUrl(response.S3Objects[0].Key);
                return image;
            }

            return null;
        }

        /// <summary>
        /// UploadLinkImageAsync
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<IImage> UploadLinkImageAsync(IImage image, ILink link)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            byte[] bytes = Convert.FromBase64String(image.Body);
            var key = GenerateLinkImageKey(image, link);

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

            image.Url = GetPreSignedUrl(key);

            return image;
        }

        /// <summary>
        /// GetLinkImageByIdAsync
        /// </summary>
        /// <param name="linkId"></param>
        /// <returns></returns>
        public async Task<IImage> GetLinkImageByIdAsync(ILink link)
        {
            if (!await AmazonS3Util.DoesS3BucketExistAsync(_client, _s3Bucket))
            {
                throw new AmazonS3Exception("S3 Bucket does not exist");
            }

            var prefix = GetLinkImageKey(link);

            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = _s3Bucket,
                Prefix = prefix
            };
            var response = await _client.ListObjectsV2Async(listObjectsRequest);

            if (response.S3Objects.Count > 0)
            {
                IImage image = new Image();
                image.Url = GetPreSignedUrl(response.S3Objects[0].Key);
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
            var key = _s3UserFilesPrefix + _envPrefix + userIdPrefix + _s3ProfileImagePrefix + _profilePictureKeyName + "." + image.FileExtension;

            return key;
        }

        private string GetProfileImageKey(string appUserId)
        {
            string userIdPrefix = appUserId + "/";
            return _s3UserFilesPrefix + _envPrefix + userIdPrefix + _s3ProfileImagePrefix + _profilePictureKeyName;
        }

        private string GenerateLinkImageKey(IImage image, ILink link)
        {
            string hashInput = link.Id + link.Url;
            string fileNameHash = CreateMD5(hashInput);
            var key = _s3LinksImagesPrefix + _envPrefix + fileNameHash + "." + image.FileExtension;

            return key;
        }

        private string GetLinkImageKey(ILink link)
        {
            string hashInput = link.Id + link.Url;
            string fileNameHash = CreateMD5(hashInput);
            return _s3LinksImagesPrefix + _envPrefix + fileNameHash;
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}