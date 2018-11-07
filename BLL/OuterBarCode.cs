using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class OuterBarCode
    {
        private readonly DAL.OuterBarCode dal = new DAL.OuterBarCode();
        

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.OuterBarCode model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 得到一批数据,根據工單和工號
        /// </summary>
        /*
        public DataSet GetList(string workorder, string workno)
        {
            return dal.GetList(workorder, workno);
        }
        */

        /// <summary>
        /// 得到一批数据,根據工單
        /// </summary>
        public DataSet GetList(string workorder)
        {
            return dal.GetList(workorder);
        }

        /// <summary>
        /// 得到一個對象實體
        /// </summary>
        public Model.OuterBarCode GetModel(string barcode)
        {
            return dal.GetModel(barcode);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public bool Update(Model.OuterBarCode model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 得到一批数据,根據條碼
        /// </summary>
        public DataSet GetListByBC(string barcode)
        {
            return dal.GetListByBC(barcode);
        }

        ///<summary>
        ///查詢一個工單的最大箱號
        ///</summary>
        public int GetMaxCartonNO(string workorder)
        {
            return dal.GetMaxCartonNO(workorder);
        }
    }
}
