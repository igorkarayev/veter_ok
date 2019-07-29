using System;
using System.Security.Cryptography;
using System.Text;

namespace Delivery.BLL
{
    public class Gravatar
    {
        /// Hashes an email with MD5.  Suitable for use with Gravatar profile
        /// image urls
        public static string GravatarImageLink(string email, string size)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            var sizeMod = String.Empty;

            if (!String.IsNullOrEmpty(size))
            {
                sizeMod = "?size=" + size;
            }
            return string.Format("http://www.gravatar.com/avatar/{0}{1}", sBuilder, sizeMod);
        }
    }
}