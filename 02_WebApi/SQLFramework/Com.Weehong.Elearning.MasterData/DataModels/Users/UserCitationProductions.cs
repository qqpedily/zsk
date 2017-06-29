using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// 用户引用文献
    /// </summary>
    [Table("UserAlias")]
    public class UserCitationProductions
    {

        public UserCitationProductions Clone()
        {
            return (UserCitationProductions)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("CitationID", Order = 0)]

        public Guid CitationID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductionID { get; set; }

    }
}
