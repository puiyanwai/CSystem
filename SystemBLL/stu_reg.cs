using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SystemBLL
{
    public class stu_reg
    {
        //针对student表的业务处理逻辑类
        //把表示层的数据转发给数据访问层
        SystemDAL.stu_reg dal = new SystemDAL.stu_reg();


        //判断信息是否符合规范
        public bool CheckModel(Model.Student model, out string msg)
        {
            bool check = true;
            msg = "";
            if (model.Name == "")
            {
                msg = "名称不能为空";
                check = false;
                return check;
            }
            if (!Utilities.IsVaildPhoneNumber(model.Phone))
            {
                msg = "手机号码不正确";
                check = false;
                return check;
            }
            if (!Utilities.IsValidId(model.Id))
            {
                msg = "学号不正确";
                check = false;
                return check;
            }
            return check;
        }

        //注册，根据规范检查结果返回真值
        //public bool Reg(Model.Student model, out string msg)
        //{
            //if (!CheckModel(model, out msg))
            //{
            //    return false;
            //}
            //else
            //{
            //    return dal.Reg(model);
            //}
        //}


    }
}
