using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Output
{
    /// <summary>
    /// 用户票据
    /// </summary>
    public class LoginUserTicket
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 用户姓
        /// </summary>
        public string SurnameChinese { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string NameChinese { get; set; }

        /// <summary>
        /// 用户姓拼音
        /// </summary>
        public string SurnamePhoneticize { get; set; }
        /// <summary>
        /// 用户名拼音
        /// </summary>
        public string NamePhoneticize { get; set; }

        /// <summary>
        /// 用户别名
        /// </summary>
        public string AliasName { get; set; }

        public string ImgUserHead { get; set; }
    }
}