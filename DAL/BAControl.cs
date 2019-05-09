using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;
using System.Data.SqlClient;

namespace DAL
{
    public class BAControl
    {
       
        public DataSet getControlDetail()
        {
            String sql = "select convert(varchar(16),STARTTIME,120) STARTTIME, convert(varchar(16),ENDTIME,120) ENDTIME, STATUS from dbo.BAControl WHERE ID = (SELECT MAX(ID) FROM BAControl)";
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, sql);
            return ds;
        }

        public void saveBAControl(string startTime, string endTime, string user, string status)
        {
            String sql = "insert into BAControl(StartTime, EndTime, Status, LoginUser, UpdateTime) Values(@starttime, @endtime, @status, @user, getdate())";
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@starttime",SqlDbType.VarChar,2000),                          
                new SqlParameter("@endtime",SqlDbType.VarChar,2000),
                new SqlParameter("@user",SqlDbType.VarChar,2000),          
                new SqlParameter("@status",SqlDbType.VarChar,2000)
            };
            SqlP[0].Value = startTime;
            SqlP[1].Value = endTime;
            SqlP[2].Value = user;
            SqlP[3].Value = status;
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, sql, SqlP);
        }

        public bool checkBAControl()
        {
            DataSet ds = getControlDetail();
            if (ds.Tables[0].Rows.Count == 1)
            {
                string startTime = ds.Tables[0].Rows[0][0].ToString();
                string endTime = ds.Tables[0].Rows[0][1].ToString();
                string status = ds.Tables[0].Rows[0][2].ToString();
                if (status.Equals("1"))
                {
                    DateTime startDT = Convert.ToDateTime(startTime);
                    DateTime endDT = Convert.ToDateTime(endTime);
                    int m = DateTime.Compare(startDT, DateTime.Now);
                    int n = DateTime.Compare(endDT, DateTime.Now);
                    if (m <= 0 && n >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
