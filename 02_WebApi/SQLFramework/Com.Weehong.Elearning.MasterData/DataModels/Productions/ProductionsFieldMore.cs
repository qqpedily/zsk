using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Productions
{
    /// <summary>
    /// 作品文献子字段（多值）
    /// </summary>
    [Table("ProductionsFieldMore")]
    public class ProductionsFieldMore
    {
        public ProductionsFieldMore Clone()
        {
            return (ProductionsFieldMore)this.MemberwiseClone();
        }
        /// <summary>
        /// 作品文献子字段（多值）ID
        /// </summary>
        [Key]
        [Column("UUID",Order =0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 作品文献表ID
        /// </summary>
        public Nullable<Guid> ProductionID { get; set; }
        /// <summary>
        /// 字段顺序
        /// </summary>
        public Nullable<int> FieldSequence { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string DefaultText { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }



    }
}
