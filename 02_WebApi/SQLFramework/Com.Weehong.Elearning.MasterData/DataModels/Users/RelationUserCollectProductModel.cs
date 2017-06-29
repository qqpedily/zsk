using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataAdapter.Productions;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    /// <summary>
    /// �����ղ�
    /// </summary>
    [Table("Relation_UserCollectProduct")]
    public partial class RelationUserCollectProductModel
    {
        public RelationUserCollectProductModel Clone()
        {
            return (RelationUserCollectProductModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid? ProductionID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string ProductionName { get; set; }
        /// <summary>
        ///�ղ���ID
        /// </summary>
        public Guid? SysUserID { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool ValidStatus { get; set; }

        //private Production _Production;
        //[NotMapped]
        //public Production Production
        //{
        //    get
        //    {
        //        if (_Production == null)
        //        {
        //            ProductionsAdapter adapter = new ProductionsAdapter();
        //            _Production = adapter.GetAll().Where(w => w.ProductionID == ProductionID).FirstOrDefault();
        //        }
        //        return _Production;
        //    }

        //}
    }
}
