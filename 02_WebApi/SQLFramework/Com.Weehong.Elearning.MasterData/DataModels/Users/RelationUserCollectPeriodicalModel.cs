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
using Com.Weehong.Elearning.MasterData.DataAdapter;
using Com.Weehong.Elearning.MasterData.Repositories;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{


    [Table("Relation_UserCollectPeriodical")]
    public partial class RelationUserCollectPeriodicalModel
    {
        public RelationUserCollectPeriodicalModel Clone()
        {
            return (RelationUserCollectPeriodicalModel)this.MemberwiseClone();
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
        public StaticProductions Production
        {
            get
            {

                StaticProductionsAdapter adapter = new StaticProductionsAdapter();


                StaticProductions p = null;
                using (var db = new OperationManagerDbContext())
                {

                    p = db.StaticProductions.Where(w => w.ProductionID == PeriodicalID).FirstOrDefault();


                }


                return p;






            }
        }
        //[NotMapped]
        //public UserModel User
        //{
        //    get
        //    {

        //        UserAdapter adapter = new UserAdapter();
        //        UserModel p = adapter.GetAll().Where(w => w.UserID == SysUserID+"").FirstOrDefault();
        //        return p;


        //    }

        //}
    }
}
