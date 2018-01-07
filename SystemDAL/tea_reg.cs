using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SystemDAL
{
    public class tea_reg
    {
        public tea_reg()
        { }

        #region Method


        //增加一条记录
        public bool TeaReg(Model.tea_reg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into chef( ");
            strSql.Append("id,name,pwd,phone_num) ");
            strSql.Append("values ( ");
            strSql.Append("@id,@name,@pwd,@phone_nume) ");

            SqlParameter[] parameters ={ 
                           new SqlParameter ("@id",SqlDbType.NVarChar,16), 
                           new SqlParameter ("@name",SqlDbType .VarChar ,32),
                           new SqlParameter ("@pwd",SqlDbType .NVarChar,16),                           
                           new SqlParameter ("@phone_nume",SqlDbType .VarChar ,16),           
                                 
                                       };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Password;
            parameters[3].Value = model.Phone_num;

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