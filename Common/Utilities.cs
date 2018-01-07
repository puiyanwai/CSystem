using System;
using System.Text.RegularExpressions;

namespace Common
{
    public class Utilities
    {
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
