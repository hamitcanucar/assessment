using System;
using System.Security.Cryptography;
using System.Text;

namespace ExampleProject.Util
{
    public static class HashExtentions
    {
        public static string HashToHmac256(this string str, string key)
        {
            if (System.String.IsNullOrEmpty(str)) return null;

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var inputBytes = Encoding.UTF8.GetBytes(str);

            using(var hmac = new HMACSHA256(keyBytes)){
                var hash = hmac.ComputeHash(inputBytes);
                return GetStringFromHash(hash);
            }
        }

        public static string HashToSha256(this string str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            return GetStringFromHash(CreateSha256Hash(str));
        }

        public static string HashToSha1(this string str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            return GetStringFromHash(CreateSha1Hash(str));
        }

        public static byte[] HashToSha1AsByte(this string str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            return CreateSha1Hash(str);
        }

        public static string ToHexString(this byte[] data)
        {
            return GetStringFromHash(data);
        }

        private static byte[] CreateSha256Hash(string str)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(str));
            }
        }

        private static byte[] CreateSha1Hash(string str)
        {
            using (var sha = SHA1.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(str));
            }
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("X2"));
            }
            return str.ToString();
        }
    }
}