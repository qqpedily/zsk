using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.DataModels
{

    /// <summary>
    /// 文献与附件关联表
    /// </summary>
    [Table("ProductionsUploadFile")]
    public class ProductionsUploadFileModel
    {
        public ProductionsUploadFileModel Clone()
        {
            return (ProductionsUploadFileModel)this.MemberwiseClone();
        }

        /// <summary>
        /// 文献id
        /// </summary>
        public string  ProductionID { get; set; }


        /// <summary>
        /// 文件id
        /// </summary>
        public string UploadFile { get; set; }
     
        /// <summary>
        /// 排序
        /// </summary>
        public int FileSequence { get; set; }
    }
}
