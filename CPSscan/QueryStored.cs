using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPSscan
{
    public partial class QueryStored : Form
    {
        private int mark = 0;


        public QueryStored()
        {
            InitializeComponent();
        }

        private void QueryStored_Load(object sender, EventArgs e)
        {
            this.dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;    //設定 DGV 控件的剪貼板復制模式 ClipboardCopyMode
            this.textBox1.Text = "";
            this.label3.Text = "";
            this.label5.Text = "";

            this.dataGridView1.DataSource = null;

            this.EnableTimeControls(false);
        }

        private void EnableTimeControls(bool mark)
        {
            //是否啟用入庫時間控件
            if (mark)
            {
                this.label6.Enabled = true;
                this.label7.Enabled = true;
                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker2.Enabled = true;
            }
            else
            {
                this.label6.Enabled = false;
                this.label7.Enabled = false;
                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;
            }
        }

        private void QueryStored_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();

            this.button1.Text = "開";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.QueryBCInfo();
        }

        private void QueryBCInfo()          //查詢已入庫信息
        {         
            
            string workOrder = this.textBox1.Text.Trim().Replace("'","").Replace("\"","");        //工單號,替換單引號和雙引號, good

            bool storedTimeCheck = this.checkBox1.Checked;   //checkBox控件的值

            if (((workOrder == null) || (workOrder == "")) && (!storedTimeCheck))
            {
                MessageBox.Show("請輸入工單或選擇入庫時間", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }



            //拼接條件字符串
            string whereStr = " 1=1 ";

            if (workOrder != "")
            {
                whereStr += " and workorder='"+workOrder+"' ";
            }

            if (storedTimeCheck)
            {
                //入庫時間
                string beginTime = this.dateTimePicker1.Value.ToString("yyyy-MM-dd");            //("yyyy-MM-dd HH:mm:ss")
                string endTime = this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");     //ToString("yyyy-MM-dd 23:59:59")

                whereStr += " and (storedtime between '" + beginTime + "' and '" + endTime + "') ";
            }




            //查詢已入庫信息
            BLL.InStore bll = new BLL.InStore();

            //DataSet ds = bll.GetList(workOrder);

            DataSet ds = bll.GetListByWhere(whereStr);

            if ((ds != null) && (ds.Tables.Count > 0))
            {
                //ds.Tables[0].Columns.Remove("rid");
                //ds.Tables[0].Columns.Remove("reelqty");      //移除2列

                this.dataGridView1.DataSource = ds.Tables[0];

                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].HeaderText = "工單";
                this.dataGridView1.Columns[0].Width = 145;
                this.dataGridView1.Columns[1].HeaderText = "料號";
                this.dataGridView1.Columns[1].Width = 172;
                this.dataGridView1.Columns[2].HeaderText = "課別與線別";
                this.dataGridView1.Columns[2].Width = 172;
                this.dataGridView1.Columns[3].HeaderText = "待入庫數量";
                this.dataGridView1.Columns[3].Width = 105;
                this.dataGridView1.Columns[4].HeaderText = "倉庫";
                this.dataGridView1.Columns[4].Width = 200;
                this.dataGridView1.Columns[5].HeaderText = "條碼 ";
                this.dataGridView1.Columns[5].Width = 170;
                this.dataGridView1.Columns[6].HeaderText = "待入庫時間";
                this.dataGridView1.Columns[6].Width = 155;
                this.dataGridView1.Columns[7].HeaderText = "操作人員";
                this.dataGridView1.Columns[7].Width = 112;
                this.dataGridView1.Columns[8].HeaderText = "模號";
                this.dataGridView1.Columns[8].Width = 172;
                this.dataGridView1.Columns[9].HeaderText = "品名";
                this.dataGridView1.Columns[9].Width = 180;
                this.dataGridView1.Columns[10].HeaderText = "箱號";
                this.dataGridView1.Columns[10].Width = 105;
            }
            else
            {
                this.dataGridView1.DataSource = null;
            }



            //得到已入庫總量
            if ((workOrder != null) && (workOrder != ""))
            {
                this.label5.Text = bll.GetStoredTotal(workOrder);
            }
            else
            {
                this.label5.Text = "";
            }



            bll = null;

                       

            //得到工單總量
            if ((workOrder != null) && (workOrder != ""))
            {
                this.QueryWorkOrderTotal(workOrder);
            }
            else
            {
                this.label3.Text = "";
            }
        }

        private void QueryWorkOrderTotal(string workOrder)
        {

            this.label3.Text = "";     //初值


            //查詢工單總數量
            BLL.WorkOrderBC bll = new BLL.WorkOrderBC();
            DataSet myds = bll.GetWorkOrderInfo(workOrder);

            if ((myds != null) && (myds.Tables.Count == 2) && (myds.Tables[0].Rows.Count > 0))
            {
                this.label3.Text =
                       (myds.Tables[0].Rows[0]["START_QUANTITY"] == DBNull.Value ? "" :
                       myds.Tables[0].Rows[0]["START_QUANTITY"].ToString().Trim());            //總數量  

            }

            bll = null;

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.QueryBCInfo();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.QueryBCInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mark == 0)
            {
                mark++;
                this.button1.Text = "關";
            }
            else if (mark == 1)
            {
                mark--;
                this.button1.Text = "開";
            }


            if (mark == 1)
            {
                this.timer1.Start();
            }
            else
            {
                this.timer1.Stop();
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.EnableTimeControls(true);
            }
            else
            {
                this.EnableTimeControls(false);
            }
        }

    }
}
