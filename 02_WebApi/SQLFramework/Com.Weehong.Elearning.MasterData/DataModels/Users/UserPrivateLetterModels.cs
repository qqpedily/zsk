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
    /// 用户评论
    /// </summary>
    [Table("UserPrivateLetter")]
    public class UserPrivateLetterModels
    {

        public UserPrivateLetterModels Clone()
        {
            return (UserPrivateLetterModels)this.MemberwiseClone();
        }
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }

        public Guid UserID { get; set; }
       
        public string UserName { get; set; }
       
        public string UserUploadIMG { get; set; }
        public Guid SendUserID { get; set; }
      
        public string SendUserName { get; set; }
     
        public string SendUserUploadIMG { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public DateTime CreateTime { get; set; }
        public bool ValidStatus { get; set; }
    }
}
