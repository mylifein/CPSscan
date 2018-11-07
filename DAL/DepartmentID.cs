using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using System.Data;

namespace DAL
{
    public class DepartmentID
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string department_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select department_id,department_code,line_id,line_code ");
            strSql.Append(" from DepartmentID ");

            if ((department_id != null) && (department_id.Trim() != ""))
            {
                strSql.Append(" where department_id=" + department_id.Trim());
            }

            strSql.Append(" order by department_id asc,line_id asc");

            return SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), null);

        }

    }
}
