using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
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
    /// 作品文献字段
    /// </summary>
    [Table("ProductionsField")]
    public class ProductionsField
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProductionsField Clone()
        {
            return (ProductionsField)this.MemberwiseClone();
        }
        /// <summary>
        /// 作品文献字段ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 作品文献表ID
        /// </summary>
        public Guid? ProductionID { get; set; }
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


        ///// <summary>
        ///// 字段属性名称
        ///// </summary>
        //public string FieldName
        //{
        //    get
        //    {
        //        ProductionsFieldAdapter adapter = new ProductionsFieldAdapter();
        //        var list = adapter.LoadSYS_MetaDataByID(MetaDataID);
        //        return list.FieldName;
        //    }
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductionsFieldTotal
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<ProductionsField> ProductionsField { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
