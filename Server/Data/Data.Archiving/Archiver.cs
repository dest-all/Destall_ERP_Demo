using Data.Repository;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Text.Json;
using Data.EntityFramework;
using System.Runtime.CompilerServices;

namespace Data.Archiving
{
    public class Archiver
    {
        readonly Encoding _encodingUsed;

        readonly ApplicationDbContext _dbContext;

        public Archiver(ApplicationDbContext dbContext, Encoding? encodingUsed = null)
        {
            _dbContext = dbContext;
            _encodingUsed = encodingUsed ?? Encoding.Default;
        }

        public async Task LoadToDatabaseAsync(byte[] bytes, bool inputIsCompressed = true)
        {
            if (inputIsCompressed)
            {
                bytes = await DecompressAsync(bytes);
            }
            string json = _encodingUsed.GetString(bytes);

            var contents = JsonSerializer.Deserialize<DatabaseContents>(json);

            _dbContext.Download(contents);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<byte[]> UploadAsync()
        {
            var content = await _dbContext.UploadAsync();

            var json = JsonSerializer.Serialize(content);

            var bytes = _encodingUsed.GetBytes(json);

            bytes = await CompressAsync(bytes);

            return bytes;
        }

        public static Task<byte[]> CompressAsync(string str)
            => CompressAsync(Encoding.Default.GetBytes(str));

        public static async Task<byte[]> DecompressAsync(byte[] byteArray)
        {
            using var inputStream = new MemoryStream(byteArray);
            using var decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress);

            using var outputStream = new MemoryStream();

            await decompressionStream.CopyToAsync(outputStream);

            outputStream.Position = 0;

            var result = outputStream.ToArray();

            return result;
        }

        public static async Task<byte[]> CompressAsync(byte[] byteArray)
        {
            using var inputStream = new MemoryStream(byteArray);
            using var outputStream = new MemoryStream();

            using var compressionStream = new GZipStream(outputStream, CompressionLevel.SmallestSize);

            await inputStream.CopyToAsync(compressionStream);
            await compressionStream.FlushAsync();

            var result = outputStream.ToArray();

            return result;
        }
    }
}