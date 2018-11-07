using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class User
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string loginid, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from users ");
            strSql.Append(" where loginid=@LoginID and pwd=@Pwd");                      //結果只會是0和大於0的正整數

            SqlParameter[] parameters = {	                                           
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100),
			                                new SqlParameter("@Pwd", SqlDbType.VarChar,2000)
                                        };

            parameters[0].Value = loginid.Trim();
            parameters[1].Value = password.Trim();

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

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into users(loginid,username,deptname,pwd) ");
            strSql.Append(" values(@LoginID,@UserName,@DeptName,@Pwd)");              

            SqlParameter[] parameters = {					
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100),					
                                            new SqlParameter("@UserName", SqlDbType.VarChar,100),					
                                            new SqlParameter("@DeptName", SqlDbType.VarChar,2000),
                    					    new SqlParameter("@Pwd", SqlDbType.VarChar,2000)
                                        };

            parameters[0].Value = model.LoginID.Trim();
            parameters[1].Value = model.UserName.Trim();
            parameters[2].Value = model.DeptName;
            parameters[3].Value = model.Pwd.Trim();
           
            

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string loginid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete users where loginid=@LoginID");


            SqlParameter[] parameters = {					
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100)                                                                                   		
                                        };

            parameters[0].Value = loginid.Trim();

            //int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            int affectedrows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (affectedrows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据,聯合主鍵的刪除
        /// </summary>
        public bool DeleteList(string userlist)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from users where loginid in (" + userlist + ")");

            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);

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
        public bool Update(Model.User model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update users set ");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
            strSql.Append(" username=@UserName,deptname=@DeptName,pwd=@Pwd ");
            strSql.Append(" where loginid=@LoginID");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100),					
                                            new SqlParameter("@UserName", SqlDbType.VarChar,100),					
                                            new SqlParameter("@DeptName", SqlDbType.VarChar,2000),
                                            new SqlParameter("@Pwd", SqlDbType.VarChar,2000)  
                                        };

            parameters[0].Value = model.LoginID.Trim();
            parameters[1].Value = model.UserName.Trim();
            parameters[2].Value = model.DeptName;
            parameters[3].Value = model.Pwd.Trim();
            

            //SQL Bit類型對應ADO.NET的 bool類型           


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

        public bool Update(string loginid, string pwd, string newpwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update users set ");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
            strSql.Append(" pwd=@NewPwd ");
            strSql.Append(" where loginid=@LoginID and pwd=@Pwd");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100),
                                            new SqlParameter("@Pwd", SqlDbType.VarChar,2000),
                                            new SqlParameter("@NewPwd", SqlDbType.VarChar,2000)
                                        };

            parameters[0].Value = loginid.Trim();
            parameters[1].Value = pwd.Trim();
            parameters[2].Value = newpwd.Trim();
            


            //SQL Bit類型對應ADO.NET的 bool類型           


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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select loginid,username,deptname,pwd ");
            strSql.Append(" from users ");

            if ((strWhere != null) && (strWhere.Trim() != ""))
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by loginid asc");

            return SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListOrderByDept(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select loginid,username,deptname,pwd ");
            strSql.Append(" from users ");

            if ((strWhere != null) && (strWhere.Trim() != ""))
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by deptname asc");

            return SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.User DataRowToModel(DataRow row)
        {
            Model.User model = new Model.User();

            if (row != null)
            {
                model.LoginID = row["LoginID"].ToString().Trim();

                model.UserName = row["UserName"].ToString().Trim();      //long.Parse()

                //model.DeptName = row["DeptName"].ToString().Trim();      //long.Parse()

                if (row["DeptName"] == DBNull.Value)
                {
                    model.DeptName = null;
                }
                else
                {
                    model.DeptName = row["DeptName"].ToString().Trim();
                }

                model.Pwd = row["Pwd"].ToString().Trim();

                //model.PalletID = row["PalletID"].ToString().Trim();

                //model.OuterBarCode = row["OuterBarCode"].ToString().Trim();

                //model.ScanTime = DateTime.Parse(row["ScanTime"].ToString().Trim());


                #region
                /*
                model.ItemNo = Int32.Parse(row["ItemNo"].ToString().Trim());

                if (row["NeedScan"] == DBNull.Value)
                {
                    model.NeedScan = null;
                }
                else
                {
                    model.NeedScan = row["NeedScan"].ToString().Trim();
                }

                model.BarCodeLength = Int32.Parse(row["BarCodeLength"].ToString().Trim());

                int.Parse()

                if (row["CanRepeatScan"] == DBNull.Value)
                {
                    model.CanRepeatScan = null;
                }
                else
                {
                    model.CanRepeatScan = bool.Parse(row["CanRepeatScan"].ToString().Trim());           // bool.Parse()      //Convert.ToBoolean() 
                }
                */


                //if (row["Debits"] != null && row["Debits"].ToString() != "")
                //{
                //    model.Debits = decimal.Parse(row["Debits"].ToString());
                //}

                #endregion

            }

            return model;
        }

        /// <summary>
        /// 获得数据列表,多表聯合查詢
        /// </summary>
        public DataSet GetItems(string aircraftno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.aircraftno,a.itemno,a.itemname,a.barcodelength,isnull(b.bcrulecontain,'') 'bcrulecontain' ");
            strSql.Append(" from ScanBarCode_RgstredAircraftNo a left join scanbarcode_lianxiangbcrule b ");
            strSql.Append(" on a.aircraftno=b.aircraftno and a.itemno=b.itemno and a.itemname=b.itemname ");
            strSql.Append(" where a.aircraftno='");
            strSql.Append(aircraftno.Trim());
            strSql.Append("' ");
            strSql.Append(" and (upper(a.itemname)!='NA') and (upper(a.itemname)!='N/A') ");
            strSql.Append(" order by a.itemno asc");

            return SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);

        }

        /// <summary>
        /// 得到一個對象實體
        /// </summary>
        public Model.User GetModel(string loginid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select loginid,username,deptname,pwd ");
            strSql.Append(" from users ");
            strSql.Append(" where loginid=@LoginID");

            SqlParameter[] parameters = {  
                                            new SqlParameter("@LoginID", SqlDbType.VarChar,100)	                                        
                                        };


            parameters[0].Value = loginid.Trim();
            

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 調用一個SP
        /// </summary>
        public bool RunProcedure(string workorder, string outerbarcode, int BCcountperbox)
        {
            SqlParameter[] SqlP = new SqlParameter[]{
                new SqlParameter("@wo",SqlDbType.VarChar,1000),
                new SqlParameter("@obc",SqlDbType.VarChar,1000),
                new SqlParameter("@RQ",SqlDbType.Int),
                
                new SqlParameter("@returnValue",SqlDbType.Int)
            };


            //賦值
            SqlP[0].Value = workorder.Trim();
            SqlP[1].Value = outerbarcode.Trim();
            SqlP[2].Value = BCcountperbox;


            SqlP[3].Direction = ParameterDirection.ReturnValue;               //定義方向,返回值


            object retVal = SQLHelper.ExecuteNonQuery_cg2(SQLHelper.ConnectionString, CommandType.StoredProcedure, "", SqlP);

            if ((retVal == null) || (retVal == DBNull.Value))
            {
                return false;
            }
            else if (int.Parse(retVal.ToString().Trim()) == 0)
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
