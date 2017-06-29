using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.ExtendModel
{
    /// <summary>
    /// 学者推荐
    /// </summary>
    public class ScholarsRecommend
    {
        /// <summary>
        /// 图像ID
        /// </summary>
        public Guid? UUID { get; set; }
        /// <summary>
        ///姓
        /// </summary>
        public string SurnameChinese { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string NameChinese { get; set; }
        


    }
}
