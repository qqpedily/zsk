using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
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
    /// 模板字段
    /// </summary>
    /// <returns></returns>
    [Table("SYS_TemplateField")]
    public class SYS_TemplateField
    {

        public SYS_TemplateField Clone()
        {
            return (SYS_TemplateField)this.MemberwiseClone();
        }
        /// <summary>
        /// 模板字段ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid? TemplateID { get; set; }
        /// <summary>
        /// 元数据ID
        /// </summary>
        public Guid? MetaDataID { get; set; }
        /// <summary>
        /// 字段顺序
        /// </summary>
        public int? FieldSequence { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段类型（0为下拉列表，1为单行文本框，2为文本域）
        /// </summary>
        public int? FieldType { get; set; }
        /// <summary>
        /// 默认
        /// </summary>
        public string DefaultText { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 是否必填（0为是，1为否）
        /// </summary>
        public int? NeedFill { get; set; }
        /// <summary>
        /// 是否多值（0为是，1为否）
        /// </summary>
        public int? MoreValue { get; set; }
        /// <summary>
        /// 是否显示（0为是，1为否）
        /// </summary>
        public int? IsShow { get; set; }
        /// <summary>
        /// 英文值显示效果（0为保持原样，1为句首字母大写，2为标题式首字母大写，3为首字母大写样式，4为全部大写，5为全部小写）
        /// </summary>
        public int? EnglishShow { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #region  

        #endregion

        /// <summary>
        /// 元数据管理
        /// </summary>
        [NotMapped]
        public SYS_MetaData SYS_MetaData
        {
            get
            {
                SYS_MetaDataAdapter sYS_MetaDataAdapter = new SYS_MetaDataAdapter();
                return sYS_MetaDataAdapter.GetAll().Where(w => w.MetaDataID == this.MetaDataID).FirstOrDefault();
            }
        }
        ///// <summary>
        ///// 模板类型 模板
        ///// </summary>
        //[NotMapped]
        //public SYS_Template SYS_Template
        //{
        //    get
        //    {
        //        SYS_TemplateAdapter sYS_TemplateAdapter = new SYS_TemplateAdapter();
        //        return sYS_TemplateAdapter.GetAll().Where(w => w.TemplateID == this.TemplateID).FirstOrDefault();
        //    }
        //}


    }
}
