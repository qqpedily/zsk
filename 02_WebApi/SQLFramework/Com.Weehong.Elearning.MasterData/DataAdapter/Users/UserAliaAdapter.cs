using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Users;
using System.Globalization;
using Pinyin4net;
using Com.Weehong.Elearning.MasterData.Repositories;
using System.Data.Entity;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Users
{
    /// <summary>
    /// 用户别名
    /// </summary>
    public class UserAliasAdapter : EffectedAdapterBase<UserAliasModel, List<UserAliasModel>>
    {
        /// <summary>
        /// 根据用户ID查询别名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public  UserAliasModel LoadByUserID(Guid userID)
        {
            using (var db = new OperationManagerDbContext())
            {
                return db.UserAlia.Where(w => w.UserID == userID).FirstOrDefault();
            }
        }

        public void SetAllUserAlias()
        {
            using (OperationManagerDbContext au = new OperationManagerDbContext())
            {
                var userL = au.User.ToList();
                int u1 = userL.Count();

                foreach (var u in userL)
                {
                    // u.isup = "1";
                    //  u1--;
                    //Console.WriteLine("共(" + u1 + ")个============" + u.姓名 + "=============");

                    var Binfo = GetNamePinyin(u, "big");//XinYu
                    var Linfo = GetNamePinyin(u, "little"); //xinyu

                    var fuxing = GetNamePinyin1(u);

                    // au.UserTable.Add(Binfo);


                    // NameRules n = new NameRules();

                    UserAliasModel n = new UserAliasModel();

                    //1.Tian XinYu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + " " + Binfo.PYSurname,
                        // UserTableID = Binfo.ID
                        UUID = Guid.NewGuid(),

                        AliasName = Binfo.PYName + " " + Binfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)


                    });

                    //2.XinYu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYSurname + " " + Binfo.PYName,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYSurname + " " + Binfo.PYName,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });


                    //3.Tian XY
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + " " + GetUpper(Binfo.PYSurname),
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYName + " " + GetUpper(Binfo.PYSurname),
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });

                    //4.XY Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = GetUpper(Binfo.PYSurname) + " " + Binfo.PYName,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = GetUpper(Binfo.PYSurname) + " " + Binfo.PYName,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)


                    });

                    //5.Tian, xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + ", " + Linfo.PYSurname,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYName + ", " + Linfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)

                    });

                    //6.Xinyu T
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q6,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q6,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //7.Tian Xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q7,
                        //UserTableID = Linfo.ID
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q7,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //8.Tian Xin-yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q8,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q8,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //9.Tian X.Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q9,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q9,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //10.Tian X.-Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q10,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q10,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //11.Xinyu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q11,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q11,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //12.Xin-yu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q12,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q12,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //13.Tian, XinYu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q13,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q13,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });


                    //14.Tian, Xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q14,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q14,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //15.Tian, XY
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q15,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q15,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //16.Tian, Xin-yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q16,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q16,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //17.Tian, X.Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q17,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q17,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //18.Tian, X.-Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q18,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q18,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //19.Tian Xinyu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q19,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q19,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //20.Tian Xinyu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q20,
                        //UserTableID = Linfo.ID
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q20,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //21.Tian XinYu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q21,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q21,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //22.Tian XinYu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q22,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q22,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //23.Xin-Yu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q23,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q23,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //24.Tian Xin-Yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q24,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q24,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //25.Tian, XinYu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q25,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //26.Tian, XinYu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q26,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q26,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //27.Tian, Xinyu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q27,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q27,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //28.Tian XY(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q28,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q28,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //29.Tian XY（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q29,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q29,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //30.tianxinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Linfo.PYName + Linfo.PYSurname,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Linfo.PYName + Linfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });
                }
                au.SaveChanges();
            }
        }


        public void SetUserAliasByUserID(string userid)
        {
            using (OperationManagerDbContext au = new OperationManagerDbContext())
            {
                var userL = au.User.Where(s => s.UUID.ToString() == userid).ToList();
                // int u1 = userL.Count();

                foreach (var u in userL)
                {
                    // u.isup = "1";
                    //  u1--;
                    //Console.WriteLine("共(" + u1 + ")个============" + u.姓名 + "=============");

                    var Binfo = GetNamePinyin(u, "big");//XinYu
                    var Linfo = GetNamePinyin(u, "little"); //xinyu

                    var fuxing = GetNamePinyin1(u);

                    // au.UserTable.Add(Binfo);


                    // NameRules n = new NameRules();

                    UserAliasModel n = new UserAliasModel();

                    //1.Tian XinYu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + " " + Binfo.PYSurname,
                        // UserTableID = Binfo.ID
                        UUID = Guid.NewGuid(),

                        AliasName = Binfo.PYName + " " + Binfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)


                    });

                    //2.XinYu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYSurname + " " + Binfo.PYName,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYSurname + " " + Binfo.PYName,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });


                    //3.Tian XY
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + " " + GetUpper(Binfo.PYSurname),
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYName + " " + GetUpper(Binfo.PYSurname),
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });

                    //4.XY Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = GetUpper(Binfo.PYSurname) + " " + Binfo.PYName,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = GetUpper(Binfo.PYSurname) + " " + Binfo.PYName,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)


                    });

                    //5.Tian, xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Binfo.PYName + ", " + Linfo.PYSurname,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Binfo.PYName + ", " + Linfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)

                    });

                    //6.Xinyu T
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q6,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q6,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //7.Tian Xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q7,
                        //UserTableID = Linfo.ID
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q7,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //8.Tian Xin-yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q8,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q8,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //9.Tian X.Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q9,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q9,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //10.Tian X.-Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q10,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q10,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //11.Xinyu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q11,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q11,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //12.Xin-yu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q12,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q12,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //13.Tian, XinYu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q13,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q13,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });


                    //14.Tian, Xinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q14,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q14,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //15.Tian, XY
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q15,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q15,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //16.Tian, Xin-yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q16,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q16,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //17.Tian, X.Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q17,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q17,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //18.Tian, X.-Y.
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q18,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q18,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //19.Tian Xinyu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q19,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q19,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //20.Tian Xinyu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q20,
                        //UserTableID = Linfo.ID
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q20,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //21.Tian XinYu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q21,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q21,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //22.Tian XinYu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q22,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q22,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //23.Xin-Yu Tian
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q23,
                        //UserTableID = Linfo.ID


                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q23,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)

                    });

                    //24.Tian Xin-Yu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q24,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q24,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //25.Tian, XinYu(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q25,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //26.Tian, XinYu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q26,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q26,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //27.Tian, Xinyu（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q27,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q27,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //28.Tian XY(田新玉)
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q28,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q28,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //29.Tian XY（田新玉）
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = fuxing.GS.q29,
                        //UserTableID = Linfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = fuxing.GS.q29,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Linfo.ID)
                    });

                    //30.tianxinyu
                    au.UserAlia.Add(new UserAliasModel
                    {
                        //SplitName = Linfo.PYName + Linfo.PYSurname,
                        //UserTableID = Binfo.ID

                        UUID = Guid.NewGuid(),
                        AliasName = Linfo.PYName + Linfo.PYSurname,
                        FirstChineseShow = 1,
                        FirstEnglishShow = 0,
                        UserID = new Guid(Binfo.ID)
                    });
                }
                au.SaveChanges();
            }
        }


        public static UserTableS GetNamePinyin1(UserModel u)
        {
            geshi geshi = new geshi();
            UserTableS aus = new UserTableS();
            UserTable au = new UserTable();
            au.Name = u.SurnameChinese + u.NameChinese;
            au.ID = u.UUID.ToString();

            string xingming = u.SurnameChinese + u.NameChinese;

            try
            {
                if (xingming.Length == 2)
                {
                    if (XingDuoYin.ContainsKey(xingming[0].ToString()))
                    {
                        au.CName = xingming[0].ToString();
                        au.PYName = XingDuoYin[xingming[0].ToString()];
                    }
                    else
                    {
                        au.CName = xingming[0].ToString();
                        au.PYName = ConvertPinyin(xingming[0], "big");
                    }

                    au.CSurname = xingming[1].ToString();
                    au.PYSurname = ConvertPinyin(xingming[1], "big");


                    string dade = ConvertPinyin(xingming[1], "big");
                    string dadeN = GetUpper(dade);


                    geshi.q6 = dade + " " + GetUpper(au.PYName);
                    geshi.q7 = au.PYName + " " + dade;
                    geshi.q8 = au.PYName + " " + dade + "-";
                    geshi.q9 = au.PYName + " " + dadeN + ".";
                    geshi.q10 = au.PYName + " " + dadeN + ".-";
                    geshi.q11 = dade + " " + au.PYName;
                    geshi.q12 = dade + "-" + " " + au.PYName;
                    geshi.q13 = au.PYName + "," + dade;
                    geshi.q14 = au.PYName + "," + dade;
                    geshi.q15 = au.PYName + "," + dadeN;
                    geshi.q16 = au.PYName + "," + dade + "-";
                    geshi.q17 = au.PYName + ", " + dadeN + ".";
                    geshi.q18 = au.PYName + ", " + dadeN + ".-";
                    geshi.q19 = au.PYName + " " + dade + "(" + au.Name + ")";
                    geshi.q20 = au.PYName + " " + dade + " (" + au.Name + ")";
                    geshi.q21 = au.PYName + " " + dade + "(" + au.Name + ")";
                    geshi.q22 = au.PYName + " " + dade + " (" + au.Name + ")";
                    geshi.q23 = dade + " " + au.PYName;
                    geshi.q24 = au.PYName + " " + dade;
                    geshi.q25 = au.PYName + ", " + dade + "(" + au.Name + ")";
                    geshi.q26 = au.PYName + ", " + dade + " (" + au.Name + ")";
                    geshi.q27 = au.PYName + ", " + dade + " (" + au.Name + ")";
                    geshi.q28 = au.PYName + " " + dadeN + "(" + au.Name + ")";
                    geshi.q29 = au.PYName + " " + dadeN + " (" + au.Name + ")";



                }
                else
                {
                    int isFuXing = Fuxing.Count(s => xingming.StartsWith(s)) > 0 ? 2 : 1;
                    for (int i = 0; i < xingming.Length; i++)
                    {
                        if (i == 0 && isFuXing == 1 && XingDuoYin.ContainsKey(xingming[0].ToString()))
                        {
                            au.CName = xingming[0].ToString();
                            au.PYName = XingDuoYin[xingming[0].ToString()];
                        }
                        else if (i < isFuXing)
                        {

                            au.PYName += ConvertPinyin(xingming[i], "big");
                            au.CName += xingming[i];
                        }
                        else
                        {
                            string dade = ConvertPinyin(xingming[i], "big");
                            string xiaode = ConvertPinyin(xingming[i], "little");

                            if (i == isFuXing)
                            {
                                geshi.q6 += dade;
                                geshi.q7 += dade;
                                geshi.q8 += dade + "-";
                                geshi.q10 += GetUpper(dade) + ".-";
                                geshi.q11 += dade;
                                geshi.q12 += dade + "-";
                                geshi.q14 += dade;
                                geshi.q16 += dade + "-";
                                geshi.q18 += GetUpper(dade) + ".-";
                                geshi.q19 += dade;
                                geshi.q20 += dade;
                                geshi.q23 += dade;
                                geshi.q24 += dade;
                                geshi.q27 += dade;

                            }
                            else
                            {
                                geshi.q6 += xiaode;
                                geshi.q7 += xiaode;
                                geshi.q8 += xiaode;
                                geshi.q10 += GetUpper(dade) + ".";
                                geshi.q11 += xiaode;
                                geshi.q12 += xiaode;
                                geshi.q14 += xiaode;
                                geshi.q16 += xiaode;
                                geshi.q18 += GetUpper(dade) + ".";
                                geshi.q19 += xiaode;
                                geshi.q20 += xiaode;
                                geshi.q23 += "-" + xiaode;
                                geshi.q24 += "-" + xiaode;
                                geshi.q27 += xiaode;
                            }
                            geshi.q9 += GetUpper(dade) + ".";
                            geshi.q13 += dade;
                            geshi.q15 += GetUpper(dade);
                            geshi.q17 += GetUpper(dade) + ".";
                            geshi.q21 += dade;
                            geshi.q22 += dade;
                            geshi.q25 += dade;
                            geshi.q26 += dade;
                            geshi.q28 += GetUpper(dade);
                            geshi.q29 += GetUpper(dade);

                            au.CSurname += xingming[i];

                        }
                    }
                    geshi.q6 = geshi.q6 + " " + GetUpper(au.PYName);
                    geshi.q7 = au.PYName + " " + geshi.q7;
                    geshi.q8 = au.PYName + " " + geshi.q8;
                    geshi.q9 = au.PYName + " " + geshi.q9;
                    geshi.q10 = au.PYName + " " + geshi.q10;
                    geshi.q11 = geshi.q11 + " " + au.PYName;
                    geshi.q12 = geshi.q12 + " " + au.PYName;
                    geshi.q13 = au.PYName + "," + geshi.q13;
                    geshi.q14 = au.PYName + "," + geshi.q14;
                    geshi.q15 = au.PYName + "," + geshi.q15;
                    geshi.q16 = au.PYName + "," + geshi.q16;
                    geshi.q17 = au.PYName + ", " + geshi.q17;
                    geshi.q18 = au.PYName + ", " + geshi.q18;
                    geshi.q19 = au.PYName + " " + geshi.q19 + "(" + au.Name + ")";
                    geshi.q20 = au.PYName + " " + geshi.q20 + " (" + au.Name + ")";
                    geshi.q21 = au.PYName + " " + geshi.q21 + "(" + au.Name + ")";
                    geshi.q22 = au.PYName + " " + geshi.q22 + " (" + au.Name + ")";
                    geshi.q23 = geshi.q23 + " " + au.PYName;
                    geshi.q24 = au.PYName + " " + geshi.q24;
                    geshi.q25 = au.PYName + ", " + geshi.q25 + "(" + au.Name + ")";
                    geshi.q26 = au.PYName + ", " + geshi.q26 + " (" + au.Name + ")";
                    geshi.q27 = au.PYName + ", " + geshi.q27 + " (" + au.Name + ")";
                    geshi.q28 = au.PYName + " " + geshi.q28 + "(" + au.Name + ")";
                    geshi.q29 = au.PYName + " " + geshi.q29 + " (" + au.Name + ")";




                }
            }
            catch { }
            aus.GS = geshi;
            aus.UT = au;
            return aus;

        }




        public static UserTable GetNamePinyin(UserModel u, string isFormat)
        {

            UserTable au = new UserTable();
            string xingming = u.SurnameChinese + u.NameChinese;

            au.Name = xingming;
            au.ID = u.UUID.ToString();
            au.UserID = u.UUID + "";



            try
            {
                if (xingming.Length == 2)
                {
                    if (XingDuoYin.ContainsKey(xingming[0].ToString()))
                    {
                        au.CName = xingming[0].ToString();
                        au.PYName = XingDuoYin[xingming[0].ToString()];
                    }
                    else
                    {
                        au.CName = xingming[0].ToString();
                        au.PYName = ConvertPinyin(xingming[0], isFormat);
                    }

                    au.CSurname = xingming[1].ToString();
                    au.PYSurname = ConvertPinyin(xingming[1], isFormat);
                }
                else
                {
                    int isFuXing = Fuxing.Count(s => xingming.StartsWith(s)) > 0 ? 2 : 1;
                    for (int i = 0; i < xingming.Length; i++)
                    {
                        if (i == 0 && isFuXing == 1 && XingDuoYin.ContainsKey(xingming[0].ToString()))
                        {
                            au.CName = xingming[0].ToString();
                            au.PYName = XingDuoYin[xingming[0].ToString()];
                        }
                        else if (i < isFuXing)
                        {

                            au.PYName += ConvertPinyin(xingming[i], isFormat);
                            au.CName += xingming[i];
                        }
                        else
                        {
                            au.PYSurname += ConvertPinyin(xingming[i], isFormat);
                            au.CSurname += xingming[i];
                        }
                    }
                }
            }
            catch { }

            return au;

        }



        public static string ConvertPinyin(char c, string isFormat)
        {


            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo text = cultureInfo.TextInfo;

            Pinyin4net.Format.HanyuPinyinOutputFormat format = new Pinyin4net.Format.HanyuPinyinOutputFormat();
            format.CaseType = Pinyin4net.Format.HanyuPinyinCaseType.LOWERCASE;
            format.ToneType = Pinyin4net.Format.HanyuPinyinToneType.WITHOUT_TONE;
            format.VCharType = Pinyin4net.Format.HanyuPinyinVCharType.WITH_U_UNICODE;
            var py = PinyinHelper.ToHanyuPinyinStringArray(c, format);
            if (py == null)
                return string.Empty;

            if (isFormat == "big")
            {
                return text.ToTitleCase(py.FirstOrDefault());
            }
            else
            {
                return py.FirstOrDefault();
            }
        }

        public static string GetUpper(string str)
        {
            var result = "";
            if (str != null)
            {
                foreach (var item in str.ToArray())
                {
                    if (item >= 'A' && item <= 'Z')
                    {
                        result += item;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 复姓
        /// </summary>
        public static List<string> Fuxing = new List<string>() { "欧阳", "太史", "端木", "上官", "司马", "东方", "独孤", "南宫", "万俟", "闻人", "夏侯", "诸葛", "尉迟", "公羊", "赫连", "澹台", "皇甫", "宗政", "濮阳", "公冶", "太叔", "申屠", "公孙", "慕容", "仲孙", "钟离", "长孙", "宇文", "司徒", "鲜于", "司空", "闾丘", "子车", "亓官", "司寇", "巫马", "公西", "颛孙", "壤驷", "公良", "漆雕", "乐正", "宰父", "谷梁", "拓跋", "夹谷", "轩辕", "令狐", "段干", "百里", "呼延", "东郭", "南门", "羊舌", "微生", "公户", "公玉", "公仪", "梁丘", "公仲", "公上", "公门", "公山", "公坚", "左丘", "公伯", "西门", "公祖", "第五", "公乘", "贯丘", "公皙", "南荣", "东里", "东宫", "仲长", "子书", "子桑", "即墨", "达奚", "褚师" };
        static Dictionary<string, string> XingDuoYin = new Dictionary<string, string> {
            {"覃","Qin"},
            {"冯","Feng"},
            {"缪","Miao"},
            {"夏","Xia"},
            {"瞿","Qu"},
            {"曾","Zeng"},
            {"石","Shi"},
            {"解","Xie"},
            {"折","She"},
            {"那","Na"},
            {"藏","Zang"},
            {"褚","Chu"},
            {"扎","Zha"},
            {"景","Jing"},
            {"翟","Zhai"},
            {"都","Du"},
            {"六","Lu"},
            {"薄","Bo"},
            {"郇","Xun"},
            {"晁","Chao"},
            {"贲","Ben"},
            {"贾","Jia"},
            {"的","De"},
            {"馮","Feng"},
            {"哈","Ha"},
            {"居","Ju"},
            {"尾","Wei"},
            {"盖","Gai"},
            {"查","Zha"},
            {"盛","Sheng"},
            {"塔","Ta"},
            {"和","He"},
            {"柏","Bai"},
            {"朴","Piao"},
            {"蓝","Lan"},
            {"牟","Mu"},
            {"殷","Yin"},
            {"谷","Gu"},
            {"乾","Qian"},
            {"陆","Lu"},
            {"乜","Nie"},
            {"乐","Yue"},
            {"陶","Tao"},
            {"阚","Kan"},
            {"叶","Ye"},
            {"强","Qiang"},
            {"不","Bu"},
            {"丁","Ding"},
            {"阿","A"},
            {"汤","Tang"},
            {"万","Wan"},
            {"车","Che"},
            {"称","Chen"},
            {"沈","Shen"},
            {"区","Ou"},
            {"仇","Qiu"},
            {"宿","Su"},
            {"南","Nan"},
            {"单","Shan"},
            {"卜","Bu"},
            {"鸟","Niao"},
            {"思","Si"},
            {"寻","Xun"},
            {"於","Yu"},
            {"烟","Yan"},
            {"余","Yu"},
            {"浅","Qian"},
            {"艾","Ai"},
            {"浣","Wan"},
            {"无","Wu"},
            {"信","Xin"},
            {"許","Xu"},
            {"齐","Qi"},
            {"俞","Yu"},
            {"若","Ruo"}
        };
    }

    public partial class geshi
    {
        public int id { get; set; }
        public string name { get; set; }
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
        public string q6 { get; set; }
        public string q7 { get; set; }
        public string q8 { get; set; }
        public string q9 { get; set; }
        public string q10 { get; set; }
        public string q11 { get; set; }
        public string q12 { get; set; }
        public string q13 { get; set; }
        public string q14 { get; set; }
        public string q15 { get; set; }
        public string q16 { get; set; }
        public string q17 { get; set; }
        public string q18 { get; set; }
        public string q19 { get; set; }
        public string q20 { get; set; }
        public string q21 { get; set; }
        public string q22 { get; set; }
        public string q23 { get; set; }
        public string q24 { get; set; }
        public string q25 { get; set; }
        public string q26 { get; set; }
        public string q27 { get; set; }
        public string q28 { get; set; }
        public string q29 { get; set; }
        public string q30 { get; set; }
        public string q31 { get; set; }
    }

    public partial class UserTableS
    {


        public UserTable UT { get; set; }

        public geshi GS { get; set; }
    }

    public partial class UserTable
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CSurname { get; set; }
        public string CName { get; set; }
        public string PYSurname { get; set; }
        public string PYName { get; set; }
        public string UserID { get; set; }
    }
}