using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace LinqToSqlDAL
{
    public class LinqDataContext:DataContext
    {
        public LinqDataContext(string connStr)
            : base(connStr)
        {           
        }

        /// <summary>
        /// 返回 MenuPermission 表集合
        /// </summary>
        public Table<Model.MenuPermission> Menus                            //這里可以用方法來代替此屬性返回
        { 
            get
            {
                return GetTable<Model.MenuPermission>();
            }
        }
    }
}
