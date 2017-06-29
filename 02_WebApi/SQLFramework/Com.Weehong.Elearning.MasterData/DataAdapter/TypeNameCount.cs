using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter
{
    /// <summary>
    /// 类别统计数
    /// </summary>
    public class TypeNameCount
    {
        public string TypeValue { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public string TypeID { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int TypeCount { get; set; }
    }
}
