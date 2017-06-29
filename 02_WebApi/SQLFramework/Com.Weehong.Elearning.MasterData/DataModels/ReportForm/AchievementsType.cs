using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.ReportForm
{
    /// <summary>
    /// 年度成果类型分布    科通用   
    /// </summary>
    public class AchievementsType
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string name { get; set;}
        /// <summary>
        /// 类型值 数量
        /// </summary>
        public int value { get; set; }

    }
}
