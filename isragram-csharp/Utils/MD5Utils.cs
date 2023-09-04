using System.Security.Cryptography;
using System.Text;

namespace isragram_csharp.Utils
{
    public class MD5Utils
    {
        public static string GenerateMD5Hash(string text)
        {
            MD5 md5hash = MD5.Create();
            var bytes = md5hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuildder = new StringBuilder();

            foreach ( int b in bytes ) {
                stringBuildder.Append( b );
            }
            return stringBuildder.ToString();
        }
    }
}
