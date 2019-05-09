using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BCScanned
    {
        private readonly DAL.BCScanned dal = new DAL.BCScanned();
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.BCScanned model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 通過工單,課別ID,線別ID,工號得到最后一條記錄的時間CurrentTime
        /// </summary>
        public string GetLastTime(Model.BCScanned model)
        {
            return dal.GetLastTime(model);
        }

        /// <summary>
        /// 通過工單,課別ID,料號得到與最後一條記錄的時間差
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long getScanTimeDiff(Model.BCScanned model)
        {
            return dal.getScanTimeDiff(model);
        }

        /// <summary>
        /// 通過工單,課別ID,線別ID,工號得到最后一條記錄的每箱數量
        /// </summary>
        public string GetLastAmountPerBox(Model.BCScanned model)
        {
            return dal.GetLastAmountPerBox(model);
        }

        /// <summary>
        /// 通過工單查詢已掃描了多少
        /// </summary>        
        public int FinishedAmount(string workorder)
        {
            return dal.FinishedAmount(workorder);
        }
        

        /// <summary>
        /// 通過工單和工號查詢已掃描了多少
        /// </summary>
        /*
        public long FinishedAmount(string workorder,string workno)
        {
            return dal.FinishedAmount(workorder,workno);
        }
        */

       
    }
}
