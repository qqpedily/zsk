using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户信息列表 用户查询用户信息显示
    /// </summary>
    public class UserList
    {

        /// <summary>
        /// ID 
        /// </summary>
        public Guid UUID { set; get; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string UserEmail { set; get; }
        /// <summary>
        /// 姓（中文）
        /// </summary>
        public string SurnameChinese { set; get; }
        /// <summary>
        /// 姓（拼音）
        /// </summary>
        public string SurnamePhoneticize { set; get; }
        /// <summary>
        /// 名（中文）
        /// </summary>
        public string NameChinese { set; get; }
        /// <summary>
        /// 名（拼音）
        /// </summary>
        public string NamePhoneticize { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户名称(拼音)
        /// </summary>
        public string PinYinUserName { get; set; }
    }
}
