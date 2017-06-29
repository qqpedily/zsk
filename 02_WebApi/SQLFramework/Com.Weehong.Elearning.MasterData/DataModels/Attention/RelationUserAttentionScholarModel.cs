

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    [Table("Relation_UserAttentionScholar")]
    public partial class RelationUserAttentionScholarModel
    {
        public RelationUserAttentionScholarModel Clone()
        {
            return (RelationUserAttentionScholarModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        public Guid? ScholarID { get; set; }
        public string ScholarName { get; set; }
        public Guid? SysUserID { get; set; }
        public DateTime? CreateTime { get; set; }
        public short? ValidStatus { get; set; }

        [NotMapped]
        public UserModel User
        {
            get
            {

                UserAdapter adapter = new UserAdapter();
                UserModel p = adapter.GetAll().Where(w => w.UserID == ScholarID + "").FirstOrDefault();
                return p;


            }

        }
        /// <summary>
        /// 学者的作品数
        /// </summary>
        [NotMapped]
        public int? ProductCount
        {
            get
            {
                RelationUserCollectScholarAdapter adapter = new RelationUserCollectScholarAdapter();
                var list = adapter.LoadProductsCountCountByXuezheID(UUID, ScholarName);
                return list;
            }
        }


    }
}
