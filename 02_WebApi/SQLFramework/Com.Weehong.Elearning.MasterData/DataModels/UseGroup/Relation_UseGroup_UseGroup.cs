using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.UseGroup
{
    /// <summary>
    /// 用户组下属子用户组关联表
    /// </summary>
    [Table("Rrelation_UseGroup_UseGroup")]
    public class Relation_UseGroup_UseGroup
    {

        public Relation_UseGroup_UseGroup Clone()
        {
            return (Relation_UseGroup_UseGroup)this.MemberwiseClone();
        }
        /// <summary>
        /// 用户组ID
        /// </summary>
        [Key]
        [Column("UseGroupID", Order = 0)]
        public Guid UseGroupID { get; set; }
        /// <summary>
        /// 子用户组ID为Rrelation_UseGroup表UseGroupID
        /// </summary>
        public Guid? ChildUserGroupID { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? sort { get; set; }

    }
}
