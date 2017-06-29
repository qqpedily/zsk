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
    /// 身份类型表
    /// </summary>
    [Table("SYS_IdentityType")]
    public class SYS_IdentityType
    {
        public SYS_IdentityType Clone()
        {
            return (SYS_IdentityType)this.MemberwiseClone();
        }
        /// <summary>
        /// 身份类型表ID
        /// </summary>
        [Key]
        [Column("ItID", Order =0)]
        public Guid ItID { get; set; }
        /// <summary>
        /// 身份类型名称
        /// </summary>
        public string IdentityTypeName { get; set; }
        /// <summary>
        /// 身份类型英文名称
        /// </summary>
        public string IdentityTypeEnglishName { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public Nullable<int> Sort { get; set; }
        /// <summary>
        /// 删除标志(1正常;0删除)
        /// </summary>
        public Nullable<int> DelFlag { get; set; }




    }
}
