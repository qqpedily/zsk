using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.SysTemplate
{
    /// <summary>
    /// 全文许可定制
    /// </summary>
    [Table("SYS_FullTextPermission")]
    public class SYS_FullTextPermission
    {
        public SYS_FullTextPermission Clone()
        {
            return (SYS_FullTextPermission)this.MemberwiseClone();
        }
        /// <summary>
        /// 全文许可定制ID
        /// </summary>
        [Key]
        [Column("FullTextPermissionID", Order =0)]
        public Guid FullTextPermissionID { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// 中文全称
        /// </summary>
        public string ChineseName { get; set; }
        /// <summary>
        /// 英文全称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 中文摘要
        /// </summary>
        public string ChineseAbstract { get; set; }
        /// <summary>
        /// 英文摘要
        /// </summary>
        public string EnglishAbstract { get; set; }
        /// <summary>
        /// 许可URI
        /// </summary>
        public string PermissionURI { get; set; }



    }
}
