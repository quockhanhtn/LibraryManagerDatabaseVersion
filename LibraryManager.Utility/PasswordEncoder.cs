using System.Security.Cryptography;
using System.Text;

namespace LibraryManager.Utility
{
    /// <summary>
    /// Mã hóa password
    /// </summary>
    public static class PasswordEncoder
    {
        /// <summary>
        /// Mã hóa chuỗi theo Base64
        /// </summary>
        /// <param name="plainText">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa theo Base64</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Mã hóa chuỗi theo MD5
        /// </summary>
        /// <param name="plainText">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa theo MD5 có 32 ký tự</returns>
        public static string MD5Hash(string plainText)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(plainText));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// Mã hóa chuỗi theo Base64 rồi tiếp tục mã hóa theo MD5
        /// </summary>
        /// <param name="plainText">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa có 32 ký tự</returns>
        public static string Base64ThenMD5(string plainText)
        {
            return MD5Hash(Base64Encode(plainText));
        }
    }
}
