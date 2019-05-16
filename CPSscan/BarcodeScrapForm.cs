using DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CPSscan
{
    public partial class BarcodeScrapForm : Form
    {
        int dgv_ClickedRowIndex = -1;         //用於保存用戶單擊DGV單元格時,此單元格所在的行索引,行列索引都是從0開始的
        public int dgv_DoubleClickedRowIndex = -1;   //用於保存用戶雙擊DGV單元格時,此單元格所在的行索引,行列索引都是從0開始的
        public BarcodeScrapForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public string WorkOrder         //給本窗體添加一個(工單)屬性
        {
            get;
            set;
        }

        private void BarcodeScrapForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;    //設定 DGV 控件的剪貼板復制模式 ClipboardCopyMode
            this.textBox1.Text = "";

            this.dataGridView1.DataSource = null;

            dgv_ClickedRowIndex = -1;         //初值
            dgv_DoubleClickedRowIndex = -1;   //初值


            this.label2.Enabled = false;
            this.textBox2.Enabled = false;
            this.radioButton1.Checked = true;
        }

        private void BarcodeScrapForm_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();

            //檢查屬性是否已被賦值
            if ((WorkOrder != null) && (WorkOrder.Trim() != ""))
            {
                //如果已被賦值,則查詢此工單
                this.textBox1.Text = WorkOrder.Trim();
                this.QueryBCInfo();
            }
        }

        private void QueryBCInfo()          //查詢已打印的歷史貼紙信息
        {

            dgv_ClickedRowIndex = -1;         //每次查詢之前將此變量賦初值-1
            dgv_DoubleClickedRowIndex = -1;   //每次查詢之前將此變量賦初值-1


            string workorder = string.Empty, barcode = string.Empty;


            if (this.radioButton1.Checked)    //按工單查詢
            {

                workorder = this.textBox1.Text.Trim();    //工單號

                if ((workorder == null) || (workorder == ""))
                {
                    MessageBox.Show("請輸入工單", "信息", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

            }

            if (this.radioButton2.Checked)   //按條碼查詢
            {
                barcode = this.textBox2.Text.Trim();    //條碼

                if ((barcode == null) || (barcode == ""))
                {
                    MessageBox.Show("請輸入條碼", "信息", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
            }


            //查詢已打印的歷史貼紙信息
            BLL.OuterBarCode bll = new BLL.OuterBarCode();
            DataSet ds = null;



            if (this.radioButton1.Checked)
            {
                ds = bll.GetList(workorder);
            }


            if (this.radioButton2.Checked)
            {
                ds = bll.GetListByBC(barcode);
            }



            if ((ds != null) && (ds.Tables.Count > 0))
            {
                //ds.Tables[0].Columns.Remove("rid");
                //ds.Tables[0].Columns.Remove("reelqty");      //移除2列

                this.dataGridView1.DataSource = ds.Tables[0];

                //DGV 改變列名,列寬
                this.dataGridView1.Columns[0].HeaderText = "工單";
                this.dataGridView1.Columns[0].Width = 145;
                this.dataGridView1.Columns[1].HeaderText = "課別與線別";
                this.dataGridView1.Columns[1].Width = 172;
                this.dataGridView1.Columns[2].HeaderText = "料號";
                this.dataGridView1.Columns[2].Width = 172;
                this.dataGridView1.Columns[3].HeaderText = "倉別";
                this.dataGridView1.Columns[3].Width = 105;
                this.dataGridView1.Columns[4].HeaderText = "數量";
                this.dataGridView1.Columns[4].Width = 105;
                this.dataGridView1.Columns[5].HeaderText = "IPQC";
                this.dataGridView1.Columns[5].Width = 112;
                this.dataGridView1.Columns[6].HeaderText = "日期時間";
                this.dataGridView1.Columns[6].Width = 158;
                this.dataGridView1.Columns[7].HeaderText = "條碼";
                this.dataGridView1.Columns[7].Width = 195;
                this.dataGridView1.Columns[8].HeaderText = "掃描人員";
                this.dataGridView1.Columns[8].Width = 112;
                this.dataGridView1.Columns[9].HeaderText = "模號";
                this.dataGridView1.Columns[9].Width = 172;
                this.dataGridView1.Columns[10].HeaderText = "品名";
                this.dataGridView1.Columns[10].Width = 180;
                this.dataGridView1.Columns[11].HeaderText = "箱號";
                this.dataGridView1.Columns[11].Width = 105;
            }
            else
            {
                this.dataGridView1.DataSource = null;
            }

            bll = null;
        }

        private void BarcodeScrapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            WorkOrder = null;        //窗體關閉后,將工單屬性賦值為null
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.QueryBCInfo();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.QueryBCInfo();
            this.dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //注意: 是單元格單擊時發生此事件.
            //MessageBox.Show(e.RowIndex.ToString());
            //MessageBox.Show(e.ColumnIndex.ToString());

            //注意: DGV 的內容區的行列索引都是從0開始的,標題索引都是-1

            /*  在第DGV的第2列上單擊時
            if ((e.ColumnIndex == 1) && (e.RowIndex > -1))
            {
                palletid = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
            }
            else
            {
                palletid = "";
            }
            */


            //DGV的行列索引都是從0開始的
            if ((e.ColumnIndex > -1) && (e.RowIndex > -1))       //如果用戶單擊是的單元格的時候,即行列索引都是大於-1的時候
            {
                //palletid = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                dgv_ClickedRowIndex = e.RowIndex;      //保存被單擊的單元格所在的行索引
            }
            else
            {
                dgv_ClickedRowIndex = -1;        //賦初值-1
            }

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.label1.Enabled = true;
                this.textBox1.Enabled = true;
                this.label2.Enabled = false;
                this.textBox2.Enabled = false;
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.label2.Enabled = true;
                this.textBox2.Enabled = true;
                this.label1.Enabled = false;
                this.textBox1.Enabled = false;
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.QueryBCInfo();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dgv_ClickedRowIndex == -1)
            {
                MessageBox.Show("沒有得到行號,請在下面單擊要作廢的行", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //get barcode and scrap barcode
            string barcode = this.dataGridView1["BarCode", dgv_ClickedRowIndex].Value == null ? "" :
                this.dataGridView1["BarCode", dgv_ClickedRowIndex].Value.ToString().Trim();
            Boolean updateResult = this.UpdateBarcodeStatus(barcode);
            if (updateResult)
            {
                MessageBox.Show("作廢條碼編號:"+barcode + "成功", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }else
            {
                MessageBox.Show("作廢條碼編號:" + barcode + "失敗", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //將條碼的狀態改為1為作廢
        public bool UpdateBarcodeStatus(string barcode)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OuterBarCode set Flag=@Flag where ");
            strSql.Append(" barcode=@BarCode");


            SqlParameter[] parameters = {
                                            new SqlParameter("@BarCode", SqlDbType.VarChar,2000),
                                            new SqlParameter("@Flag", SqlDbType.VarChar,2000),
                                        };
            parameters[0].Value = barcode.Trim();
            parameters[1].Value = '1';

            //object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            int rows = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionString, CommandType.Text, strSql.ToString(), parameters);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
