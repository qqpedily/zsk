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
    /// 元数据管理
    /// </summary>
    [Table("SYS_MetaData")]
    public class SYS_MetaData
    {
        public SYS_MetaData Clone()
        {
            return (SYS_MetaData)this.MemberwiseClone();
        }
        /// <summary>
        /// 元数据管理ID
        /// </summary>
        [Key]
        [Column("MetaDataID",Order =0)]
        public Guid MetaDataID { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段英文名称
        /// </summary>
        public string FieldEnglishName { get; set; }
        /// <summary>
        /// element (要素)
        /// </summary>
        public string Element { get; set; }
        /// <summary>
        /// qualifier 
        /// </summary>
        public string Qualifier { get; set; }
        /// <summary>
        /// 定义说明
        /// </summary>
        public string Definition { get; set; }
        /// <summary>
        /// 定义说明英文
        /// </summary>
        public string DefinitionEnglish { get; set; }
        /// <summary>
        /// 英文值显示效果（保持原样为0，简写为1）
        /// </summary>
        public Nullable<int> Display { get; set; }
        /// <summary>
        /// 适用于统计(是为0，否为1)
        /// </summary>
        public Nullable<int> IsCount { get; set; }
        /// <summary>
        /// 适用于统计目录显示(是为0，否为1)
        /// </summary>
        public Nullable<int> IsCountDir { get; set; }
        /// <summary>
        /// 输入统计形式(文本框为0，复选框为1)
        /// </summary>
        public Nullable<int> CountStyle { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public Nullable<int> Number { get; set; }

        /// <summary>
        /// KEY
        /// </summary>
        public int MetaDataCode { get; set; }
    }
}
