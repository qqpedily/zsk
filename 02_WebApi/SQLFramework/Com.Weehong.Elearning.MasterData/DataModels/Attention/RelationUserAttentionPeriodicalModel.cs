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


    [Table("Relation_UserAttentionPeriodical")]
    public partial class RelationUserAttentionPeriodicalModel
    {
        public RelationUserAttentionPeriodicalModel Clone()
        {
            return (RelationUserAttentionPeriodicalModel)this.MemberwiseClone();
        }
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("UUID", Order = 0)]
        public Guid UUID { get; set; }
        public Guid? PeriodicalID { get; set; }
        public string PeriodicalName { get; set; }
        public Guid? SysUserID { get; set; }
        public DateTime? CreateTime { get; set; }
        public short? ValidStatus { get; set; }

        [NotMapped]
        public Production Production
        {
            get
            {

                ProductionsAdapter adapter = new ProductionsAdapter();
                Production p = adapter.GetAll().Where(w => w.ProductionID == PeriodicalID).FirstOrDefault();
                return p;


            }

        }
    }
}
