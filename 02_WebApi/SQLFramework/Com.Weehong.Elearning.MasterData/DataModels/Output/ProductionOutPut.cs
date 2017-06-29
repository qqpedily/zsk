
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
namespace Com.Weehong.Elearning.MasterData.DataModels
{
    /// <summary>
    /// 作品文献
    /// </summary>

    public class ProductionOutPut
    {

        /// <summary>
        /// 文献ID
        /// </summary>
        public Guid ProductionID { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public Guid? TemplateID { get; set; }
        /// <summary>
        /// 文献标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类型模板名称
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 下载该文献的量
        /// </summary>
        public Nullable<int> DownloadNum { get; set; }

        /// <summary>
        /// 引用该文献的量
        /// </summary>
        public Nullable<int> CitationNum { get; set; }

        /// <summary>
        /// 浏览该文献的量
        /// </summary>
        public Nullable<int> BrowseNum { get; set; }
        /// <summary>
        /// 作品文献创建时间
        /// </summary>
        public Nullable<DateTime> CreateTime { get; set; }

     




    }

}
