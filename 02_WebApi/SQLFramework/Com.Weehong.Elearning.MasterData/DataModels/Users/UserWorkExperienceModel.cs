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
    /// 用户工作经验
    /// </summary>
    [Table("UserWorkExperience")]
    public class UserWorkExperienceModel
    {
        public UserWorkExperienceModel Clone()
        {
            return (UserWorkExperienceModel)this.MemberwiseClone();
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
        /// 机构英文
        /// </summary>
        public string MechanismEnglish { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 部门英文
        /// </summary>
        public string DeptEnglish { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 职务英文
        /// </summary>
        public string PostEnglish { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 职称英文
        /// </summary>
        public string TitleEnglish { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string RegionContry { get; set; }
        /// <summary>
        /// 省或市
        /// </summary>
        public string RegionProvince { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string RegionCity { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }




    }
}
