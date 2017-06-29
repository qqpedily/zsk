using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Output
{
    /// <summary>
    /// 分页行转列数据
    /// </summary>
    public class DataTableTotal
    {
            /// <summary>
            /// 数据
            /// </summary>
            public List<Production> Production { get; set; }
            /// <summary>
            /// 数据总数
            /// </summary>
            public int TotalCount { get; set; }
        
    }
}