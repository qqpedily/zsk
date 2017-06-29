using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.DataAdapter.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.DataModels.Productions;
using Com.Weehong.Elearning.MasterData.DataModels.SysTemplate;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YinGu.Operation.Framework.Domain.Comm;

namespace YinGu.Operation.Framework.Domain.Productions
{
    public class TransProductions
    {
        private Guid GetMetaID(List<SYS_MetaData> list, string qualifier)
        {
            return list.Where(mw => mw.Qualifier == qualifier).FirstOrDefault().MetaDataID;
        }
        /// <summary>
        /// 添加文献信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<WebModelIsSucceed> AddOrUpdateProductionsField(ProductionsModel model)
        {

            WebModelIsSucceed isSucceed = new WebModelIsSucceed();
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    List<ProductionsField> list = new List<ProductionsField>();

                    //作品文献表
                    try
                    {
                        #region 文献
                        Production productions = new Production();
                        productions.ProductionID = Guid.NewGuid();
                        productions.TemplateID = model.TemplateID;
                        productions.IsUploadFileFlag = model.IsUploadFileFlag;
                        productions.UserID = model.UserID;
                        productions.DownloadNum = 0;
                        productions.BrowseNum = 0;
                        productions.CreateTime = DateTime.Now;

                        foreach (var fieldList in model.DicProductionsField)
                        {
                            ProductionsField productionsField = new ProductionsField();
                            productionsField.UUID = Guid.NewGuid();
                            productionsField.ProductionID = productions.ProductionID;
                            productionsField.TemplateID = model.TemplateID;
                            productionsField.MetaDataID = fieldList.MetaDataID;
                            productionsField.FieldSequence = fieldList.FieldSequence;
                            productionsField.DefaultText = fieldList.DefaultText;
                            productionsField.DefaultValue = fieldList.DefaultValue;
                            productionsField.Remark = fieldList.Remark;
                            productionsField.FieldValue = fieldList.FieldValue;
                            list.Add(productionsField);
                        }

                        db.Production.AddOrUpdate(productions);
                        db.ProductionsField.AddOrUpdate(list.ToArray());
                        #endregion

                        #region 未认领
                        RelationUserClaimWorksModel rcw = new RelationUserClaimWorksModel();
                        rcw.ProductionID = productions.ProductionID + "";
                        rcw.UserClaimWorksID = Guid.NewGuid();
                        rcw.SysUserID = model.UserID;
                        rcw.UserClaimWorksStatus = 0;
                        rcw.IsHave = 0;
                        rcw.AuthorOrder = 0;
                        rcw.CorrespondenceAuthor = 0;
                        rcw.ParticipatingAuthor = 0;

                        db.RelationUserClaimWorks.AddOrUpdate(rcw);
                        #endregion

                        #region 大表
                        List<SYS_MetaData> list_Meta = await db.SYS_MetaData.ToListAsync();

                        StaticProductions staticProductions = new StaticProductions();

                        staticProductions.ProductionID = productions.ProductionID;
                        staticProductions.TemplateID = productions.TemplateID.Value;
                        staticProductions.CreateTime = DateTime.Now;
                        staticProductions.UserID = productions.UserID;
                        staticProductions.DataType = model.TemplateName;
                        #region 元数据赋值
                        staticProductions.Abstract = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "abstract")).ToList());
                        staticProductions.Author = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "author")).ToList());
                        staticProductions.Chemicallist = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "chemicallist")).ToList());
                        staticProductions.Citations = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "citations")).ToList());
                        staticProductions.Cn = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "cn")).ToList());
                        staticProductions.Cooperation = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "cooperation")).ToList());
                        staticProductions.Correspondent = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "correspondent")).ToList());
                        staticProductions.Correspondentemail = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "correspondentemail")).ToList());
                        staticProductions.Dataset = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "dataset")).ToList());
                        staticProductions.Department = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "department")).ToList());
                        staticProductions.Discipline = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "discipline")).ToList());
                        staticProductions.Doctype = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "doctype")).ToList());
                        staticProductions.Doi = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "doi")).ToList());
                        staticProductions.Ei = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "Ei")).ToList());
                        staticProductions.EnglishAbstract = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "EnglishAbstract")).ToList());
                        staticProductions.FirstAuthor = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "FirstAuthor")).ToList());
                        staticProductions.Fulljournaltitle = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "fulljournaltitle")).ToList());
                        staticProductions.Funder = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "funder")).ToList());
                        staticProductions.Indexed = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "indexed")).ToList());
                        staticProductions.Institution = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "institution")).ToList());
                        staticProductions.Iso = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "iso")).ToList());
                        staticProductions.Issn = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "issn")).ToList());
                        staticProductions.Issue = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "issue")).ToList());
                        staticProductions.Issued = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "issued")).ToList());
                        staticProductions.Jigouyuanxi = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "jigouyuanxi")).ToList());
                        staticProductions.Journalabbreviation = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "journalabbreviation")).ToList());
                        staticProductions.Keyword = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "keyword")).ToList());
                        staticProductions.Keywords_plus = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "keywords_plus")).ToList());
                        staticProductions.Link = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "link")).ToList());
                        staticProductions.Orcid = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "orcid")).ToList());
                        staticProductions.Pages = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "pages")).ToList());
                        staticProductions.Pmid = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "pmid")).ToList());
                        staticProductions.Project = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "project")).ToList());
                        staticProductions.Projectno = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "projectno")).ToList());
                        staticProductions.Publisher = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "Publisher")).ToList());
                        staticProductions.Rank = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "rank")).ToList());
                        staticProductions.Reference = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "reference")).ToList());
                        staticProductions.Referencecount = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "referencecount")).ToList());
                        staticProductions.Resulttype = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "resulttype")).ToList());
                        staticProductions.Source = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "source")).ToList());
                        staticProductions.Subjectword = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "subjectword")).ToList());
                        staticProductions.Sumpages = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "sumpages")).ToList());
                        staticProductions.Title = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "Title")).ToList());
                        staticProductions.Ut = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "ut")).ToList());
                        staticProductions.Version = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "version")).ToList());
                        staticProductions.Volume = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "volume")).ToList());
                        staticProductions.Webofscience = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "webofscience")).ToList());
                        staticProductions.Wos_headings = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "wos_headings")).ToList());
                        staticProductions.Wos_subject = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "wos_subject")).ToList());
                        staticProductions.Wos_subject_extended = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "wos_subject_extended")).ToList());
                        staticProductions.Woscitations = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "woscitations")).ToList());
                        staticProductions.Wosid = ListChangeString.GetString(list.Where(w => w.MetaDataID == GetMetaID(list_Meta, "wosid")).ToList());
                        #endregion


                        db.StaticProductions.AddOrUpdate(staticProductions);
                        #endregion
                        isSucceed.IsSucceed = await db.SaveChangesAsync() > 0 ? true : false;
                        OperationFiles operationFiles = new OperationFiles();
                        await operationFiles.SaveFile(productions.ProductionID);//上传文档信息
                        dbContextTransaction.Commit();
                        isSucceed.ErrorMessage = productions.ProductionID+"";
                        return isSucceed;
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        isSucceed.IsSucceed = false;
                        isSucceed.ErrorMessage = ex.Message + "\r\n" + ex.StackTrace;
                        return isSucceed;
                    }

                }
            }
        }
        /// <summary>
        /// 获取文献分页数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="curPage">第几页</param>
        /// <returns></returns>
        public async Task<ProductionTotal> GetProductionsList(Guid UserID, int pageSize, int curPage)
        {
            ProductionTotal productionTotal = new ProductionTotal();
            using (var db = new OperationManagerDbContext())
            {
                productionTotal.TotalCount = db.Production.Where(w => w.UserID == UserID).Count();
                //List<Production> list = db.Production.OrderByDescending(o => o.CreateTime).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                //productionTotal.ProductionList = db.Production.OrderByDescending(o => o.CreateTime).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                productionTotal.ProductionList = await db.Production.OrderBy(c => c.CreateTime).Where(w => w.UserID == UserID).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();

                return productionTotal;
            }
        }





    }
}
