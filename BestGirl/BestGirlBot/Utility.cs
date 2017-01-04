using System;
using System.IO;
using System.IO.Compression;
using System.Text;

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

		public static string ReduceWhitespace(string s, bool trim = true)
		{
			StringBuilder builder = new StringBuilder();

			var str = trim ? s.Trim() : s;
			bool previousWhitespace = false;
			foreach (char c in str)
			{
				if (previousWhitespace && Char.IsWhiteSpace(c)) continue;
				if (Char.IsWhiteSpace(c))
				{
					builder.Append(' ');
					previousWhitespace = true;
				}
				else
				{
					builder.Append(c);
					previousWhitespace = false;
				}
			}

			return builder.ToString();
		}
	}
}
