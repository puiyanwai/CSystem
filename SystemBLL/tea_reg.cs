using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SystemBLL
{
    public class tea_reg
    {
        //针对chef表的业务处理逻辑类
        //把表示层的数据转发给数据访问层
        SystemDAL.tea_reg dal = new SystemDAL.tea_reg();

        //判断联系人信息是否符合规范
        public bool CheckModel(Model.Teacher model, out string msg)
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
                msg = "工号不正确";
                check = false;
                return check;
            }
            return check;
        }

        //根据检查规范返回真值
        //public bool TeaReg(Model.tea_reg model, out string msg)
        //{
        //    if (!CheckModel(model, out msg))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return dal.TeaReg(model);
        //    }
        //}
    }
}
