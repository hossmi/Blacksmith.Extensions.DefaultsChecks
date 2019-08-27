using System.IO;

namespace Blaxpro.Tools.Extensions.Streams
{
    public static class StreamsExtensions
    {
        public static byte[] toBytes(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
