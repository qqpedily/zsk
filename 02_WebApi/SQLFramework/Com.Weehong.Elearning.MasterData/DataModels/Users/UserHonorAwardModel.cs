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
    /// 用户学术履历中的荣誉奖励
    /// </summary>
    [Table("UserHonorAward")]
    public class UserHonorAwardModel
    {
        public UserHonorAwardModel Clone()
        {
            return   (UserHonorAwardModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 获奖时间
        /// </summary>
        public string HonorAwardTime { get; set; }
        /// <summary>
        /// 获奖信息
        /// </summary>
        public string HonorAwardInfo { get; set; }
        /// <summary>
        /// 获奖信息英文
        /// </summary>
        public string HonorAwardInfoEnglish { get; set; }
        /// <summary>
        /// 获奖说明
        /// </summary>
        public string HonorAwardExplain { get; set; }
        /// <summary>
        /// 获奖说明英文
        /// </summary>
        public string HonorAwardExplainEnglish { get; set; }
        /// <summary>
        /// 授奖机构
        /// </summary>
        public string Mechanism { get; set; }
        /// <summary>
        /// 授奖机构英文
        /// </summary>
        public string MechanismEnglish { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目名称英文
        /// </summary>
        public string ProjectEnglishName { get; set; }
        /// <summary>
        /// 奖励类别（国家级、省部级、其他）
        /// </summary>
        public int? RewardType { get; set; }
        /// <summary>
        /// 奖励等级（一等奖、二等奖、三等奖）
        /// </summary>
        public int? RewardLevel { get; set; }
        /// <summary>
        /// 排名 ?
        /// </summary>
        public string Ranking { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }



    }
}
