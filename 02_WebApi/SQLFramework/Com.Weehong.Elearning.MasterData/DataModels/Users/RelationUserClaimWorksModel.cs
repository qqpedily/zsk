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
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataAdapter;

namespace Com.Weehong.Elearning.MasterData.DataModels.Users
{
    [Table("Relation_UserClaimWorks")]
    //用户认领作品
    public partial class RelationUserClaimWorksModel
    {
        public RelationUserClaimWorksModel Clone()
        {
            return (RelationUserClaimWorksModel)this.MemberwiseClone();
        }

        [Key]
        [Column("UserClaimWorksID", Order = 0)]
        public Guid UserClaimWorksID { get; set; }
        public string ProductionID { get; set; }
        public Guid? SysUserID { get; set; }
        public int? UserClaimWorksStatus { get; set; }
        public int? IsHave { get; set; }
        public int? AuthorOrder { get; set; }
        public int? CorrespondenceAuthor { get; set; }
        public int? ParticipatingAuthor { get; set; }

        [NotMapped]
        public StaticProductions Production {
            get {

                StaticProductionsAdapter adapter = new  StaticProductionsAdapter();


                StaticProductions p = null;
                using (var db = new OperationManagerDbContext())
                {

                    p= db.StaticProductions.Where(w => w.ProductionID.ToString() == ProductionID).FirstOrDefault();

                   
                }


                return p;
                

            }

        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    public class RelationUserClaimWorksTotal
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<RelationUserClaimWorksModel> RelationUserClaimWorksList { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; }
    }




}
