using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Collections;
using System.Data.Entity;
using Com.Weehong.Elearning.Domain.WebModel;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;

namespace YinGu.Operation.Framework.Domain.Users
{
    public class TransUser
    {
        UserAdapter userAdapter = new UserAdapter();
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<WebModelIsSucceed> AddOrUpdateUser(UserModel user)
        {
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    WebModelIsSucceed isSucceed = new WebModelIsSucceed();
                    try
                    {
                        //用户            
                        //UserModel oldUser = userAdapter.GetAll().Where(w => w.UUID == user.UUID).FirstOrDefault();
                        UserModel oldUser = await db.User.Where(w => w.UUID == user.UUID ).FirstOrDefaultAsync();
                        if (oldUser == null)//添加
                        {
                            oldUser.UUID =  Guid.NewGuid() ;
                        }
                        oldUser.UserEmail = user.UserEmail;
                        oldUser.SurnameChinese = user.SurnameChinese;
                        oldUser.SurnamePhoneticize = user.SurnamePhoneticize;
                        oldUser.NameChinese = user.NameChinese;
                        oldUser.NamePhoneticize = user.NamePhoneticize;
                        oldUser.Orcid = user.Orcid;
                        oldUser.UserID = user.UserID;
                        oldUser.UnitID = user.UnitID;
                        oldUser.DeptID = user.DeptID;
                        oldUser.PttID = user.PttID;
                        oldUser.ItID = user.ItID;
                        oldUser.Telphone = user.Telphone;
                        oldUser.IsLogin = user.IsLogin;
                        oldUser.PassWord = user.PassWord;
                        oldUser.Degree = user.Degree;
                        oldUser.DegreeOpen = user.DegreeOpen;
                        oldUser.Education = user.Education;
                        oldUser.EducationOpen = user.EducationOpen;
                        oldUser.ResearchID = user.ResearchID;
                        oldUser.Tutor = user.Tutor;
                        oldUser.TutorOpen = user.TutorOpen;
                        oldUser.IsExpert = user.IsExpert;
                        oldUser.Sex = user.Sex;
                        oldUser.BirthDay = user.BirthDay;
                        oldUser.PoliticalStatus = user.PoliticalStatus;
                        oldUser.Nation = user.Nation;
                        oldUser.BirthPlace = user.BirthPlace;
                        oldUser.PlaceOrigin = user.PlaceOrigin;
                        oldUser.PersonalProfile = user.PersonalProfile;
                        oldUser.PersonalProfileEnglish = user.PersonalProfileEnglish;

                        db.User.AddOrUpdate(oldUser);
                        dbContextTransaction.Commit();

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
        /// 获取用户分页数据
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        /// <param name="curPage">第几页</param>
        /// <returns></returns>
        public UserTotal GetUserTotal(int pageSize, int curPage)
        {
            UserTotal userTotal = new UserTotal();
            using (var db = new OperationManagerDbContext())
            {
                //userTotal.TotalCount =  db.User.Count();
                userTotal.TotalCount = db.User.Count();
                userTotal.UserList =  db.User.OrderByDescending(c => c.SurnameChinese).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToList();
                
                return userTotal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="curPage"></param>
        /// <returns></returns>
        public async Task<List<UserModel>> GetUserlList(int pageSize, int curPage)
        {

            using (var db = new OperationManagerDbContext())
            {
                await db.User.CountAsync();
                return await db.User.OrderByDescending(c => c.SurnameChinese).Take(pageSize * curPage).Skip(pageSize * (curPage - 1)).ToListAsync();

            }
        }




    }
}
