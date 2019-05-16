using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DBUtility;

namespace DAL
{
    public class OuterBarCode
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.OuterBarCode model)
        {

            //CurrentTime是指界面上要顯示的時間,SaveTime是保存此條記錄的時間,它們是相同的,都是指服務器上的當前時間


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into OuterBarCode(WorkOrder,DepartmentID,LineID,DeptLineCode,Segment,CurrentBoxAmount,");
            strSql.Append("IPQC,CurrentBoxTime,BarCode,WorkNo,SaveTime,RepositoryID,Attribute7,NDescription,CartonNO) ");
            strSql.Append(" values(@WorkOrder,@DepartmentID,@LineID,@DeptLineCode,@Segment,@CurrentBoxAmount,@IPQC,");
            strSql.Append("@CurrentBoxTime,@BarCode,@WorkNo,convert(varchar(100),getdate(),120),@RepositoryID,@Attribute7,@NDescription,@CartonNO)");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),
					                        new SqlParameter("@DepartmentID", SqlDbType.Int),	
                                            new SqlParameter("@LineID", SqlDbType.Int),	
                                            new SqlParameter("@DeptLineCode", SqlDbType.VarChar,8000),					
                                            new SqlParameter("@Segment", SqlDbType.VarChar,2000),
                    					    new SqlParameter("@CurrentBoxAmount", SqlDbType.BigInt),
                                            new SqlParameter("@IPQC", SqlDbType.VarChar,2000),
                                            new SqlParameter("@CurrentBoxTime", SqlDbType.DateTime),					                                            					
                                            new SqlParameter("@BarCode", SqlDbType.VarChar,2000),
                    					    new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),
                                            new SqlParameter("@RepositoryID", SqlDbType.VarChar,800),
                                            new SqlParameter("@Attribute7", SqlDbType.VarChar,2000),
                                            new SqlParameter("@NDescription", SqlDbType.VarChar,2000),
                                            new SqlParameter("@CartonNO", SqlDbType.Int)
                                        };


            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.DepartmentID;
            parameters[2].Value = model.LineID;
            parameters[3].Value = model.DeptLineCode.Trim();
            parameters[4].Value = model.Segment.Trim();
            parameters[5].Value = model.CurrentBoxAmount;
            parameters[6].Value = model.IPQC.Trim();
            parameters[7].Value = model.CurrentBoxTime;
            parameters[8].Value = model.BarCode.Trim();
            parameters[9].Value = model.WorkNo.Trim();
            parameters[10].Value = model.RepositoryID.Trim();
            parameters[11].Value = model.Attribute7.Trim();
            parameters[12].Value = model.NDescription.Trim();
            parameters[13].Value = model.CartonNO;



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
        /// 得到一批数据,根據工單和工號
        /// </summary>
        /*
        public DataSet GetList(string workorder,string workno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WorkOrder,DeptLineCode,Segment,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
            strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo from OuterBarCode ");
            strSql.Append(" where workorder=@WorkOrder and workno=@WorkNo order by CurrentBoxTime asc");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),					
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000)                                           
                                        };

            parameters[0].Value = workorder.Trim();
            parameters[1].Value = workno.Trim();
            


            //SQL Bit類型對應ADO.NET的 bool類型           


            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

                        
            return ds;
            
        }
        */

        /// <summary>
        /// 得到一批数据,根據工單
        /// </summary>
        public DataSet GetList(string workorder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WorkOrder,DeptLineCode,Segment,RepositoryID,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
            strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo,Attribute7,NDescription,");
            strSql.Append("CartonNO from OuterBarCode where workorder=@WorkOrder AND (Flag <> 1 OR FLAG IS NULL) AND BarCode NOT IN (select BarCode FROM InStore where InStore.WorkOrder = @WorkOrder)  order by CurrentBoxTime asc");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900)
                                        };

            parameters[0].Value = workorder.Trim();
          

            //SQL Bit類型對應ADO.NET的 bool類型           


            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);


            return ds;

        }

        /// <summary>
        /// 得到一批数据,根據工單和查詢條件   用於條碼重列印功能
        /// </summary>
        public DataSet GetConditionList(string workorder,string selectItem)
        {
            StringBuilder strSql = new StringBuilder();
            DataSet ds = new DataSet();
            switch (selectItem)
            {
                case "1" :
                    strSql.Append("select WorkOrder,DeptLineCode,Segment,RepositoryID,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
                    strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo,Attribute7,NDescription,");
                    strSql.Append("CartonNO from OuterBarCode where workorder=@WorkOrder AND (Flag <> 1 OR FLAG IS NULL) order by CurrentBoxTime asc");

                    SqlParameter[] parameters = {
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900)
                                        };
                    parameters[0].Value = workorder.Trim();

                    //SQL Bit類型對應ADO.NET的 bool類型           
                    ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
                    break;
         
                case "2" :
                    strSql.Append("select WorkOrder,DeptLineCode,Segment,RepositoryID,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
                    strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo,Attribute7,NDescription,");
                    strSql.Append("CartonNO from OuterBarCode where workorder=@WorkOrder AND (Flag <> 1 OR FLAG IS NULL) AND BarCode IN (select BarCode FROM InStore where InStore.WorkOrder = @WorkOrder)   order by CurrentBoxTime asc");

                    SqlParameter[] parameters1 = { new SqlParameter("@WorkOrder", SqlDbType.VarChar,900)};

                    parameters1[0].Value = workorder.Trim();

                    //SQL Bit類型對應ADO.NET的 bool類型           
                    ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters1);
                    break;
                case "3" :
                    strSql.Append("select WorkOrder,DeptLineCode,Segment,RepositoryID,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
                    strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo,Attribute7,NDescription,");
                    strSql.Append("CartonNO from OuterBarCode where workorder=@WorkOrder AND (Flag <> 1 OR FLAG IS NULL) AND BarCode NOT IN (select BarCode FROM InStore where InStore.WorkOrder = @WorkOrder)   order by CurrentBoxTime asc");

                    SqlParameter[] parameters2 = { new SqlParameter("@WorkOrder", SqlDbType.VarChar, 900) };

                    parameters2[0].Value = workorder.Trim();

                    //SQL Bit類型對應ADO.NET的 bool類型           
                    ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters2);
                    break;

            }
          
            return ds;

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.OuterBarCode DataRowToModel(DataRow row)
        {
            Model.OuterBarCode model = new Model.OuterBarCode();

            if (row != null)
            {
                model.WorkOrder = row["WorkOrder"].ToString().Trim();

                model.DeptLineCode = row["DeptLineCode"].ToString().Trim();               

                model.Segment = row["Segment"].ToString().Trim();

                model.CurrentBoxAmount=long.Parse(row["CurrentBoxAmount"].ToString().Trim());

                model.IPQC=row["ipqc"].ToString().Trim();

                model.CurrentBoxTime=DateTime.Parse(row["currentboxtime"].ToString().Trim());

                model.BarCode=row["barcode"].ToString().Trim();

                model.WorkNo=row["workno"].ToString().Trim();

                model.RepositoryID = row["repositoryid"].ToString().Trim();

                model.Attribute7 = row["attribute7"].ToString().Trim();        //當DB中是null值時,查詢出來之后,這里的值將是DBNull,DBNull是可以.ToString()方法的. 如: DBNull.Value即可知道.

                model.NDescription = row["ndescription"].ToString().Trim();

                model.CartonNO =
                    row["cartonno"] == DBNull.Value ? 0 : Convert.ToInt32(row["cartonno"].ToString().Trim());

                model.Flag = row["flag"].ToString().Trim();


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

            }

            return model;
        }

        /// <summary>
        /// 得到一個對象實體
        /// </summary>
        public Model.OuterBarCode GetModel(string barcode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select workorder,deptlinecode,segment,currentboxamount,ipqc,currentboxtime,");
            strSql.Append("barcode,workno,repositoryid,attribute7,ndescription,cartonno,flag from outerbarcode where barcode=@BarCode");
           
            
            
            SqlParameter[] parameters = {  
                                            new SqlParameter("@BarCode", SqlDbType.VarChar,2000)	
                                        };


            parameters[0].Value = barcode.Trim();
                       
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);         //只將DataTable中的第1行記錄當一個對象實體返回.其它行不返回,因為這里是得到一個對象實體.
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public bool Update(Model.OuterBarCode model)
        {

           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OuterBarCode set RepositoryID=@RepositoryID where ");           
            strSql.Append(" workorder=@WorkOrder and segment=@Segment and currentboxtime=@CurrentBoxTime ");
            strSql.Append(" and barcode=@BarCode and workno=@WorkNo");

            SqlParameter[] parameters = {					
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900),
					                        new SqlParameter("@Segment", SqlDbType.VarChar,2000),	
                                            new SqlParameter("@CurrentBoxTime", SqlDbType.DateTime),	
                                            new SqlParameter("@BarCode", SqlDbType.VarChar,2000),					
                                            new SqlParameter("@WorkNo", SqlDbType.VarChar,2000),
                    					    new SqlParameter("@RepositoryID", SqlDbType.VarChar,800)                                           
                                        };

            parameters[0].Value = model.WorkOrder.Trim();
            parameters[1].Value = model.Segment.Trim();
            parameters[2].Value = model.CurrentBoxTime;
            parameters[3].Value = model.BarCode.Trim();
            parameters[4].Value = model.WorkNo.Trim();
            parameters[5].Value = model.RepositoryID.Trim();
           





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
        /// 得到一批数据,根據條碼
        ///排除單據作廢的單據
        /// </summary>
        public DataSet GetListByBC(string barcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WorkOrder,DeptLineCode,Segment,RepositoryID,CurrentBoxAmount,IPQC,");                             //用@變量來代替加(+)變量就做到了SQL注入防範,用加(+)變量的方式不安全或導致SQL語句發生錯誤.即在變量的位置不要用加號(+).
            strSql.Append("convert(varchar(100),CurrentBoxTime,120) 'CurrentBoxTime',BarCode,WorkNo,Attribute7,NDescription,");
            strSql.Append("CartonNO from OuterBarCode where barcode=@BarCode AND (Flag <> 1 OR FLAG IS NULL) order by CurrentBoxTime asc");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@BarCode", SqlDbType.VarChar,2000)                                                                                       
                                        };

            parameters[0].Value = barcode.Trim();




            //SQL Bit類型對應ADO.NET的 bool類型           


            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);


            return ds;

        }

        ///<summary>
        ///查詢一個工單的最大箱號
        ///</summary>
        public int GetMaxCartonNO(string workorder)
        {
            int maxCartonNO=-1;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(max(cartonno),0) cartonno from outerbarcode where workorder=@WorkOrder");

            SqlParameter[] parameters = {                                            
                                            new SqlParameter("@WorkOrder", SqlDbType.VarChar,900)                                                                                       
                                        };

            parameters[0].Value = workorder.Trim();

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            
            while (sdr.Read())
            {
                //MessageBox.Show(sdr[0].ToString().Trim());
                //為 DGV2 每個單元格賦值
                //this.dataGridView2.Rows.Add();    //DGV 增加1行
                //this.dataGridView2[0, dgv_row].Value = sdr[0].ToString().Trim();         //項次
                //this.dataGridView2[1, dgv_row].Value = sdr[1].ToString().Trim();         //名稱

                maxCartonNO = Convert.ToInt32(sdr["cartonno"].ToString().Trim());          //找到的最大箱號
            }

            sdr.Close();
            
            if (sdr == null)            //去掉對象實例
            { 
            }
            else
            {
                if (!sdr.IsClosed)                           //如果sdr沒有被關閉,則關閉
                {
                    sdr.Close();
                }
                sdr.Dispose();
                sdr = null;
            }

            return maxCartonNO;
        }
    }
}
