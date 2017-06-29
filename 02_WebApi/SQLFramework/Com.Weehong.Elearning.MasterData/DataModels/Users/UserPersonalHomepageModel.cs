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
    /// 用户_联系信息
    /// </summary>
    [Table("UserPersonalHomepage")]
    public class UserPersonalHomepageModel
    {
        public UserPersonalHomepageModel Clone()
        {
            return (UserPersonalHomepageModel)this.MemberwiseClone();
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
        /// 上传文件路径
        /// </summary>
        public string UploadIMG { get; set; }
        /// <summary>
        /// 访问权限（公开所有访问者均可浏览为0，内部公开仅注册用户可浏览为1，不公开仅限用户本人浏览为2）
        /// </summary>
        public int? PersonalHomepagePower { get; set; }
        /// <summary>
        /// 访问权限（公开:根据用户设置的权限确定个人主页的公开范围为0；
        ///内部公开：该个人主页只能对系统内部用户公开为1；
        ///强制公开：该个人页面强制对所有访问者公开为2；
        ///强制内部公开：该个人主页强制对所用系统内部用户公开为3；
        ///禁止公开：仅用户本人和管理员可浏览该个人主页为4；
        /// </summary>
        public int? AdministratorSettings { get; set; }
        /// <summary>
        /// 个人中文访问主页URL
        /// </summary>
        public string ChineseVersion { get; set; }
        /// <summary>
        /// 个人英文文访问主页URL
        /// </summary>
        public string EnglishVersion { get; set; }




    }
}
