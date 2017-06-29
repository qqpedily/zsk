using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户学术履历中的_研究项目
    /// </summary>
    [Table("UserResearchProject")]
    public class UserResearchProjectModel
    {
        public UserResearchProjectModel Clone()
        {
            return (UserResearchProjectModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID 
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目名称(英文)
        /// </summary>
        public string ProjectNameEnglish { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// 资助机构
        /// </summary>
        public string FinancialBody { get; set; }
        /// <summary>
        /// 资助机构英文
        /// </summary>
        public string FinancialBodyEnglish { get; set; }
        /// <summary>
        /// 依托机构
        /// </summary>
        public string RelyingInstitution { get; set; }
        /// <summary>
        /// 依托机构英文
        /// </summary>
        public string RelyingInstitutionEnglish { get; set; }
        /// <summary>
        /// 所属研究计划
        /// </summary>
        public string ResearchPlan { get; set; }
        /// <summary>
        /// 所属研究计划英文
        /// </summary>
        public string ResearchPlanEnglish { get; set; }
        /// <summary>
        /// 资助类型
        /// </summary>
        public string FundingType { get; set; }
        /// <summary>
        /// 资助类型英文
        /// </summary>
        public string FundingTypeEnglish { get; set; }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectLeader { get; set; }
        /// <summary>
        /// 项目负责人英文
        /// </summary>
        public string ProjectLeaderEnglish { get; set; }
        /// <summary>
        /// 项目角色（0为负责人，1为主要成员，2为其他）
        /// </summary>
        public int ProjectRole { get; set; }
        /// <summary>
        /// 主要成员  ?
        /// </summary>
        public string LeadingMember { get; set; }
        /// <summary>
        /// 主要成员英文
        /// </summary>
        public string LeadingMemberEnglish { get; set; }
        /// <summary>
        /// 学科分类
        /// </summary>
        public string SubjectClassification { get; set; }
        /// <summary>
        /// 学科分类英文
        /// </summary>
        public string SubjectClassificationEnglish { get; set; }
        /// <summary>
        /// 经费总额
        /// </summary>
        public string TotalExpenditure { get; set; }
        /// <summary>
        /// 承担任务经费
        /// </summary>
        public string AssumeTaskFund { get; set; }
        /// <summary>
        /// 起讫时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间 
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 项目简介 
        /// </summary>
        public string ProjectProfile { get; set; }
        /// <summary>
        /// 项目简介英文
        /// </summary>
        public string ProjectProfileEnglish { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }




    }
}
