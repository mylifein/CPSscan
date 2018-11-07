using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class DepartmentID
    {
        private readonly DAL.DepartmentID dal = new DAL.DepartmentID();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string department_id)
        {
            return dal.GetList(department_id);
        }
    }
}
