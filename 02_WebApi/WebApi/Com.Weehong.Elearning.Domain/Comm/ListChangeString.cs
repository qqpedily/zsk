using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YinGu.Operation.Framework.Domain.Comm
{
    /// <summary>
    /// 数组转字符
    /// </summary>
    public class ListChangeString
    {
        /// <summary>
        /// 获取数组格式字符串
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="format">格式字符</param>
        /// <returns></returns>
        public static string GetString(List<ProductionsField> list, char format)
        {
            string result = "";
            foreach (var item in list)
            {
                result += item.DefaultText + format;
            }

            return result.Substring(0, result.Length - 1);
        }

        /// <summary>
        /// 获取数组格式字符串
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="format">格式字符</param>
        /// <returns></returns>
        public static string GetString(List<ProductionsField> list)
        {
            string result = "";
            foreach (var item in list)
            {
                result += item.DefaultText + "|";
            }

            return result.Length > 0 ? result.Substring(0, result.Length - 1) : result;
        }
    }
}
