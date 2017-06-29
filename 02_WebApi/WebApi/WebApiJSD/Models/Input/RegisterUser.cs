using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Input
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 姓中文
        /// </summary>
        public string SurnameChinese { get; set; }

        /// <summary>
        /// 姓英文
        /// </summary>
        public string SurnamePhoneticize { get; set; }

        /// <summary>
        /// 名字中文
        /// </summary>
        public string NameChinese { get; set; }

        /// <summary>
        /// 名字英文
        /// </summary>
        public string NamePhoneticize { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public Guid UnitID { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DeptID { get; set; }

        /// <summary>
        /// 职称ID
        /// </summary>
        public Guid PttID { get; set; }

        /// <summary>
        /// 身份ID
        /// </summary>
        public Guid ItID { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 二次密码
        /// </summary>
        public string TwoPassword { get; set; }
    }
}