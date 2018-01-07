using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBLL
{
    public class UserLogin
    {
        SystemDAL.UserLogin stulog = new SystemDAL.UserLogin();
        SystemDAL.UserLogin tealog = new SystemDAL.UserLogin();

        //登录，返回真值
        public bool StuLogin(string userName, string userPassword)
        {
            return stulog.StuLogin(userName, userPassword);
        }


        public bool TeaLogin(string userName, string userPassword)
        {
            return tealog.TeaLogin(userName, userPassword);
        }
    }
}
