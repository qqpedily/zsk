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
    /// 模板文献列表定制
    /// </summary>
    [Table("SYS_TemplateEntryListCustomization")]
    public class SYS_TemplateEntryListCustomization
    {
        public SYS_TemplateEntryListCustomization Clone()
        {
            return (SYS_TemplateEntryListCustomization)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID")]
        public Guid UUID { get; set; }


        /// <summary>
        /// 模板ID
        /// </summary>

        public Guid TemplateID { get; set; }
        /// <summary>
        /// "格式分为如下;
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 模板类型分为插入文本为0；插入字段1；插入分隔符2；
        /// </summary>
        public int? type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? sort { get; set; }

    }
}
