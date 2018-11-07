using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MenuPermission
    {
        private readonly LinqToSqlDAL.MenuPermission dal = new LinqToSqlDAL.MenuPermission();

        /// <summary>
        ///得到要禁用的菜單項
        /// </summary>
        public List<string> GetDisableMenuItems(string workno)
        {
            return dal.GetDisableMenuItems(workno);
        }
                
        /// <summary>
        ///得到要禁用的菜單項,讓LinqToSql執行SQL語句
        /// </summary>
        public List<string> GetDisableMenuItemsBySql(string workno)
        {
            return dal.GetDisableMenuItemsBySql(workno);
        }
    }
}
