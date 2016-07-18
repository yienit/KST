using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Util
{
    /// <summary>
    /// Class for encryption and decryption.
    /// </summary>
    public class SecurityUtil
    {
        /// <summary>
        /// MD5 encryption
        /// </summary>
        public static string MD5(string src)
        {
            string result = string.Empty;

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(src));

            for (int i = 0; i < bytes.Length; i++)
            {
                result += bytes[i].ToString("x2");
            }

            return result.Trim();
        }
    }
}
