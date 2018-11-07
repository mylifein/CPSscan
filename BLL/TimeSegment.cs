using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TimeSegment
    {

        private readonly DAL.TimeSegment dal = new DAL.TimeSegment();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.TimeSegment model)
        {
            return dal.Add(model);
        }

        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.TimeSegment model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 更新自動時間
        /// </summary>
        public bool UpdateAutoTime(Model.TimeSegment model)
        {
            return dal.UpdateAutoTime(model);
        }

        /// <summary>
        /// 更新異常記錄的暫停時間和自動時間一樣
        /// </summary>
        public bool UpdateAbnormal(Model.TimeSegment model)
        {
            return dal.UpdateAbnormal(model);
        }

        /// <summary>
        /// 更新結束時間
        /// </summary>
        public bool UpdateEndTime(Model.TimeSegment model)
        {
            return dal.UpdateEndTime(model);
        }
    }
}
