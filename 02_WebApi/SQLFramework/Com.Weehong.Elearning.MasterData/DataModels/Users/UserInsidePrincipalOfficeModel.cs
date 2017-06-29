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
    /// 用户内部任职信息
    /// </summary>
    [Table("UserInsidePrincipalOffice")]
    public class UserInsidePrincipalOfficeModel
    {
        public UserInsidePrincipalOfficeModel Clone()
        {
            return (UserInsidePrincipalOfficeModel)this.MemberwiseClone();
        }
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }

        public Guid? UserID { get; set; }

        public Guid? UnitID { get; set; }
        public Guid? DeptID { get; set; }
        public Guid? PttID { get; set; }
        public Guid? ItID { get; set; }
        public string PostName { get; set; }
        public string PostEnglishName { get; set; }
        public int? PrincipalOffice { get; set; }
        public int? Power { get; set; }

    }
}
