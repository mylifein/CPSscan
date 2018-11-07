using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ContainerNumSet
    {
        private readonly DAL.ContainerNumSet dal = new DAL.ContainerNumSet();

        public string getUserDepartment(string loginedid)
        {
            return dal.getUserDepartment(loginedid);
        }

        public void save(string dept, string model, string num)
        {
            dal.save(dept, model, num);
        }
    }
}
