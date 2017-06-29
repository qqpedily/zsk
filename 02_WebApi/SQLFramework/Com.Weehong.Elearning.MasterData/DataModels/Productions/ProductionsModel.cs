using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Productions
{
    /// <summary>
    /// 作品文献 
    /// </summary>
    public class ProductionsModel
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid TemplateID { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 文件上传标识 默认没有上传为0；上传为1
        /// </summary>
        public int? IsUploadFileFlag { get; set; }


        /// <summary>
        /// 作品文献字段
        /// </summary>
        public List<ProductionsField> DicProductionsField { get; set; }
    }
}
