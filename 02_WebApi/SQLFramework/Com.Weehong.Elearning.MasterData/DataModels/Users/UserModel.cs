using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.SysManage;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysManage;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("User")]
    public class UserModel
    {
        public UserModel Clone()
        {
            return (UserModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID 
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { set; get; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string UserEmail { set; get; }
        /// <summary>
        /// 姓（中文）
        /// </summary>
        public string SurnameChinese { set; get; }
        /// <summary>
        /// 姓（拼音）
        /// </summary>
        public string SurnamePhoneticize { set; get; }
        /// <summary>
        /// 名（中文）
        /// </summary>
        public string NameChinese { set; get; }
        /// <summary>
        /// 名（拼音）
        /// </summary>
        public string NamePhoneticize { set; get; }
        /// <summary>
        /// orcid
        /// </summary>
        public string Orcid { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { set; get; }
        /// <summary>
        /// 单位（SYS_College的CID）
        /// </summary>
        public System.Guid? UnitID { set; get; }
        /// <summary>
        /// 部门（SYS_College的CID）
        /// </summary>
        public System.Guid? DeptID { set; get; }
        /// <summary>
        /// 职称（SYS_PositionalTitleType表PttID）
        /// </summary>
        public System.Guid? PttID { set; get; }
        /// <summary>
        /// 身份（SYS_IdentityType）
        /// </summary>
        public System.Guid? ItID { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone { set; get; }
        /// <summary>
        /// 可以登录（0可以，1不可以）
        /// </summary>
        public int IsLogin { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { set; get; }
        /// <summary>
        /// 学位
        /// </summary>
        public int? Degree { set; get; }
        /// <summary>
        /// 学位是否公开
        /// </summary>
        public int? DegreeOpen { set; get; }
        /// <summary>
        /// 学历
        /// </summary>
        public int? Education { set; get; }
        /// <summary>
        /// 学历是否公开
        /// </summary>
        public int? EducationOpen { set; get; }
        /// <summary>
        /// ResearchID
        /// </summary>
        public string ResearchID { set; get; }
        /// <summary>
        /// 导师
        /// </summary>
        public int? Tutor { set; get; }
        /// <summary>
        /// 导师是否公开
        /// </summary>
        public int? TutorOpen { set; get; }
        /// <summary>
        /// 是否专家（0为专家，1为否
        /// </summary>
        public int? IsExpert { set; get; }
        /// <summary>
        /// 性别（0为男，1为女）
        /// </summary>
        public int? Sex { set; get; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDay { set; get; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalStatus { set; get; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { set; get; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string BirthPlace { set; get; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PlaceOrigin { set; get; }
        /// <summary>
        /// 个人简介中文
        /// </summary>
        public string PersonalProfile { set; get; }
        /// <summary>
        /// 个人简介英文
        /// </summary>
        public string PersonalProfileEnglish { set; get; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [NotMapped]
        public string Pwd { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderBy { set; get; }

        /// <summary>
        /// 是否是推荐学者 0否 1是
        /// </summary>
        public int? IsRecommend { set; get; }


    }

    



}
