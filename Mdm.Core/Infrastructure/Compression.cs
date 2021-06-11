using System.IO;
using System.IO.Compression;
using System.Text;

namespace Mdm.Core.Infrastructure
{
    internal class Compression
    {
        public static Stream Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    CopyTo(msi, gs);
                }

                return new MemoryStream(mso.ToArray());
            }
        }

        public static string Unzip(Stream stream)
        {
            byte[] bytes;

            using (var br = new BinaryReader(stream))
            {
                bytes = br.ReadBytes((int)stream.Length);
            }

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

    }
}
