using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class User
    {
        private readonly DAL.User dal = new DAL.User();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string loginid,string password)
        {
            return dal.Exists(loginid,password);
        }

        /// <summary>
        /// 得一個對象實體
        /// </summary>
        public Model.User GetModel(string loginid)
        {
            return dal.GetModel(loginid);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(string loginid,string pwd,string newpwd)
        {
            return dal.Update(loginid,pwd,newpwd);
        }
    }
}
