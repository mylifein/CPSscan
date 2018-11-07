using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Data;

namespace DAL
{
    public class BCScanned
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.BCScanned model)
        {

            //CurrentTime是指界面上要顯示的時間,SaveTime是保存此條記錄的時間,它們是相同的,都是指服務器上的當前時間


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BCScanned(WorkOrder,Segment,StartQuantity,DepartmentID,DepartmentCode,LineID,LineCode,");
            strSql.Append("IPQC,CurrentTime,AmountPerBox,BarCode,WorkNo,SaveTime,RepositoryID,Attribute7,NDescription) ");
            strSql.Append(" values(@WorkOrder,@Segment,@StartQuantity,@DepartmentID,@DepartmentCode,@LineID,@LineCode,@IPQC,");
            strSql.Append("convert(varchar(100),getdate(),120),@AmountPerBox,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),");
            strSql.Append("@RepositoryID,@Attribute7,@NDescription)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),					
                                            new SqlParameter("@Segment", SqlDbType.VarChar,2000),					
                                            new SqlParameter("@StartQuantity", SqlDbType.BigInt),
                                             new SqlParameter("@DepartmentID", SqlDbType.Int),
                    					    new SqlParameter("@DepartmentCode", SqlDbType.VarChar,4000),
                                            new SqlParameter("@LineID", SqlDbType.Int),
                                            new SqlParameter("@LineCode", SqlDbType.VarChar,4000),
                                            new SqlParameter("@IPQC", SqlDbType.VarChar,2000),					                                            					
                                            new SqlParameter("@AmountPerBox", SqlDbType.BigInt),
                    					    new SqlParameter("@BarCode", SqlDbType.VarChar,2000),
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),
                                            new SqlParameter("@RepositoryID", SqlDbType.VarChar,800),
                                            new SqlParameter("@Attribute7",SqlDbType.VarChar,2000),
                                            new SqlParameter("@NDescription",SqlDbType.VarChar,2000)
                                        };


            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.Segment.Trim();
            parameters[2].Value = model.StartQuantity;
            parameters[3].Value = model.DepartmentID;
            parameters[4].Value = model.DepartmentCode.Trim();
            parameters[5].Value = model.LineID;
            parameters[6].Value = model.LineCode.Trim();
            parameters[7].Value = model.IPQC.Trim();
            parameters[8].Value = model.AmountPerBox;
            parameters[9].Value = model.BarCode.Trim();
            parameters[10].Value = model.WorkNo.Trim();
            parameters[11].Value = model.RepositoryID.Trim();
            parameters[12].Value = model.Attribute7.Trim();
            parameters[13].Value = model.NDescription.Trim();



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
        /// 通過工單,課別ID,線別ID,工號得到最后一條記錄的時間CurrentTime
        /// </summary>
        public string GetLastTime(Model.BCScanned model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(CurrentTime) 'CurrentTime' from BCScanned ");
            strSql.Append(" where workorder=@WorkOrder and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo");                      

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

            object ret_v = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters);                         //得到第1行第1列的值

            if ((ret_v != null) && (ret_v != DBNull.Value))
            {
                return ret_v.ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 通過工單,課別ID,線別ID,工號得到最后一條記錄的每箱數量
        /// </summary>
        public string GetLastAmountPerBox(Model.BCScanned model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select amountperbox from BCScanned ");
            strSql.Append(" where workorder=@WorkOrder and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo ");
            strSql.Append(" and currenttime=(select max(CurrentTime) 'CurrentTime' from BCScanned ");
            strSql.Append(" where workorder=@WorkOrder and departmentid=@DepartmentID and lineid=@LineID and workno=@WorkNo)");

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

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters);

            if ((ds != null) && (ds.Tables.Count >0 ) && (ds.Tables[0].Rows.Count>0))
            {
                return ds.Tables[0].Rows[0][0].ToString().Trim();           //如果此單元格中為DBNull時一樣可以這樣返回,實際DB中不允許為null
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 通過工單查詢已掃描了多少
        /// </summary>        
        public long FinishedAmount(string workorder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bcscanned where workorder=@WorkOrder");

            SqlParameter[] parameters = {	                                           
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900)			                                
                                        };

            parameters[0].Value = workorder.Trim();
         

            object ret_v = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters);                         //得到第1行第1列的值

            if ((ret_v != null) && (ret_v != DBNull.Value) && (ret_v.ToString().Trim()!=""))
            {
                return long.Parse(ret_v.ToString().Trim());
            }
            else
            {
                return -1;         //返回-1,已完工數量不可能為-1
            }

        }
        

        /// <summary>
        /// 通過工單和工號查詢已掃描了多少
        /// </summary>         
        /*
        public long FinishedAmount(string workorder,string workno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from bcscanned where workorder=@WorkOrder and workno=@WorkNo");

            SqlParameter[] parameters = {	                                           
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),		
	                                        new SqlParameter("@WorkNo", SqlDbType.VarChar,2000)
                                        };

            parameters[0].Value = workorder.Trim();
            parameters[1].Value = workno.Trim();


            object ret_v = SQLHelper.ExecuteScalar(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), parameters);                         //得到第1行第1列的值

            if ((ret_v != null) && (ret_v != DBNull.Value) && (ret_v.ToString().Trim() != ""))
            {
                return long.Parse(ret_v.ToString().Trim());
            }
            else
            {
                return -1;         //返回-1,已完工數量不可能為-1
            }

        }
        */
    }
}
