using System.IO;
using System.IO.Compression;

namespace BestGirlBot
{
	public class Utility
	{
		public static Stream ZlibDecompress(byte[] bytes)
		{
			var decompressed = new MemoryStream();
			using (var compressed = new MemoryStream(bytes))
			using (var zlib = new DeflateStream(compressed, CompressionMode.Decompress))
				zlib.CopyTo(decompressed);

			decompressed.Position = 0;
			return decompressed;
		}

		public static string Deflate(byte[] bytes)
		{
			var decompressed = ZlibDecompress(bytes);
			using (var reader = new StreamReader(decompressed))
				return reader.ReadToEnd();
		}
	}
}
