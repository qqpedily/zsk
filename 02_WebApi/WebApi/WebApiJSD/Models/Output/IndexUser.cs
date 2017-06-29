using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiZSK.Models.Output
{
    /// <summary>
    /// 首页用户数据
    /// </summary>
    public class IndexUser
    {
        /// <summary>
        /// Code
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string SurnameChinese { get; set; }

        /// <summary>
        /// 名
        /// </summary>
        public string NameChinese { get; set; }

        /// <summary>
        /// 成果数
        /// </summary>
        public int totalcount { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>

        public string UploadIMG { get; set; }

        private string _SubjectName;
        /// <summary>
        /// 研究方向
        /// </summary>
        public string SubjectName
        {
            get
            {
                if (string.IsNullOrEmpty(_SubjectName))
                {
                    using (var db = new OperationManagerDbContext())
                    {
                        var list = db.UserSearchDirection.AsNoTracking().Select(s => new { s.UserID, s.SubjectName }).Where(w => w.UserID == this.UUID).ToList();
                        foreach (var item in list)
                        {
                            _SubjectName += item.SubjectName + ",";
                        }
                    }
                }
                return _SubjectName != null && _SubjectName.Length > 0 ? _SubjectName.Substring(0, _SubjectName.Length - 1) : _SubjectName;
            }
        }
    }
}