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
    /// 用户学习经历
    /// </summary>
    [Table("UserLearningExperience")]
    public class UserLearningExperienceModel
    {
        public UserLearningExperienceModel Clone()
        {
            return (UserLearningExperienceModel)this.MemberwiseClone();
        }
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        ///用户ID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 机构
        /// </summary>
        public string Mechanism { get; set; }
        /// <summary>
        /// 机构 英文
        /// </summary>
        public string MechanismEnglish { get; set; }
        /// <summary>
        /// 院系
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 院系英文
        /// </summary>
        public string DeptEnglish { get; set; }
        /// <summary>
        ///专业
        /// </summary>
        public string Major { get; set; }
        /// <summary>
        /// 专业英文
        /// </summary>
        public string MajorEnglish { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string RegionContry { get; set; }
        /// <summary>
        ///省
        /// </summary>
        public string RegionProvince { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string RegionCity { get; set; }
        /// <summary>
        ///专业类别
        /// </summary>
        public string MajorType { get; set; }
        /// <summary>
        /// 专业类别英文
        /// </summary>
        public string MajorTypeEnglish { get; set; }
        /// <summary>
        /// 导师
        /// </summary>
        public string Tutor { get; set; }
        /// <summary>
        /// 导师 英文
        /// </summary>
        public string TutorEnglish { get; set; }
        /// <summary>
        /// 学位（0为博士、1为硕士、2为学士）
        /// </summary>
        public int? Degree { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }







    }
}
