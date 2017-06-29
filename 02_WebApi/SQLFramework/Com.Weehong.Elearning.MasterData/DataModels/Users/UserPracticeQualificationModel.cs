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
    /// 用户执业资格
    /// </summary>
    [Table("UserPracticeQualification")]
    public class UserPracticeQualificationModel
    {
        public UserPracticeQualificationModel Clone()
        {
            return (UserPracticeQualificationModel)this.MemberwiseClone();
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
        /// 名称
        /// </summary>
        public string QualificationName { get; set; }
        /// <summary>
        /// 名称英文
        /// </summary>
        public string QualificationEnglish { get; set; }
        /// <summary>
        /// 颁发机构
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// 颁发机构英文
        /// </summary>
        public string AuthorityEnglish { get; set; }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string ProfessionalCategory { get; set; }
        /// <summary>
        /// 专业类别英文
        /// </summary>
        public string ProfessionalCategoryEnglish { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string RegionContry { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string RegionProvince { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string RegionCity { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 描述英文
        /// </summary>
        public string DescriptionEnglish { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }





    }
}
