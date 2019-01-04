using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class InStore
    {
        /// <summary>
        /// 調用一個SP,得到Oracle DB中的所有倉庫
        /// </summary>
        public DataSet GetRepository()
        {
            /*
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@WorkOrder",SqlDbType.VarChar,2000)                           
            };
            */

            //賦值
            /*
            SqlP[0].Value = workorder.Trim();
            */

            //SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            DataSet myDS = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,
                CommandType.StoredProcedure, "GetRepository", null);

            return myDS;

        }

        public string GetRepositoryName(string repositoryID)
        {
            DataSet ds = GetRepository();
            string repositoryid="", repositoryname="";
            //循環DS的行,將數據加入到comboBox
            for (int ds_row = 0; ds_row < ds.Tables[0].Rows.Count; ds_row++)
            {
                repositoryid = ds.Tables[0].Rows[ds_row]["SECONDARY_INVENTORY_NAME"].ToString().Trim();     //倉庫ID
                if (repositoryID.Equals(repositoryid))
                {
                    repositoryname =
                        repositoryid + "   " + ds.Tables[0].Rows[ds_row]["DESCRIPTION"].ToString().Trim();      //倉庫名稱
                    break;
                }
            }
            return repositoryname;
        }

        /// <summary>
        /// 調用一個SP,根據條碼進行入庫
        /// </summary>
        public DataSet PutInStore(Model.InStore model,out int mark)
        {
            
            SqlParameter[] SqlP = {
                new SqlParameter("@barcode",SqlDbType.VarChar,2000),   
                new SqlParameter("@nowscannedworkno",SqlDbType.VarChar,2000),
                new SqlParameter("@repositoryid",SqlDbType.VarChar,800),
                new SqlParameter("@repositoryname",SqlDbType.VarChar,8000),
                new SqlParameter("@mark",SqlDbType.Int)
            };
            

            //賦值
            
            SqlP[0].Value = model.BarCode.Trim();
            SqlP[1].Value = model.NowScannedWorkNo.Trim();
            SqlP[2].Value = model.RepositoryID.Trim();
            SqlP[3].Value = model.RepositoryName.Trim();

            SqlP[4].Direction = ParameterDirection.Output;                      //定義方向,輸出參數

            //SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            DataSet myDS = SQLHelper.ExecuteDataset(out mark,SQLHelper.ConnectionString,
                CommandType.StoredProcedure, "PutInStore_2", SqlP);

            return myDS;

        }

        /// <summary>
        /// 根據工單查詢數據
        /// </summary>
        public DataSet GetList(string workorder)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select workorder,segment,deptlinecode,currentboxamount,repositoryname,barcode,");
            strSql.Append("convert(varchar(100),storedtime,120) 'storedtime',nowscannedworkno,attribute7,ndescription,");
            strSql.Append("cartonno from instore where workorder=@WorkOrder order by storedtime asc");            
            

            
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@WorkOrder",SqlDbType.VarChar,900)                
            };


            //賦值

            SqlP[0].Value = workorder.Trim();                          //工單
            //SqlP[1].Value = model.NowScannedWorkNo.Trim();
            //SqlP[2].Value = model.RepositoryID.Trim();
            //SqlP[3].Value = model.RepositoryName.Trim();

            //SqlP[4].Direction = ParameterDirection.Output;                      //定義方向,輸出參數

            //SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            DataSet myDS = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), SqlP);

            return myDS;

        }

        /// <summary>
        /// 通過工單,計算入庫總和
        /// </summary>
        public string GetStoredTotal(string workorder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(sum(currentboxamount),0) 'totalAmount' from InStore ");
            strSql.Append(" where workorder=@WorkOrder");

            SqlParameter[] parameters = {	                                           
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),                                           
                                        };

            parameters[0].Value = workorder.Trim();
            


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
        /// 根據工單和入庫時間任意組合查詢數據
        /// </summary>
        public DataSet GetListByWhere(string whereStr)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select workorder,segment,deptlinecode,currentboxamount,repositoryname,barcode,");
            strSql.Append("convert(varchar(100),storedtime,120) 'storedtime',nowscannedworkno,attribute7,");
            strSql.Append("ndescription,cartonno from instore where ");
            strSql.Append(whereStr);
            strSql.Append(" order by storedtime asc");



            /*
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@WorkOrder",SqlDbType.VarChar,900)                
            };
            */

            //賦值

            //SqlP[0].Value = workorder.Trim();                          //工單
            //SqlP[1].Value = model.NowScannedWorkNo.Trim();
            //SqlP[2].Value = model.RepositoryID.Trim();
            //SqlP[3].Value = model.RepositoryName.Trim();

            //SqlP[4].Direction = ParameterDirection.Output;                      //定義方向,輸出參數

            //SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            DataSet myDS = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString,
                CommandType.Text, strSql.ToString(), null);

            return myDS;

        }
    }
}
