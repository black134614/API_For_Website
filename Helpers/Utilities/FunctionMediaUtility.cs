
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace API.Helpers.Utilities
{
    public interface IFunctionMediaUtility
    {
        Task<string> UploadAsync(IFormFile file, string fileNameWithoutExtension);
        Task<string> UploadAsync(string file, string fileNameWithoutExtension);
    }
    public class FunctionMediaUtility : IFunctionMediaUtility
    {
        private readonly IWebHostEnvironment _webHostEnv;

        public FunctionMediaUtility(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        /// <summary>
        /// Upload image or video file to wwwroot folder. Return file name with extension, default return null.
        /// </summary>
        /// <param name="file">File to upload.</param>
        /// <param name="fileNameWithoutExtension">File name without extension.</param>
        /// <returns>File name with extension.</returns>
        public async Task<string> UploadAsync(IFormFile file, string fileNameWithoutExtension)
        {
            if (file == null)
                return null;

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension))
                return null;

            extension = extension.ToLower();
            var folderPath = extension == ".mp4" ? Path.Combine(_webHostEnv.WebRootPath, @"uploaded\video")
                                                 : Path.Combine(_webHostEnv.WebRootPath, @"uploaded\images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{fileNameWithoutExtension}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            try
            {
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                return fileName;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Upload image or video file converted from base64 string to wwwroot folder. Return file name with extension, default return null.
        /// </summary>
        /// <param name="file">base64 string.</param>
        /// <param name="fileNameWithoutExtension">File name without extension.</param>
        /// <returns>File name with extension.</returns>
        public async Task<string> UploadAsync(string file, string fileNameWithoutExtension)
        {
            if (string.IsNullOrEmpty(file))
                return null;

            var extension = $".{file.Split(';')[0].Split('/')[1]}";
            if (string.IsNullOrEmpty(extension))
                return null;

            extension = extension.ToLower();
            var folderPath = extension == ".mp4" ? Path.Combine(_webHostEnv.WebRootPath, @"uploaded\video")
                                                 : Path.Combine(_webHostEnv.WebRootPath, @"uploaded\images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{fileNameWithoutExtension}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            var base64 = file.Contains("video") ? file.Replace("data:video/mp4;base64,", "") : file.Substring(file.IndexOf(',') + 1);
            if (!file.Contains("video"))
                base64 = base64.Trim('\0');

            var fileData = Convert.FromBase64String(base64);

            try
            {
                await System.IO.File.WriteAllBytesAsync(filePath, fileData);
                return fileName;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}