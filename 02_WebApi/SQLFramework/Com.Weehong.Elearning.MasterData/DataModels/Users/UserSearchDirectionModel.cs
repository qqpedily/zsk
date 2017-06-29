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
    /// 用户学术履历中的用户研究方向
    /// </summary>
    [Table("UserSearchDirection")]
    public class UserSearchDirectionModel
    {
        public UserSearchDirectionModel Clone()
        {
             return (UserSearchDirectionModel)this.MemberwiseClone();
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
        /// 学科名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 学科英文名称
        /// </summary>
        public string SubjectEnglishName { get; set; }
        /// <summary>
        /// 研究领域名称
        /// </summary>
        public string SearchDirectionNam { get; set; }
        /// <summary>
        /// 研究领域英文名称
        /// </summary>
        public string SearchDirectionEng { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string ThemeName { get; set; }
        /// <summary>
        /// 主题英文名称
        /// </summary>
        public string ThemeEnglishName { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }






    }
}
