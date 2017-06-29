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
    /// 学科
    /// </summary>
    [Table("SYS_Subject")]
    public class SYS_Subject
    {
        public SYS_Subject Clone()
        {
            return (SYS_Subject)this.MemberwiseClone();
        }
        /// <summary>
        /// 学科ID
        /// </summary>
        [Key]
        [Column("",Order =0)]
        public Guid SubjectID { get; set; }
        /// <summary>
        /// 大类
        /// </summary>
        public string BigCollege { get; set; }
        /// <summary>
        /// 小类
        /// </summary>
        public string SmallCollege { get; set; }
        /// <summary>
        /// 学科名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }


    }
}
