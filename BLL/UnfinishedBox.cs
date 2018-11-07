using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BLL
{
    public class UnfinishedBox
    {
        private readonly DAL.UnfinishedBox dal = new DAL.UnfinishedBox();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.UnfinishedBox model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 根據工單,課別ID,線別ID,工號查詢當前的項次號
        /// </summary>
        public string GetItemNo(Model.UnfinishedBox model)
        {

            string itemNo = "";

            SqlDataReader dr = dal.GetItemNo(model);

            while (dr.Read())
            {
                //MessageBox.Show(sdr[0].ToString().Trim());
                //comboBox3.Items.Add(sdr[0].ToString().Trim());


                //如果 sdr 中有 null 或空串 "",則 continue     ,      sdr[3] 可以為空串""
                /*
                if ((sdr[0] == DBNull.Value) || (sdr[1] == DBNull.Value) || (sdr[2] == DBNull.Value) || (sdr[3] == DBNull.Value) ||
                    (sdr[0].ToString().Trim() == "") || (sdr[1].ToString().Trim() == "") || (sdr[2].ToString().Trim() == ""))
                {
                    continue;   //任何一個為空時將直接讀取下一條記錄
                }

                this.dataGridView2.Rows.Add();
                this.dataGridView2[0, dgv_x2].Value = sdr[0].ToString().Trim();       //項次
                this.dataGridView2[1, dgv_x2].Value = sdr[1].ToString().Trim();       //名稱
                this.dataGridView2[2, dgv_x2].Value = "";                             //條碼為空串
                this.dataGridView2[3, dgv_x2].Value = sdr[2].ToString().Trim();       //條碼長度
                this.dataGridView2[4, dgv_x2].Value = sdr[3].ToString().Trim();       //條碼含有字符
                this.dataGridView2[5, dgv_x2].Value = "0";                            //能否重復掃描
                dgv_x2++;
                */

                itemNo = dr["itemno"].ToString().Trim();              //即使為DBNull也可以用ToString()方法.
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


            return itemNo;
        }

        /// <summary>
        /// 刪除一条数据
        /// </summary>
        public bool Delete(Model.UnfinishedBox model)
        {
            return dal.Delete(model);            
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Model.UnfinishedBox model)
        {
            return dal.Exists(model);
        }
    }
}
