using Com.Weehong.Elearning.MasterData.DataAdapter.UseGroup;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.UseGroup
{
    /// <summary>
    /// 用户组管理
    /// </summary>
    [Table("Relation_UseGroup")]
    public class Relation_UseGroup
    {
        public Relation_UseGroup Clone()
        {
            return (Relation_UseGroup)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UseGroupID", Order = 0)]
        public Guid UseGroupID { get; set; }
        /// <summary>
        ///用户组名称
        /// </summary>
        public string UseGroupName { get; set; }
        /// <summary>
        /// 发起人ID，关联SYS_User表UUID
        /// </summary>
        public Guid? SponsorID { get; set; }
        /// <summary>
        /// 用户组创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 用户组描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 用户组类别
        /// </summary>
        public string GroupType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 组成员 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public string GroupMembersName
        {
            get
            {
                string UserName = String.Empty;
                List<UserList> userlist = Relation_UseGroup_UserAdapter.Instance.LoadByUserData(this.UseGroupID, this.SponsorID);
                foreach (var list in userlist)
                {
                    UserName += list.UserName + ",";
                }
                if (!string.IsNullOrWhiteSpace(UserName))
                    UserName = UserName.Substring(0, UserName.Length - 1); ;
                return UserName;
            }
        }

        /// <summary>
        /// 用户组用户 邀请的用户集合
        /// </summary>
        [NotMapped]
        public List<Relation_UseGroup_User> Relation_UseGroup_User { get; set; }

    }



}
