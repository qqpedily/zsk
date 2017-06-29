using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.Common
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5Method
    {
        public static readonly MD5Method Instance = new MD5Method();

        /// <summary>
        /// 创建Key
        /// </summary>
        /// <returns></returns>
        public string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        /// 对字符串做MD5加密
        /// </summary>
        /// <param name="pToEncrypt">加密字符串</param>
        /// <returns></returns>
        public string MD5Encrypt(string pToEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(pToEncrypt);
            var encs = md5.ComputeHash(data);
            return BitConverter.ToString(encs).Replace("-", "");
        }
    }
}
