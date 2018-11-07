using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class WorkOrderBC
    {
        private readonly DAL.WorkOrderBC dal = new DAL.WorkOrderBC();


        /// <summary>
        /// 得到一個DataSet
        /// </summary>
        public DataSet GetWorkOrderInfo(string workorder)
        {
            return dal.GetWorkOrderInfo(workorder);
        }

        ///<summary>
        ///增加一條記錄
        ///</summary>
        public bool Add(Model.WorkOrderBC model)
        {
            return dal.Add(model);
        }

        public string getContainerNum(string model, string department)
        {
            return dal.getContainerNum(model, department);
        }
    }
}
