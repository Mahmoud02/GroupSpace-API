using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Shared
{
    public static class S3Helper
    {
        private static IAmazonS3 s3Client;
        public static async Task<string> UploadFileAsync(string FileName, Stream FileStream, string bucketName, string keyName, string SecretKey)
        {
            string filepath = "covers" + "/" + Guid.NewGuid().ToString() + "_" + FileName;

            PutObjectRequest request = new()
            {
                InputStream = FileStream,
                BucketName = bucketName,
                Key = filepath // <-- in S3 key represents a path  
            };
            s3Client = new AmazonS3Client(keyName, SecretKey, RegionEndpoint.EUWest2);
            await s3Client.PutObjectAsync(request);
            return "https://group-space-storage.s3.eu-west-2.amazonaws.com/"+filepath;
        }
    }
}
