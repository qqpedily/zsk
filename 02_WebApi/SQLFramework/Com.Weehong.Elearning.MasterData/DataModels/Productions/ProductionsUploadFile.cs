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
    /// 作品文献上传文件
    /// </summary>
    [Table("ProductionsUploadFile")]
    public class ProductionsUploadFile
    {
        public ProductionsUploadFile Clone()
        {
            return (ProductionsUploadFile)this.MemberwiseClone();
        }
        /// <summary>
        /// 作品文献表ID
        /// </summary>
        [Key]
        [Column("ProductionID", Order =0)]
        public Guid ProductionID { get; set; }
        /// <summary>
        /// 附件上传
        /// </summary>
        public string UploadFile { get; set; }
        /// <summary>
        /// 附件上传顺序
        /// </summary>
        public int? FileSequence { get; set; }



    }
}
