using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Com.Weehong.Elearning.DataModels;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.UseGroup;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;

namespace Com.Weehong.Elearning.MasterData.Repositories
{
    public class OperationManagerDbContext : DbContext
    {
        public OperationManagerDbContext()
            : base("OperationManager") { }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual DbSet<UserModel> User { get; set; }        


        /// <summary>
        /// 用户别名
        /// </summary>
        public virtual DbSet<UserAliasModel> UserAlia { get; set; }
        /// <summary>
        /// 用户_联系信息
        /// </summary>
        public virtual DbSet<UserContactInformationModel> UserContactInformation { get; set; }

        /// <summary>
        /// 用户外语能力
        /// </summary>
        public virtual DbSet<UserEnglishLevelModel> UserEnglishLevel { get; set; }
        /// <summary>
        /// 荣耀头衔
        /// </summary>
        public virtual DbSet<UserHonorModel> UserHonor { get; set; }
        /// <summary>
        /// 用户学术履历中的荣誉奖励
        /// </summary>
        public virtual DbSet<UserHonorAwardModel> UserHonorAward { get; set; }
        /// <summary>
        /// 用户内部任职信息
        /// </summary>
        public virtual DbSet<UserInsidePrincipalOfficeModel> UserInsidePrincipalOffice { get; set; }

        /// <summary>
        /// 用户学习经历
        /// </summary>
        public virtual DbSet<UserLearningExperienceModel> UserLearningExperience { get; set; }
        /// <summary>
        /// 用户其他任职信息
        /// </summary>
        public virtual DbSet<UserOtherPrincipalOfficeModel> UserOtherPrincipalOffice { get; set; }
        
       /// <summary>
       ///用户_联系表
       /// </summary>
        public virtual DbSet<UserPersonalHomepageModel> UserPersonalHomepage { get; set; }
        /// <summary>
        /// 用户职业资格
        /// </summary>
        public virtual DbSet<UserPracticeQualificationModel> UserPracticeQualification { get; set; }
        /// <summary>
        /// 用户学术履历中的_研究项目
        /// </summary>
        public virtual DbSet<UserResearchProjectModel> UserResearchProject { get; set; }
        /// <summary>
        /// 用户学术履历中的用户研究方向
        /// </summary>
        public virtual DbSet<UserSearchDirectionModel> UserSearchDirection { get; set; }

        /// <summary>
        /// 用户工作经验
        /// </summary>
        public virtual DbSet<UserWorkExperienceModel> UserWorkExperience { get; set; }


        /// <summary>
        /// 附件
        /// </summary>
        public virtual DbSet<AttachmentModel> Attachment { get; set; }
        /// <summary>
        /// 模板字段
        /// </summary>
        public virtual DbSet<SYS_TemplateField> SYS_TemplateField { get; set; }
        /// <summary>
        /// 全文许可定制
        /// </summary>
        public virtual DbSet<SYS_FullTextPermission> SYS_FullTextPermission { get; set; }
        /// <summary>
        /// 元数据管理
        /// </summary>
        public virtual DbSet<SYS_MetaData> SYS_MetaData { get; set; }
        /// <summary>
        ///模板类型  模板
        /// </summary>
        public virtual DbSet<SYS_Template> SYS_Template { get; set; }
        /// <summary>
        ///模板引用格式定制
        /// </summary>
        public virtual DbSet<SYS_TemplateCustomFormatting> SYS_TemplateCustomFormatting { get; set; }
        /// <summary>
        /// 模板文献列表定制
        /// </summary>
        public virtual DbSet<SYS_TemplateEntryListCustomization> SYS_TemplateEntryListCustomization { get; set; }
        /// <summary>
        ///作品文献
        /// </summary>
        public virtual DbSet<Production> Production { get; set; }
        /// <summary>
        /// 作品文献字段
        /// </summary>
        public virtual DbSet<ProductionsField> ProductionsField { get; set; }
        /// <summary>
        /// 作品文献子字段(多值)
        /// </summary>
        public virtual DbSet<ProductionsFieldMore> ProductionsFieldMore { get; set; }
        /// <summary>
        /// 作品文献上传文件
        /// </summary>
        public virtual DbSet<ProductionsUploadFile> ProductionsUploadFile { get; set; }

        /// <summary>
        /// 期刊
        /// </summary>
        public virtual DbSet<RelationUserCollectPeriodicalModel> RelationUserCollectPeriodical { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual DbSet<RelationUserCollectProductModel> RelationUserCollectProduct { get; set; }


        /// <summary>
        /// 学者
        /// </summary>
        public virtual DbSet<RelationUserCollectScholarModel> RelationUserCollectScholar { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public virtual DbSet<RelationUserCollectThemeModel> RelationUserCollectTheme { get; set; }

        /// <summary>
        /// 用户认领作品
        /// </summary>
        public virtual DbSet<RelationUserClaimWorksModel> RelationUserClaimWorks { get; set; }

        /// <summary>
        /// 用户组管理
        /// </summary>
        public virtual DbSet<Relation_UseGroup> Relation_UseGroup { get; set; }

        /// <summary>
        /// 用户组和用户关联表
        /// </summary>
        public virtual DbSet<Relation_UseGroup_User> Relation_UseGroup_User { get; set; }

        /// <summary>
        /// 用户职称
        /// </summary>
        public virtual DbSet<SYS_PositionalTitleType> SYSPositionalTitleType { get; set; }

        /// <summary>
        /// 用户关注期刊
        /// </summary>
        public virtual DbSet<RelationUserAttentionPeriodicalModel> RelationUserAttentionPeriodical { get; set; }


        /// <summary>
        /// 用户关注学者
        /// </summary>
        public virtual DbSet<RelationUserAttentionScholarModel> RelationUserAttentionScholar { get; set; }

        /// <summary>
        /// 用户关注主题
        /// </summary>
        public virtual DbSet<RelationUserAttentionThemeModel> RelationUserAttentionTheme { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        public virtual DbSet<SYS_IdentityType> SYS_IdentityType { get; set; }
        /// <summary>
        /// 院系
        /// </summary>
        public virtual DbSet<SYS_College> SYS_College { get; set; }


        /// <summary>
        /// 院用户评论
        /// </summary>
        public virtual DbSet<UserCommentModels> UserCommentModels { get; set; }

        /// <summary>
        /// 用户私信
        /// </summary>
        public virtual DbSet<UserPrivateLetterModels> UserPrivateLetterModels { get; set; }


        /// <summary>
        /// 用户私信
        /// </summary>
        public virtual DbSet<StaticProductions> StaticProductions { get; set; }
    }
}
