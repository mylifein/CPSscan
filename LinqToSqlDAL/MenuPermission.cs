using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data;

namespace LinqToSqlDAL
{
    public class MenuPermission
    {
        /// <summary>
        ///得到要禁用的菜單項
        /// </summary>
        public List<string> GetDisableMenuItems(string workno)
        {
            using (LinqDataContext ldc = new LinqDataContext(SQLHelper.ConnectionString))
            {
                return ldc.Menus.Where(x => x.WorkNo == workno.Trim()).Select(x => x.DisableMenuItem).ToList();       //立即執行
            }
        }

        /// <summary>
        ///得到要禁用的菜單項,讓LinqToSql執行SQL語句
        /// </summary>        
        public List<string> GetDisableMenuItemsBySql(string workno)
        {
            using (LinqDataContext ldc = new LinqDataContext(SQLHelper.ConnectionString))
            {
                string sql = "select DisableMenuItem from dbo.MenuPermission where WorkNo={0}";


                //SqlParameter[] parameters = {
                //                                new SqlParameter{ParameterName="WorkNo", Value=workno.Trim(),SqlDbType= System.Data.SqlDbType.VarChar}
                //                            };

                //SqlParameter parameter = new SqlParameter { ParameterName = "WorkNo", Value = workno.Trim(), SqlDbType = System.Data.SqlDbType.VarChar };
                //注意: 在LinqToSql和EF中,此處,為SQL語句傳遞參數時,不能用SqlParameter,報錯,而是直接用{0},{1},{2}來傳遞參數,其值就是后面的實際值,按順序對位置.


                IEnumerable<Model.MenuPermission> disableMenus = ldc.ExecuteQuery<Model.MenuPermission>(sql, workno.Trim());

                return disableMenus.Select(x => x.DisableMenuItem).ToList();             //立即執行
            }
        }


        //ldc.ExecuteQuery()  是執行查詢
        //ldc.ExecuteCommand() 是執行增,刪,改.   執行存儲過程是這兩個方法其中的一個.
        //以上是執行查詢可用,如量要對表進行增,刪,改操作,則必須要在DB中定義此表的主鍵.
        //以上非常重要.


        //string formatString = string.Format("select DisableMenuItem from dbo.MenuPermission where WorkNo={0}", workno.Trim());   //string.Format()方法的使用,當然這种方法不能用來作為傳遞SQL語句給ldc.ExecuteQuery<Model.MenuPermission>()方法,因為沒有做到SQL防注入,從而導致執行時,DB報SQL語句語法錯誤.
    }
}
