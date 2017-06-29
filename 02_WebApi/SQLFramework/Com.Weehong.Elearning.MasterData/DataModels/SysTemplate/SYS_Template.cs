using Com.Weehong.Elearning.MasterData.Repositories;
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
    /// 模板类型  模板
    /// </summary>
    [Table("SYS_Template")]
    public class SYS_Template
    {
        public SYS_Template Clone()
        {
            return (SYS_Template)this.MemberwiseClone();
        }
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("TemplateID", Order = 0)]
        public Guid TemplateID { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public int? DataType { get; set; }
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string ContentEnglishName { get; set; }
        /// <summary>
        /// 全文许可定制ID
        /// </summary>
        public Nullable<Guid> FullTextPermissionID { get; set; }

        private List<SYS_TemplateEntryListCustomization> _SYS_TemplateEntryListCustomization;

        /// <summary>
        /// 模板文献列表定制
        /// </summary>
        [NotMapped]
        public List<SYS_TemplateEntryListCustomization> SYS_TemplateEntryListCustomization
        {
            get
            {
                if (_SYS_TemplateEntryListCustomization == null)
                {
                    using (var db = new OperationManagerDbContext())
                    {
                        _SYS_TemplateEntryListCustomization = db.SYS_TemplateEntryListCustomization.Where(w => w.TemplateID == this.TemplateID).OrderBy(o => o.sort).ToList();
                    }
                }
                return _SYS_TemplateEntryListCustomization;
            }
        }

    }
}
