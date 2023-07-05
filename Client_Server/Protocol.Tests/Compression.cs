using K4os.Compression.LZ4.Streams;
using Protocol.Extensions;
using Protocol.Models;
using Protocol.Models.People;

namespace Protocol.Tests;

public class Compression
{
    readonly EmployeeModel _employee = new EmployeeModel
    {
        FirstName = "first",
        LastName = "last"
    };

    [Test]
    public void Compress_And_Decompress()
    {
        var bytes = new byte[] { 12, 3, 32, 4, 4, 35, 53, 45, 43, 35, 3, 42, 23, 43, 253, 4, 3, 45, 234, 32 };

        using var outputStream = new MemoryStream();

        using var inputStream = new MemoryStream(bytes);
        using var compressionStream = LZ4Stream.Encode(outputStream);

        inputStream.CopyTo(compressionStream);

        compressionStream.Flush();

        byte[] compressed = outputStream.ToArray();

        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(compressed);

        return;
    }

    [Test]
    public async Task Compress_Decompress_Model()
    {
        var employee = _employee;

        var protocolMessage = ProtocolMessage.FromMessage(employee);

        var bytes = protocolMessage.ToBytes(true);

        bytes = bytes.Compress();

        bytes = bytes.Decompress();

        var employeeDecoded = ProtocolMessage.Unpack<EmployeeModel>(bytes);
            
        Assert.AreEqual(employeeDecoded.Message.ComputeChecksum(), employee.ComputeChecksum());
    }

}
