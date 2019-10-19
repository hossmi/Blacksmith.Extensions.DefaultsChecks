using System;
using System.IO;
using Everis.ToolBox;

namespace Everis.ToolBox.Extensions.Streams
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
