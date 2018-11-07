using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class LoginInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.LoginInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LoginInfo(LoginID,IP,LoginTime) ");
            strSql.Append(" values(@LoginID,@IP,convert(varchar(100),getdate(),120))");
          

            SqlParameter[] parameters = {					
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,900),					
                                            new SqlParameter("@IP", SqlDbType.VarChar,2000)                                            
                                        };

            parameters[0].Value = model.LoginID.Trim();
            parameters[1].Value = model.IP.Trim();
            



            //object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根據loginid查詢當前的IP
        /// </summary>
        public SqlDataReader GetIP(string loginid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ip 'ip' from LoginInfo where loginid=@LoginID" );
            


            SqlParameter[] parameters = {					
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,900)                                                                                  
                                        };

            parameters[0].Value = loginid.Trim();
           


            SqlDataReader dr =
            SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return dr;
        }


        /// <summary>
        /// 刪除一条数据
        /// </summary>
        public bool Delete(string loginid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete LoginInfo where loginid=@LoginID");
           


            SqlParameter[] parameters = {					
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,900)                                                                                
                                        };

            parameters[0].Value = loginid.Trim();
       




            //object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
