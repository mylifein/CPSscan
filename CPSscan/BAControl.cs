using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using CPSscan.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace CPSscan
{
    public partial class BAControl : Form
    {
        public BAControl()
        {
            InitializeComponent();
        }

        

        private void BAControl_Load(object sender, EventArgs e)
        {
            getBAControl();
        }

        private void getBAControl()
        {
            DataSet ds = new BLL.BAControl().getControlDetail();
            if (ds.Tables[0].Rows.Count == 1)
            {
                string startTime = ds.Tables[0].Rows[0][0].ToString();
                string endTime = ds.Tables[0].Rows[0][1].ToString();
                string status = ds.Tables[0].Rows[0][2].ToString();
                DateTime startDateTime = Convert.ToDateTime(startTime);
                DateTime endDateTime = Convert.ToDateTime(endTime);
                if (DateTime.Compare(endDateTime, DateTime.Now) >= 0)
                {
                    if (status.Equals("1"))
                    {
                        checkBox1.Checked = true;
                        label2.Text = "已卡控";
                    }
                    else
                    {
                        checkBox1.Checked = false;
                        label2.Text = "未卡控";
                    }

                    dateTimePicker1.Value = startDateTime;
                    dateTimePicker2.Value = endDateTime;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "";
            string startTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
            string endTime = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");
            string user = FormsVar.login.loginedid;
            if (checkBox1.Checked)
            {
                status = "1";
                saveBAControl(startTime, endTime, user, status);
            }
            //

        }

        private void saveBAControl(string startTime, string endTime, string user, string status)
        {
            new BLL.BAControl().saveBAControl(startTime, endTime, user, status);
            getBAControl();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (!validDate())
            {
                MessageBox.Show("結束日期需大於開始日期", "錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool validDate()
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            if (DateTime.Compare(dt1, dt2) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!validDate())
            {
                MessageBox.Show("結束日期需大於開始日期", "錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

       

       
    }
}
