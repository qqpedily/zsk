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
    /// 用户其它任职信息
    /// </summary>
    public class UserOtherPrincipalOfficeModel
    {
        public UserOtherPrincipalOfficeModel Clone()
        {
            return (UserOtherPrincipalOfficeModel)this.MemberwiseClone();
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
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 单位英文名称
        /// </summary>
        public string UnitEnglishName { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 部门英文名称
        /// </summary>
        public string DeptEnglishName { get; set; }
        /// <summary>
        /// 职称名称ID
        /// </summary>
        public Guid PttName { get; set; }
        /// <summary>
        /// 职称英文名称
        /// </summary>
        public string PttEnglishName { get; set; }
        /// <summary>
        /// 身份ID（SYS_IdentityType）
        /// </summary>
        public Guid? ItID { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 职务英文名称
        /// </summary>
        public string PostEnglishName { get; set; }
        /// <summary>
        /// 设为主任职信息（0为是，1为否）
        /// </summary>
        public int? PrincipalOffice { get; set; }
        /// <summary>
        /// 访问权限（公开为0，内部公开为1，不公开为2）
        /// </summary>
        public int? Power { get; set; }




    }
}
