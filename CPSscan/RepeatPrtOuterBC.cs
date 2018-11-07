using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CPSscan.Common;

namespace CPSscan
{
    public partial class RepeatPrtOuterBC : Form
    {
        int dgv_ClickedRowIndex = -1;         //用於保存用戶單擊DGV單元格時,此單元格所在的行索引,行列索引都是從0開始的
        public int dgv_DoubleClickedRowIndex = -1;   //用於保存用戶雙擊DGV單元格時,此單元格所在的行索引,行列索引都是從0開始的


        public RepeatPrtOuterBC()
        {
            InitializeComponent();
        }

        public string WorkOrder         //給本窗體添加一個(工單)屬性
        {            
            get;
            set;
        }

        private void RepeatPrtOuterBC_Load(object sender, EventArgs e)
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

        private void RepeatPrtOuterBC_Shown(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.QueryBCInfo();
            this.dataGridView1.ClearSelection();
        }

        private void QueryBCInfo()          //查詢已打印的歷史貼紙信息
        {

            dgv_ClickedRowIndex = -1;         //每次查詢之前將此變量賦初值-1
            dgv_DoubleClickedRowIndex = -1;   //每次查詢之前將此變量賦初值-1


            string workorder=string.Empty,barcode=string.Empty;


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
            DataSet ds=null;



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

        private void RepeatPrtOuterBC_FormClosed(object sender, FormClosedEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.PrintOuterBC();
        }

        private void PrintOuterBC()          //打印外箱貼紙
        {

            //MessageBox.Show(dgv_ClickedRowIndex.ToString().Trim());

            if (dgv_ClickedRowIndex == -1)
            {
                MessageBox.Show("沒有得到行號,請在下面單擊要打印的行", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

                        


            //打印外箱貼紙            

            string[] BC_Array = new string[27];               //實例化為27個元素
            for (int c = 0; c < 27; c++)
            {
                BC_Array[c] = "";                             //為數組賦值為空串""
            }


            //將參數值放入數組中
            BC_Array[0] = 
                this.dataGridView1.Rows[dgv_ClickedRowIndex].Cells["DeptLineCode"].Value==null?"":
                this.dataGridView1.Rows[dgv_ClickedRowIndex].Cells["DeptLineCode"].Value.ToString().Trim();       //課別與線別;
            
            BC_Array[1] = this.dataGridView1["WorkOrder",dgv_ClickedRowIndex].Value==null?"":
                this.dataGridView1["WorkOrder", dgv_ClickedRowIndex].Value.ToString().Trim();                     //工單
            
            BC_Array[2] = this.dataGridView1["Segment",dgv_ClickedRowIndex].Value==null?"":
                this.dataGridView1["Segment", dgv_ClickedRowIndex].Value.ToString().Trim();                       //料號

            BC_Array[3] = this.dataGridView1["CurrentBoxAmount",dgv_ClickedRowIndex].Value==null?"":
                this.dataGridView1["CurrentBoxAmount", dgv_ClickedRowIndex].Value.ToString().Trim();              //當前箱個數
            
            BC_Array[4] = this.dataGridView1["IPQC",dgv_ClickedRowIndex].Value==null?"":
                this.dataGridView1["IPQC", dgv_ClickedRowIndex].Value.ToString().Trim();                          //IPQC

            BC_Array[5] = this.dataGridView1["CurrentBoxTime",dgv_ClickedRowIndex].Value==null?"":
                this.dataGridView1["CurrentBoxTime", dgv_ClickedRowIndex].Value.ToString().Trim();                //日期時間


            //要打印的條碼    (工單+日期時間+當前箱個數)
            //直接取DGV中相應的單元格中的內容即可
            BC_Array[6] = this.dataGridView1["BarCode", dgv_ClickedRowIndex].Value == null ? "" :
                this.dataGridView1["BarCode", dgv_ClickedRowIndex].Value.ToString().Trim();


            BC_Array[7] = this.dataGridView1["RepositoryID", dgv_ClickedRowIndex].Value == null ? "" :
                this.dataGridView1["RepositoryID", dgv_ClickedRowIndex].Value.ToString().Trim();                  //倉庫ID

            BC_Array[8] = this.dataGridView1["Attribute7", dgv_ClickedRowIndex].Value == null ? "" :
                this.dataGridView1["Attribute7", dgv_ClickedRowIndex].Value.ToString().Trim();                    //模號

            BC_Array[9] = this.dataGridView1["NDescription", dgv_ClickedRowIndex].Value == null ? "" :
                this.dataGridView1["NDescription", dgv_ClickedRowIndex].Value.ToString().Trim();                  //品名

            BC_Array[10] = this.dataGridView1["CartonNO", dgv_ClickedRowIndex].Value == null ? "" :
                            this.dataGridView1["CartonNO", dgv_ClickedRowIndex].Value.ToString().Trim();          //箱號



            
            //得到模板文件
            //string templateFileName = @".\LangChao2.qdf";                //條碼打印模板直接放在當前目錄下即可
            string templateFileName = System.IO.Directory.GetCurrentDirectory() + "\\OuterBarCode.Lab";            //“System.IO.Directory.GetCurrentDirectory”:获取当前应用程序的路径，最后不包含“\”；

            //判斷文件存在否
            if (!File.Exists(templateFileName))
            {
                MessageBox.Show("打印模板文件不存在,無法打印外箱貼紙", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }


            BCPrint bcprint = BCPrint.getInstance();
            bool ret_v = bcprint.PrintBC(templateFileName, BC_Array);

            bcprint = null;

            if (!ret_v)           
            {
                MessageBox.Show("打印外箱貼紙失敗,請檢查各項數據是否正確", "信息", MessageBoxButtons.OK,                       
                    MessageBoxIcon.Exclamation);
            }            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            MessageBox.Show(e.RowIndex.ToString(), "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            MessageBox.Show(e.ColumnIndex.ToString(), "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            */


            //DGV的行列索引都是從0開始的
            if ((e.RowIndex > -1) && (e.ColumnIndex > -1))       //如果用戶雙擊是的單元格的時候,即行列索引都是大於-1的時候
            {
                //行列索引都大於-1時,表示雙擊是單元格   
                
                //palletid = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                    
                dgv_DoubleClickedRowIndex = e.RowIndex;      //保存被雙擊的單元格所在的行索引


                if (dgv_DoubleClickedRowIndex > -1) 
                {
                    //if (this.dataGridView1.Rows[dgv_DoubleClickedRowIndex].Cells["workno"].Value.ToString().Trim() ==
                    //    FormsVar.login.loginedid.Trim())        //這一句是用來控制不是自己掃描的記錄,自己不能修改
                    //{


                        if ((FormsVar.modifyrepository == null) || (FormsVar.modifyrepository.IsDisposed))
                        {
                            FormsVar.modifyrepository = new ModifyRepository();
                        }

                        FormsVar.modifyrepository.ShowDialog();


                        //判斷是按確定還是按取消按鈕關閉子窗體的
                        if (FormsVar.modifyrepository.DialogResult == DialogResult.OK)
                        {
                            this.QueryBCInfo();
                        }

                    //}
                    //else
                    //{
                    //      //提示不是你掃描的記錄,你不能修改
                    //}

                }
            }
            else
            {
                dgv_DoubleClickedRowIndex = -1;        //賦初值-1
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
                        
            
            //this.button2.PerformClick();        //執行button2按鈕的單擊事件     //這種正規
            //this.button2_Click(new object(), new EventArgs());   //不是很正規
            //this.button2_Click(null, null);                      //不是很正規
            //對於按鈕的單擊事件,共有3種寫法,注意.
        }
    }
}
