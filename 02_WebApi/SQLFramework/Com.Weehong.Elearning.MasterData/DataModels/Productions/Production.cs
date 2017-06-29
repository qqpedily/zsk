
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using Com.Weehong.Elearning.DataHelper;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using System.Threading.Tasks;
using YinGu.Operation.Framework.Domain.Comm;

namespace Com.Weehong.Elearning.MasterData.DataModels.Productions
{
    /// <summary>
    /// 作品文献
    /// </summary>
    [Table("Productions")]
    public class Production
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Production Clone()
        {
            return (Production)this.MemberwiseClone();
        }

        /// <summary>
        /// 作品文献表ID
        /// </summary>
        [Key]
        [Column("ProductionID", Order = 0)]
        public Guid ProductionID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid? TemplateID { get; set; }
        /// <summary>
        /// 文件是否上传标识；默认没有上传为0；上传为1；
        /// </summary>
        public int? IsUploadFileFlag { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 下载该文献的量
        /// </summary>
        public Nullable<int> DownloadNum { get; set; }

        /// <summary>
        /// 引用该文献的量
        /// </summary>
        public int? CitationNum { get; set; }

        /// <summary>
        /// 浏览该文献的量
        /// </summary>
        public Nullable<int> BrowseNum { get; set; }
        /// <summary>
        /// 作品文献创建时间
        /// </summary>
        public Nullable<DateTime> CreateTime { get; set; }


        /// <summary>
        /// 文献信息
        /// </summary>
        [NotMapped]
        public DataTable ProductionsFieldsObj
        {
            get
            {
                OperationData data = new OperationData();
                ProductionsFieldAdapter adapter = new ProductionsFieldAdapter();
                List<ProductionsField> list = adapter.LoadByProductionIDAndTemplateID(ProductionID, TemplateID);

                return data.RCC_DataTableByProduction(list);


            }
            set { }
        }

        /// <summary>
        /// 学者名称
        /// </summary>
        [NotMapped]
        public string UserName
        {
            get
            {
                UserModel userModel = null;
                using (var db = new OperationManagerDbContext())
                {
                    userModel = db.User.Where(w => w.UUID == this.UserID).FirstOrDefault();

                    if (userModel != null)
                    {
                        return userModel.SurnameChinese + userModel.NameChinese;
                    }
                    else
                    {
                        return "";
                    }
                }

            }
        }


        private List<AttachmentModel> _CommAttachmentList;
        /// <summary>
        /// 文献附件
        /// </summary>
        [NotMapped]
        public List<AttachmentModel> CommAttachmentList
        {
            get
            {
                if (_CommAttachmentList == null)
                {

                    AttachmentAdapter adapter = new AttachmentAdapter();
                    _CommAttachmentList = adapter.LoadByProductionsAsync(this.ProductionID.ToString());
                }
                return _CommAttachmentList;
            }
        }



    }

}
