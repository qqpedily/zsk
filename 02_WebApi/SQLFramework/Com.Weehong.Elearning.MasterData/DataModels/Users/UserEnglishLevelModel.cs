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
    /// 用户外语能力
    /// </summary>
    [Table("UserEnglishLevel")]
    public class UserEnglishLevelModel
    {
        public UserEnglishLevelModel Clone()
        {
            return (UserEnglishLevelModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 用户ID  User表UUID
        /// </summary>
        public Guid? UserID { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary> 
        public string LanguageName { get; set; }
        /// <summary>
        /// 语言能力（听为0，说为1，读为2，写为3，同行评议为4）
        /// </summary>
        public string LanguageLevel { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }





    }
}
