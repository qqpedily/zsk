using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.ExtendModel
{
    /// <summary>
    /// 获取重要成果
    /// </summary>
    public class ImportantResults
    {
        /// <summary>
        /// 文献ID
        /// </summary>
        public Guid ProductionID { get; set; }
        /// <summary>
        /// 文献名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string ContentType { get; set; }

    }
}
