using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class TimeSegment
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.TimeSegment model)
        {           
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into TimeSegment(WorkOrder,DepartmentID,LineID,WorkNo,Time0,AutoTime) ");
            strSql.Append(" values(@WorkOrder,@DepartmentID,@LineID,@WorkNo,convert(varchar(100),getdate(),120),convert(varchar(100),getdate(),120))");
            //strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					   
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000)                                                                                                                         
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;          
            parameters[3].Value = model.WorkNo.Trim();
            
            
                       




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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.TimeSegment model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update timesegment set time1=convert(varchar(100),getdate(),120),reason=@Reason where workorder=@WorkOrder "); 
            strSql.Append(" and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo and time1 is null");
            
            //strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					   
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),                                           
                    					    new SqlParameter("@Reason", SqlDbType.VarChar,8000)                                          
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;
            parameters[3].Value = model.WorkNo.Trim();             
            parameters[4].Value = model.Reason.Trim();





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
        /// 更新自動時間
        /// </summary>
        public bool UpdateAutoTime(Model.TimeSegment model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update timesegment set autotime=convert(varchar(100),getdate(),120) where workorder=@WorkOrder ");
            strSql.Append(" and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo and time1 is null");

            //strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					   
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000)                                                               					                                              
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;
            parameters[3].Value = model.WorkNo.Trim();
            





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
        /// 更新異常記錄的暫停時間和自動時間一樣
        /// </summary>
        public bool UpdateAbnormal(Model.TimeSegment model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update timesegment set time1=autotime where workorder=@WorkOrder ");
            strSql.Append(" and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo and (time1 is null) and (isover is null)");

            //strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					   
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),                                                               					                                              
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;
            parameters[3].Value = model.WorkNo.Trim();
            





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
        /// 更新結束時間
        /// </summary>
        public bool UpdateEndTime(Model.TimeSegment model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update timesegment set time1=convert(varchar(100),getdate(),120),isover=1 where workorder=@WorkOrder ");
            strSql.Append(" and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo and time1 is null");

            //strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					   
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000)                                                               					                                              
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;
            parameters[3].Value = model.WorkNo.Trim();
            




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
