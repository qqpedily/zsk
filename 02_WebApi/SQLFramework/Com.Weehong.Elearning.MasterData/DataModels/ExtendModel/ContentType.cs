using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.ExtendModel
{
    /// <summary>
    /// 内容类型
    /// </summary>
    public class ContentTypeExtent
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ContentTypeID { get; set; }
        /// <summary>
        ///内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int ContentTypeCount { get; set; }


    }
}
