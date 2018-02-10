using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class Utilities
    {
        private static readonly int StuIdOffset = 118000000;
        private static readonly int TeaIdOffset = 218000000;

        public static int StuIdConvertToDbId(int id) => id - StuIdOffset;

        public static int TeaIdConvertToDbId(int id) => id - TeaIdOffset;

        public static int DbIdConvertToStuId(int id) => id + StuIdOffset;

        public static int DbIdConvertToTeaId(int id) => id + TeaIdOffset;

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>MD5后的密文</returns>
        public static string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();
            var epswb = csp.ComputeHash(Encoding.Unicode.GetBytes(password));
            return BitConverter.ToString(epswb).Replace("-", "");
        }

        /// <summary>
        /// 检查手机号码是否符合规范
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <exception cref="System.ArgumentNullException">phone为null</exception>
        public static bool IsVaildPhoneNumber(string phone)
        {
            if (phone == null)
                throw new ArgumentNullException("phone");
            return Regex.IsMatch(phone, @"13[012356789]\d{8}|15[012356789]\d{8}|18[012356789]\d{8}/");
        }

        /// <summary>
        /// 检查Id是否符合规范
        /// </summary>
        /// <param name="id">Id名称</param>
        /// <exception cref="System.ArgumentNullException">id为null</exception>
        public static bool IsValidId(string id)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            return Regex.IsMatch(id, @"^[1-9]*[1-9][0-9]*$");
        }
    }
}
