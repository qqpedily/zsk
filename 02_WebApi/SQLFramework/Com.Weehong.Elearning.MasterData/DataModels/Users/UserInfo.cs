using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户信息 
    /// By【ZHL】
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UUID { get; set; }
        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 英文名称   拼音
        /// </summary>
        public string EnglishName { get; set; }


    }
}
