using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SalesClient
{
    class Program
    {
        private const string filePath = @"Files/SalesData.txt";
        private const string host = "http://localhost:6523";
        private const string usernamePassword= "test:test";
        static async Task Main(string[] args)
        {
            await UploadFile(filePath);
        }

        public static async Task UploadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File [{filePath}] not found.");
            }

            using var httpClient = new HttpClient();
            using var formDataContent = new MultipartFormDataContent();
            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            var byteArray = Encoding.ASCII.GetBytes(usernamePassword);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            formDataContent.Add(fileContent, "formFile", Path.GetFileName(filePath));

            var response = await httpClient.PostAsync($"{host}/api/files", formDataContent);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("File has been uploaded successfully.");


        }
    }
}
