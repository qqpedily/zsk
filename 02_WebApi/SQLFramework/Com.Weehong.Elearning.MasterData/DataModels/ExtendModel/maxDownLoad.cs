using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.ExtendModel
{
    /// <summary>
    /// 获取下载最多
    /// </summary>
    public class maxDownLoad
    {
        /// <summary>
        /// 文献标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        ///下载数量 
        /// </summary>
        public int DownloadNum { get; set; }


    }
}
