using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户别名
    /// </summary>
    [Table("UserAlias")]
    public class UserAliasModel
    {
        public UserAliasModel Clone()
        {
            return (UserAliasModel)this.MemberwiseClone();
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
        /// 别名名称
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 首选中文显示（0为是，1为否）
        /// </summary>
        public int? FirstChineseShow { get; set; }
        /// <summary>
        /// 首选英文显示（0为是，1为否）
        /// </summary>
        public int? FirstEnglishShow { get; set; }

      


    }
}
