using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Weehong.Elearning.MasterData.DataAdapter.Users;
using Com.Weehong.Elearning.MasterData.Repositories;
using Com.Weehong.Elearning.Domain.WebModel;

namespace YinGu.Operation.Framework.Domain.Users
{
    /// <summary>
    /// 用户别名
    /// </summary>
    public class TransUserAlias
    {

        UserAdapter userAdapter = new UserAdapter();


        /// <summary>
        /// 更改用户别名  默认中英文显示
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="Field">字段</param>
        /// <returns></returns>
        public bool UpdateUserAlias(Guid? UserId,string Field)
        {
            
            using (OperationManagerDbContext db = new OperationManagerDbContext())
            {
                if (!string.IsNullOrEmpty(UserId.ToString()))
                {
                    try
                    {
                        int i =  db.Database.ExecuteSqlCommand("UPDATE UserAlias SET " + Field + "=1 WHERE UserID='" + UserId + "'");
                        if (i > -1)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            
            return false;
        }


    }
}
