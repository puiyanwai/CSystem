using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SystemDAL
{
    public class stu_reg
    {
        public stu_reg()
        { }

        #region Method


        //对表student进行操作，增加一条记录
        public bool Reg(Model.Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into student( ");
            strSql.Append("id,name,pwd,college,sex,phone_num) ");
            strSql.Append("values ( ");
            strSql.Append("@id,@name,@pwd,@college,@sex,@phone_nume) ");

            SqlParameter[] parameters ={ 
                           new SqlParameter ("@id",SqlDbType.NVarChar,16), 
                           new SqlParameter ("@name",SqlDbType .VarChar ,32),
                           new SqlParameter ("@pwd",SqlDbType .NVarChar,16), 
                           new SqlParameter ("@college",SqlDbType .VarChar ,32),          
                           new SqlParameter ("@sex",SqlDbType .NVarChar,8), 
                           new SqlParameter ("@phone_nume",SqlDbType .VarChar ,16),           
                                 
                                       };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.College;
            parameters[5].Value = model.Phone;

            int rows = SqlDbHelper.ExecuteNonQuery(strSql.ToString(), CommandType.Text, parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Method
    }
}
