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
    /// 荣耀头衔
    /// </summary>
    [Table("UserHonor")]
    public class UserHonorModel
    {
        public UserHonorModel Clone()
        {
            return (UserHonorModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID 
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { set; get; }

        /// <summary>
        /// 中文
        /// </summary>
        public string ChineseName { set; get; }

        /// <summary>
        /// 英文
        /// </summary>
        public string EnglishName { set; get; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { set; get; }
    }
}
