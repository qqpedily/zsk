using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Input
{

    /// <summary>
    /// 收藏输入类
    /// </summary>
    public class UserCollect
    {
        /// <summary>
        /// 收藏人员ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 收藏内容ID
        /// </summary>
        public Guid CollectInfoID { get; set; }

        /// <summary>
        /// 收藏内容名称
        /// </summary>
        public string CollectInfoName { get; set; }
    }
}