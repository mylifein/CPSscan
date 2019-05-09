using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace DAL
{
    public class ContainerNumSet
    {
        public string getUserDepartment(string loginedid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DeptName from Users where LoginID=@LoginID ");

            SqlParameter[] parameters = { new SqlParameter("@LoginID", SqlDbType.VarChar, 100) };

            parameters[0].Value = loginedid.Trim();
            object ret_v = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters);                         //得到第1行第1列的值

            if ((ret_v != null) && (ret_v != DBNull.Value))
            {
                return ret_v.ToString().Trim();
            }
            else
            {
                return "0";
            }
        }

        public void save(string dept, string model, string num)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ModelContainerNum(Model, Department, ContainerNum) values (@Model, @Department, @ContainerNum)");
            SqlParameter[] parameters = {					
                                            new SqlParameter("@Model", SqlDbType.VarChar,50),					
                                            new SqlParameter("@Department", SqlDbType.VarChar,50),	
                                             new SqlParameter("@ContainerNum", SqlDbType.Int),
                                         };
            parameters[0].Value = model;
            parameters[1].Value = dept;
            parameters[2].Value = num;
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);


        }
    }
}
