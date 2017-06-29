using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
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
    /// 院系配置
    /// </summary>
    [Table("SYS_College")]
    public class SYS_College
    {
        ProductionsAdapter adapter = new ProductionsAdapter();
        public SYS_College Clone()
        {
            return (SYS_College)this.MemberwiseClone();
        }
        /// <summary>
        /// 院系ID
        /// </summary>
        [Key]
        [Column("CID", Order = 0)]
        public Guid CID { get; set; }

        /// <summary>
        /// 院系名称(中文)
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 院系名称(英文)
        /// </summary>
        public string CollegeEnglishName { get; set; }

        /// <summary>
        /// 院系上级ID
        /// </summary>
        public Nullable<Guid> ParentID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 删除标识 【1正常; 0删除】
        /// </summary>
        public Nullable<int> DelFlag { get; set; }


        /// <summary>
        /// 院系类型 【大分支机构为0，小分支机构为1，科系为2】
        /// </summary>
        public int? CollegeType { get; set; }


        private int _ProcuctsNum = -1;
        /// <summary>
        /// 院系下的文献数量
        /// </summary>
      
        public int ProcuctsNum
        { get; set; }
      
        /// <summary>
        /// 院系下子院系的所有文献数量
        /// </summary>
        public int ProcuctsAllNum
        { get; set; }
    }
}







