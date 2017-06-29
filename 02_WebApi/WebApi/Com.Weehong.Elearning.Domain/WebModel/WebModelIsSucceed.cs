using Com.Weehong.Elearning.MasterData.DataAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Weehong.Elearning.Domain.WebModel
{
    public class WebModelIsSucceed
    {
        /// <summary>
        /// 是否执行成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}