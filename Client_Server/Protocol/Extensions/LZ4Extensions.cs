using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using K4os.Compression.LZ4;

using DestallMaterials.WheelProtection.Extensions.ByteArray;

namespace Protocol.Extensions;

public static class LZ4Extensions
{
    public static byte[] Compress(this byte[] bytes)
        =>  LZ4Pickler.Pickle(bytes);

    public static byte[] Decompress(this byte[] bytes)
        => LZ4Pickler.Unpickle(bytes);
}
