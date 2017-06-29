using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Productions
{


    // 静态文献表
    [Table("StaticProductions")]
    public class StaticProductions
    {


        public StaticProductions Clone()
        {
            return (StaticProductions)this.MemberwiseClone();
        }

        [Key]
        [Column("ProductionID", Order = 0)]
        // 作品ID
        public Guid ProductionID { get; set; }
        /// 模板ID
        public Guid? TemplateID { get; set; }
        /// 文献时间
        public DateTime? CreateTime { get; set; }


        public Guid? UserID { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        ///// <summary>
        ///// 下载次数
        ///// </summary>
        //public int? DownloadNum { get; set; }

        ///// <summary>
        ///// 浏览次数
        ///// </summary>
        //public int? BrowseNum { get; set; }

        /// <summary>
        /// 机构Code
        /// </summary>
        public string jigouyuanxiCode { get; set; }

        /// 总页数
        public string Sumpages { get; set; }
        /// 项目资助者
        public string Funder { get; set; }
        /// 期刊论文、会议论文等的通讯作者
        public string Correspondent { get; set; }
        /// 中文摘要
        public string Abstract { get; set; }
        /// 论文被检索工具收录的类别，如SCI、EI等
        public string Indexed { get; set; }
        /// 作品DOI标识符信息
        public string Doi { get; set; }
        /// 数据集类型
        public string Dataset { get; set; }
        /// 作品页码信息
        public string Pages { get; set; }
        /// 期刊全称
        public string Fulljournaltitle { get; set; }
        /// 总被引用次数
        public string Citations { get; set; }
        /// PMID号
        public string Pmid { get; set; }
        /// 类目[WOS]
        public string Wos_subject { get; set; }
        /// 引用的参考文献数
        public string Referencecount { get; set; }
        /// Web of Science类别
        public string Webofscience { get; set; }
        /// 机构院系
        public string Jigouyuanxi { get; set; }
        /// 主题词
        public string Subjectword { get; set; }
        /// 作品的所属部门
        public string Department { get; set; }
        /// 英文摘要
        public string EnglishAbstract { get; set; }
        /// 作品在ISI网站的唯一标识符
        public string Ut { get; set; }
        /// ISSN号
        public string Issn { get; set; }
        /// 版本信息
        public string Version { get; set; }
        /// 作品的项目归属信息
        public string Project { get; set; }
        /// 成果类型
        public string Resulttype { get; set; }
        /// 论文发表期刊的卷信息
        public string Volume { get; set; }
        /// 项目编号
        public string Projectno { get; set; }
        /// 化学信息列表
        public string Chemicallist { get; set; }
        /// 作品发表、出版、发布、报告的日期；学位论文答辩日期、专利授权日期等。
        public string Issued { get; set; }
        /// 作品内容使用的语种信息
        public string Iso { get; set; }
        /// 论文发表期刊的期信息
        public string Issue { get; set; }
        /// 期刊缩写
        public string Journalabbreviation { get; set; }
        /// 作者单位名称
        public string Institution { get; set; }
        /// 国内统一刊号
        public string Cn { get; set; }
        /// 研究领域[WOS]
        public string Wos_subject_extended { get; set; }
        /// 文章类型
        public string Doctype { get; set; }
        /// ORCID号
        public string Orcid { get; set; }
        /// 作者或专利发明人
        public string Author { get; set; }
        /// 参考文献
        public string Reference { get; set; }
        /// 作者或提交者提供的自由或受控关键词
        public string Keyword { get; set; }
        /// 资源在WOS中的记录号
        public string Wosid { get; set; }
        /// 学科主题分类
        public string Discipline { get; set; }
        /// WOS被引用次数
        public string Woscitations { get; set; }
        /// 通讯作者的电子邮箱
        public string Correspondentemail { get; set; }
        /// 资源名称
        public string Title { get; set; }
        /// 作品相关的合作状态信息
        public string Cooperation { get; set; }
        /// 作品署名或产权排序信息
        public string Rank { get; set; }
        /// 链接
        public string Link { get; set; }
        /// 作品的来源或出处信息，如发表或发布的期刊、会议录、图书等。
        public string Source { get; set; }
        /// 关键词[WOS]
        public string Keywords_plus { get; set; }

        /// 出版者
        public string Publisher { get; set; }

        /// 第一作者
        public string FirstAuthor { get; set; }

        /// SCI KeyWords Plus
        public string Wos_headings { get; set; }

        /// EI编号
        public string Ei { get; set; }





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
        private DownloadNumAndBrowseNum _DownloadAndBrowseNum;
        [NotMapped]
        public DownloadNumAndBrowseNum DownloadAndBrowseNum
        {
            get
            {
                if (_DownloadAndBrowseNum == null)
                {

                    DownloadNumAndBrowseNum num = new DownloadNumAndBrowseNum();


                    using (var db = new OperationManagerDbContext())
                    {
                        string sql = @" SELECT DownloadNum,BrowseNum FROM dbo.Productions WHERE ProductionID='" + this.ProductionID + "'";


                        _DownloadAndBrowseNum = db.Database.SqlQuery<DownloadNumAndBrowseNum>(sql).FirstOrDefault();


                    }

                }
                return _DownloadAndBrowseNum;
            }
        }

    }

    public class DownloadNumAndBrowseNum
    {
        ///// <summary>
        ///// 下载次数
        ///// </summary>
        public int? DownloadNum { get; set; }

        ///// <summary>
        ///// 浏览次数
        ///// </summary>
        public int? BrowseNum { get; set; }
    }
}
