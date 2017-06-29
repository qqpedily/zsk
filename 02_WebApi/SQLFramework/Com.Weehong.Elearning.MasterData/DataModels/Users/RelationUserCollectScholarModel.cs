

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
    /// <summary>
    /// �û��ղ�ѧ��
    /// </summary>
    [Table("Relation_UserCollectScholar")]
    public partial class RelationUserCollectScholarModel
    {
        public RelationUserCollectScholarModel Clone()
        {
            return (RelationUserCollectScholarModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// ѧ��ID
        /// </summary>
        public Guid? ScholarID { get; set; }
        /// <summary>
        /// ѧ������
        /// </summary>
        public string ScholarName { get; set; }
        /// <summary>
        /// �û�ID �ղ���ID
        /// </summary>
        public Guid? SysUserID { get; set; }
        /// <summary>
        /// �ղ�ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }
        public bool ValidStatus { get; set; }

        //[NotMapped]
        //public UserModel User
        //{
        //    get
        //    {

        //        UserAdapter adapter = new UserAdapter();
        //        UserModel p = adapter.GetAll().Where(w => w.UserID == ScholarID + "").FirstOrDefault();
        //        return p;


        //    }

        //}

        /// <summary>
        /// ѧ�ߵ���Ʒ��
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
