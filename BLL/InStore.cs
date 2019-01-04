using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class InStore
    {
        private readonly DAL.InStore dal = new DAL.InStore();

        /// <summary>
        /// 調用一個SP,得到Oracle DB中的所有倉庫
        /// </summary>
        public DataSet GetRepository()
        {
            return dal.GetRepository();
        }

        public string GetRepositoryName(string repositoryID)
        {
            return dal.GetRepositoryName(repositoryID);
        }

        /// <summary>
        /// 調用一個SP,根據條碼進行入庫
        /// </summary>
        public DataSet PutInStore(Model.InStore model,out int mark)
        {
            return dal.PutInStore(model,out mark);
        }

        /// <summary>
        /// 根據工單查詢數據
        /// </summary>
        public DataSet GetList(string workorder)
        {
            return dal.GetList(workorder);
        }

        /// <summary>
        /// 通過工單,計算入庫總和
        /// </summary>
        public string GetStoredTotal(string workorder)
        {
            return dal.GetStoredTotal(workorder);
        }

         /// <summary>
        /// 根據工單和入庫時間任意組合查詢數據
        /// </summary>
        public DataSet GetListByWhere(string whereStr)
        {
            return dal.GetListByWhere(whereStr);
        }
    }
}
