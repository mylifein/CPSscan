using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class WorkOrderBC
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.WorkOrderBC model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into workorderbc(WorkOrder,Segment,StartQuantity,DepartmentID,DepartmentCode,");
            strSql.Append("ClassCode,DDescription,WorkNo,PrintTime,CompletionSubinventory,Attribute7,NDescription) ");
            strSql.Append(" values(@WorkOrder,@Segment,@StartQuantity,@DepartmentID,@DepartmentCode,@ClassCode,");
            strSql.Append("@DDescription,@WorkNo,convert(varchar(100),getdate(),120),@CompletionSubinventory,@Attribute7,@NDescription)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),					
                                            new SqlParameter("@Segment", SqlDbType.VarChar,2000),					
                                            new SqlParameter("@StartQuantity", SqlDbType.BigInt),
                                            new SqlParameter("@DepartmentID", SqlDbType.Int),
                    					    new SqlParameter("@DepartmentCode", SqlDbType.VarChar,4000),
                                            new SqlParameter("@ClassCode", SqlDbType.VarChar,4000),					
                                            new SqlParameter("@DDescription", SqlDbType.VarChar,4000),
                    					    new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),
                                            new SqlParameter("@CompletionSubinventory", SqlDbType.VarChar,2000),
                                            new SqlParameter("@Attribute7",SqlDbType.VarChar,2000), 
                                            new SqlParameter("@NDescription",SqlDbType.VarChar,2000)
                                        };


            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.Segment.Trim();
            parameters[2].Value = model.StartQuantity;
            parameters[3].Value = model.DepartmentID;
            parameters[4].Value = model.DepartmentCode.Trim();
            parameters[5].Value = model.ClassCode.Trim();
            parameters[6].Value = model.DDescription.Trim();
            parameters[7].Value = model.WorkNo.Trim();
            parameters[8].Value = model.CompletionSubinventory.Trim();
            parameters[9].Value = model.Attribute7.Trim();
            parameters[10].Value = model.NDescription.Trim();



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
        /// 調用一個SP
        /// </summary>
        public DataSet GetWorkOrderInfo(string workorder)
        {
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@WorkOrder",SqlDbType.VarChar,2000)                           
            };


            //賦值
            SqlP[0].Value = workorder.Trim();


            //SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            DataSet myDS = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,
                CommandType.StoredProcedure, "GetWorkOrderInfo_3", SqlP);

            return myDS;

        }

        public string getContainerNum(string model, string department)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ContainerNum from ModelContainerNum where Model=@Model and Department=@Department ");

            SqlParameter[] parameters = { 
                                          new SqlParameter("@Model", SqlDbType.VarChar, 100),
                                          new SqlParameter("@Department", SqlDbType.VarChar, 100)
                                        };

            parameters[0].Value = model.Trim();
            parameters[1].Value = department.Trim();
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
    }
}
