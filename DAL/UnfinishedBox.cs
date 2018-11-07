using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class UnfinishedBox
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.UnfinishedBox model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UnfinishedBox(WorkOrder,DepartmentID,LineID,SaveTime,WorkNo,ItemNo) ");
            strSql.Append(" values(@WorkOrder,@DepartmentID,@LineID,convert(varchar(100),getdate(),120),@WorkNo,@ItemNo)");
            

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),	
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),                    					    
                                            new SqlParameter("@LineID", SqlDbType.Int),  
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),
                                            new SqlParameter("@ItemNo", SqlDbType.Int), 
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;            
            parameters[3].Value = model.WorkNo.Trim();
            parameters[4].Value = model.ItemNo;   




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
        /// 根據工單,課別ID,線別ID,工號查詢當前的項次號
        /// </summary>
        public SqlDataReader GetItemNo(Model.UnfinishedBox model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(itemno) 'itemno' from unfinishedbox where workorder=@WorkOrder and departmentid=@DepartmentID ");
            strSql.Append(" and lineid=@LineID and workno=@WorkNo");            


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


            SqlDataReader dr=
            SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            return dr;
        }

        /// <summary>
        /// 刪除一条数据
        /// </summary>
        public bool Delete(Model.UnfinishedBox model)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete unfinishedbox where workorder=@WorkOrder and departmentid=@DepartmentID ");
            strSql.Append(" and lineid=@LineID and workno=@WorkNo");
            


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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Model.UnfinishedBox model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UnfinishedBox ");
            strSql.Append(" where workorder=@WorkOrder and departmentid=@DepartmentID ");
            strSql.Append(" and lineid=@LineID and workno=@WorkNo");                           //結果只會是0和大於0的正整數

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


            int rows = int.Parse(SQLHelper.ExecuteScalar(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters).ToString().Trim());


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
