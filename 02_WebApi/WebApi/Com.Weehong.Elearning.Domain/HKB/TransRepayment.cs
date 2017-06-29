using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YinGu.Operation.Framework.Domain.WebModel;
using YinGu.Operation.Framework.MasterData.Common;
using YinGu.Operation.Framework.MasterData.DataModels;
using YinGu.Operation.Framework.MasterData.Repositories;
using YinGu.Operation.Framework.MySql.DataModels;
using YinGu.Operation.Framework.MySql.Repositories;

namespace YinGu.Operation.Framework.Domain.HKB
{
    public class TransRepayment
    {
        /// <summary>
        /// 先禁用后增加合同表及附属表
        /// </summary>
        /// <returns></returns>
        public async Task<WebModelIsSucceed> InsertCollectMoney(CollectMoneyInfo collectMoneyInfo, List<CollectMoneyImages> collectMoneyImages)
        {
            using (MySqlManagerDbContext db = new MySqlManagerDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    WebModelIsSucceed isSucceed = new WebModelIsSucceed();
                    db.CollectMoneyInfo.AddOrUpdate(collectMoneyInfo);
                    db.CollectMoneyImages.AddOrUpdate(collectMoneyImages.ToArray());

                    bool mysql = await db.SaveChangesAsync() > 0 ? true : false;
                    //如果还款金额等于应还金额  则表示已经还完！更新SQL逾期记录表 否则只更新mySql还款记录表
                    bool isUpdateSqlOverdueData = collectMoneyInfo.CollectMoney == collectMoneyInfo.ShouldCollectMoney ? true : false;
                    if (!isUpdateSqlOverdueData)
                    {
                        if (mysql)
                        {
                            isSucceed.IsSucceed = true;
                            dbContextTransaction.Commit();
                        }
                        else
                        {
                            isSucceed.IsSucceed = false;
                            dbContextTransaction.Rollback();
                        }
                    }
                    else
                    {
                        using (OperationManagerDbContext dbSql = new OperationManagerDbContext())
                        {
                            using (var dbContextTransactionSql = dbSql.Database.BeginTransaction())
                            {
                                bool sql = false;
                                //更新签约信息表 XD_Contractinfo
                                //ContractInfo contractModel =  dbSql.ContractInfo.Where(w => w.Ordernum == collectMoneyInfo.OrderNum && w.STATUS == EnumStatus.Valid).FirstOrDefault();
                                //if (contractModel == null)
                                //    contractModel = new ContractInfo();
                                //contractModel.RepaymentStatus = EnumRepayment.RepaymentFinish;
                                //dbSql.ContractInfo.AddOrUpdate(contractModel);
                                ////添加还款明细XD_RepaymentDetails
                                //List<RepaymentDetails> overdueModelLst = dbSql.RepaymentDetails.Where(c => c.Ordernum == collectMoneyInfo.OrderNum&&c.RepaymentStatus== EnumRepaymentStatus.PaymentOverdue).ToList();
                                //foreach (var item in overdueModelLst)
                                //{
                                //    item.RepaymentStatus = EnumRepaymentStatus.PaymentAllOverdue;
                                //}
                                //dbSql.RepaymentDetails.AddOrUpdate(overdueModelLst.ToArray());
                                ////更新新逾期统计表OverdueStatistics
                                //OverdueStatistics overdueStatistics =  dbSql.OverdueStatistics.Where(w => w.Ordernum == collectMoneyInfo.OrderNum).FirstOrDefault();
                                //if (overdueStatistics == null)
                                //    return  new WebModelIsSucceed() { IsSucceed = false, ErrorMessage = "订单逾期状态异常，还款失败！" };
                                //overdueStatistics.LastDrawTime = DateTime.Now;//最后还款时间
                                //overdueStatistics.PeriodsType = EnumCollectionPeriodsType.CollectionPeriodsTypeFinish;
                                //overdueStatistics.CollectionStatus = EnumCollectionStatus.CollectionStatusFinish;
                                //dbSql.OverdueStatistics.AddOrUpdate(overdueStatistics);
                                ////更新逾期明细表(按月记录) OverdueDetailed
                                //List<OverdueDetailed> overdueDetailedLst = new List<OverdueDetailed>();
                                //overdueDetailedLst = dbSql.OverdueDetailedModel.Where(c => c.Ordernum == collectMoneyInfo.OrderNum).ToList();
                                //foreach (var item in overdueDetailedLst)
                                //{
                                //    item.DisposeStatus = EnumDisposeStatus.DisposeStatusAll;
                                //}                              
                                //dbSql.OverdueDetailedModel.AddOrUpdate(overdueDetailedLst.ToArray());
                                //sql = await dbSql.SaveChangesAsync() > 0 ? true : false;

                                if (mysql && sql)
                                {
                                    isSucceed.IsSucceed = true;
                                    dbContextTransaction.Commit();
                                    dbContextTransactionSql.Commit();
                                }
                                else
                                {
                                    isSucceed.IsSucceed = false;
                                    dbContextTransaction.Rollback();
                                    dbContextTransactionSql.Rollback();
                                }


                            }
                        }

                    }
                    return isSucceed;
                }
            }
        }
    }
}
