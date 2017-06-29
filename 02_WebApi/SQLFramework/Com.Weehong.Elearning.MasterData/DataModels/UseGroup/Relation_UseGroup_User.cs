using Com.Weehong.Elearning.MasterData.DataAdapter.UseGroup;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.UseGroup;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataModels.UseGroup
{
    /// <summary>
    /// 用户组和用户关联表
    /// </summary>
    [Table("Relation_UseGroup_User")]
    public class Relation_UseGroup_User
    {

        public Relation_UseGroup_User Clone()
        {
            return (Relation_UseGroup_User)this.MemberwiseClone();
        }

        /// <summary>
        /// 用户组ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// 用户组ID
        /// </summary>
        public Guid? UseGroupID { get; set; }
        /// <summary>
        /// 关联SYS_User表UUID  被邀请人  受邀请人
        /// </summary>
        public Guid? SysUserID { get; set; }
        /// <summary>
        /// 邀请时间
        /// </summary>
        public DateTime? InvitationTime { get; set; }
        /// <summary>
        /// 是否加入用户组，未加入为0；已加入为1；拒绝加入为2（代表用户推出该用户组并且删除该条数据）；
        /// </summary>
        public int? Join { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? sort { get; set; }

        /// <summary>
        /// 用户组信息 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public string UseGroupName
        {
            get
            {
                var UseGroupName = String.Empty;
                Relation_UseGroupAdapter rrelation_UseGroupAdapter = new Relation_UseGroupAdapter();
                Relation_UseGroup relation_UseGroup = rrelation_UseGroupAdapter.GetAll().Where(w => w.UseGroupID == this.UseGroupID).FirstOrDefault();
                if (relation_UseGroup != null)
                    UseGroupName = relation_UseGroup.UseGroupName;
                return UseGroupName;
            }
        }
        /// <summary>
        /// 用户组创建时间 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public DateTime? UseGroupCreateTime
        {
            get
            {
                string CreateTime = String.Empty;
                if (this.UseGroupID != null)
                {
                    Relation_UseGroupAdapter rrelation_UseGroupAdapter = new Relation_UseGroupAdapter();
                    Relation_UseGroup relation_UseGroup = rrelation_UseGroupAdapter.GetAll().Where(w => w.UseGroupID == this.UseGroupID).FirstOrDefault();
                    return relation_UseGroup.CreateTime;
                }
                else {
                    return null;
                }
            }
        }
        /// <summary>
        /// 用户组描述 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public string UseGroupDescribe
        {
            get
            {
                if (this.UseGroupID != null)
                {
                    Relation_UseGroupAdapter rrelation_UseGroupAdapter = new Relation_UseGroupAdapter();
                    Relation_UseGroup relation_UseGroup = rrelation_UseGroupAdapter.GetAll().Where(w => w.UseGroupID == this.UseGroupID).FirstOrDefault();
                    return relation_UseGroup.Describe;
                }
                else {
                    return null;
                }
            }
        }



        /// <summary>
        /// 受邀人名称 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public string SysUserName
        {
            get
            {
                if (this.SysUserID != null)
                {
                    UserAdapter userAdapter = new UserAdapter();
                    UserModel userModel = userAdapter.GetAll().Where(w => w.UUID == this.SysUserID).FirstOrDefault();
                    return userModel.SurnameChinese + userModel.NameChinese;
                }
                else {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// 组成员 【编辑 添加不用传值】
        /// </summary>
        [NotMapped]
        public string GroupMembersName
        {
            get
            {
                string UserName = String.Empty;
                List<UserList> userlist = Relation_UseGroup_UserAdapter.Instance.LoadByUserData(this.UseGroupID,this.SysUserID);
                foreach (var list in userlist)
                {
                    UserName += list.UserName +";";
                }
                return UserName;
            }
        }




    }
}
