using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Output
{
    /// <summary>
    /// 
    /// </summary>
    public class CollegeList
    {
        /// <summary>
        /// Code
        /// </summary>
        public Guid CID { get; set; }

        /// <summary>
        /// 院系名称
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 一级院系文献数
        /// </summary>
        public int OneCount { get; set; }

        /// <summary>
        /// 二级院系文献数
        /// </summary>
        public int TwoCount { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class CollegeIndexList
    {
        /// <summary>
        /// Code
        /// </summary>
        public Guid CID { get; set; }

        /// <summary>
        /// 院系名称
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 一级院系文献数
        /// </summary>
        public int SysCollegeCount { get; set; }
    }
}