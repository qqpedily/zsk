using Com.Weehong.Elearning.DataObjects;
using Com.Weehong.Elearning.MasterData.DataModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Weehong.Elearning.MasterData.DataAdapter.Home
{
    /// <summary>
    /// 用户注册
    /// </summary>
    public class UserRegAdapter: EffectedAdapterBase<UserReg,List<UserReg>>
    {
        public static readonly UserRegAdapter Instance = new UserRegAdapter();


         


    }
}
