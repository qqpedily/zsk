using NPinyin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.DataHelper
{
    public static class DataReplace
    {
        /// <summary>
        /// 中文转英文全拼,首字符大写
        /// </summary>
        /// <param name="c">单个中文字符</param>
        /// <returns></returns>
        public static string GetSpellFristUpper(char c)
        {
            string spell = Pinyin.GetPinyin(c);
            return spell.Substring(0, 1).ToUpper() + spell.Substring(1);
        }

        /// <summary>
        /// 中文转英文全拼,首字符大写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static string GetToSpellFristUpper(string c)
        {
            string spell = Pinyin.GetPinyin(c);
            string result = "";
            foreach (var item in spell.Split(' '))
            {
                result += item.Substring(0, 1).ToUpper() + item.Substring(1) + " ";
            }
            return result.TrimEnd();
        }

        /// <summary>
        /// 中文转英文全拼,首字符大写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static List<string> GetSpellFristUpper(string c)
        {
            List<string> list = new List<string>();
            string spell = Pinyin.GetPinyin(c);
            foreach (var item in spell.Split(' '))
            {
                list.Add(item.Substring(0, 1).ToUpper() + item.Substring(1));
            }
            return list;
        }


        /// <summary>
        /// 中文转英文全拼,全小写
        /// </summary>
        /// <param name="c">单个中文字符</param>
        /// <returns></returns>
        public static string GetSpellLower(char c)
        {
            return Pinyin.GetPinyin(c);
        }

        /// <summary>
        /// 中文转英文全拼,全小写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static string GetSpellLower(string c)
        {
            return Pinyin.GetPinyin(c);
        }

        /// <summary>
        /// 中文转英文全拼,全大写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static string GetSpellUpper(char c)
        {
            return Pinyin.GetPinyin(c).ToUpper();
        }

        /// <summary>
        /// 中文转英文全拼,全大写
        /// </summary>
        /// <param name="c">单个中文字符</param>
        /// <returns></returns>
        public static string GetSpellUpper(string c)
        {
            return Pinyin.GetPinyin(c).ToUpper();
        }


        /// <summary>
        /// 中文转英文首字母,全大写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static string GetSpellInitialsUpper(string c)
        {
            return Pinyin.GetInitials(c);
        }

        /// <summary>
        /// 中文转英文首字母,全小写
        /// </summary>
        /// <param name="c">中文字符</param>
        /// <returns></returns>
        public static string GetSpellInitialsLower(string c)
        {
            return Pinyin.GetInitials(c).ToLower();
        }
    }
}
