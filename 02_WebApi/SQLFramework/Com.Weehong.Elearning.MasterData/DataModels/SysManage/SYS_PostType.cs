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
    /// 职务类型表
    /// </summary>
    [Table("SYS_PostType")]
    public class SYS_PostType
    {
        public SYS_PostType Clone()
        {
            return (SYS_PostType)this.MemberwiseClone();
        }
        /// <summary>
        /// 职务类型表ID
        /// </summary>
        [Key]
        [Column("PtID", Order =0)]
        public Guid PtID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PostTypeName { get; set; }
        public string PostTypeEnglishName { get; set; }
        public int Sort { get; set; }
        public Nullable<int> DelFlag { get; set; }




    }
}
