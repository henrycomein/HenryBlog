namespace Henry.Common
{
    using System;
    using System.Security.Cryptography;

    public class EncryptionHelper
    {
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string EncryptionPassword(string username, string password)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(username.ToLower()+ password + "_Henry");
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(buffer));
        }

    }
}

