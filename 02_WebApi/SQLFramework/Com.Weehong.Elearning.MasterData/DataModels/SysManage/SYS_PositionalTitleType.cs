using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.SysManage
{
    /// <summary>
    /// 职称类型表
    /// </summary>
    [Table("SYS_PositionalTitleType")]
    public class SYS_PositionalTitleType
    {
        public SYS_PositionalTitleType Clone()
        {
            return (SYS_PositionalTitleType)this.MemberwiseClone();
        }
        /// <summary>
        /// 职称类型表ID
        /// </summary>
        [Key]
        [Column("PttID",Order =1)]
        public Guid PttID { get; set; }
        /// <summary>
        /// 职称类型名称
        /// </summary>
        public string PostTitleTypeName { get; set; }
        /// <summary>
        /// 职称类型英文名称
        /// </summary>
        public string TitleTypeEnglishName { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public Nullable<int> Sort { get; set; }
        /// <summary>
        /// 删除标志(1正常;0删除)
        /// </summary>
        public int? DelFlag { get; set; }



    }
}
