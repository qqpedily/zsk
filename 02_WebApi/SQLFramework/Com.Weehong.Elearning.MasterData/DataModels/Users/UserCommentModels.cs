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
    [Table("UserComment")]
    public class UserCommentModels
    {

        public UserCommentModels Clone()
        {
            return (UserCommentModels)this.MemberwiseClone();
        }
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductionID { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public int Level { get; set; }
        public Guid ParentID { get; set; }
        public DateTime CreateTime { get; set; }
        public bool ValidStatus { get; set; }
       

        public string UserName { get; set; }

      

        public string UploadIMG { get; set; }
        
    }
}
