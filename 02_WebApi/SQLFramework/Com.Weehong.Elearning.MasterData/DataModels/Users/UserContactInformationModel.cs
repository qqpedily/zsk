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
    [Table("UserContactInformation")]
    public class UserContactInformationModel
    {
        public UserContactInformationModel Clone()
        {
            return (UserContactInformationModel)this.MemberwiseClone();
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
        /// 注册邮箱
        /// </summary>
        public string RegisteredMail { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsRegisteredMail { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string ContactMail { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsContactMail { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficeTelephone { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsOfficeTelephone { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsMobilePhone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsFax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsZipCode { get; set; }
        /// <summary>
        /// 通信地址
        /// </summary>
        public string MailingAddress { get; set; }
        /// <summary>
        /// 通信地址
        /// </summary>
        public string MailingAddressEnglish { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsMailingAddress { get; set; }
        /// <summary>
        /// 博客地址
        /// </summary>
        public string Blog { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsBlog { get; set; }
        /// <summary>
        /// 链接名称
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// 链接名称  英文
        /// </summary>
        public string LinkNameEnglinsh { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkAddress { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? IsLinkAddress { get; set; }






    }
}
