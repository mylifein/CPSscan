using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class BAControl
    {
        private readonly DAL.BAControl dal = new DAL.BAControl();



        public DataSet getControlDetail()
        {
            return dal.getControlDetail();
        }


        public void saveBAControl(string startTime, string endTime, string user, string status)
        {
            dal.saveBAControl(startTime, endTime, user, status);
        }

        public bool checkBAControl()
        {
            return dal.checkBAControl();
        }
    }
}
