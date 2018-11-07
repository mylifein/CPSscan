using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BLL
{
    public class LoginInfo
    {
        private readonly DAL.LoginInfo dal = new DAL.LoginInfo();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.LoginInfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 刪除一条数据
        /// </summary>
        public bool Delete(string loginid)
        {
            return dal.Delete(loginid);
        }

        /// <summary>
        /// 根據loginid查詢當前的IP
        /// </summary>
        public string GetIP(string loginid)
        {
            string ip = "";

            SqlDataReader dr = dal.GetIP(loginid);

            while (dr.Read())
            {
                ip = dr["ip"].ToString().Trim();  
            }

            dr.Close();



            if (dr == null)            //去掉對象實例
            {
            }
            else
            {
                if (!dr.IsClosed)
                {
                    dr.Close();
                }
                dr.Dispose();
                dr = null;
            }


            return ip;
        }
    }
}
