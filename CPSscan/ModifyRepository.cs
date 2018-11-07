using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using CPSscan.Common;


namespace CPSscan
{
    public partial class ModifyRepository : Form
    {
        public ModifyRepository()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string workOrder = this.label12.Text.Trim();   //工單
            string segment = this.label13.Text.Trim();     //料號
            string currentboxtime = this.label11.Text.Trim();          //日期時間
            string barCode = this.label16.Text.Trim();     //條碼
            string workNo = this.label18.Text.Trim();      //掃描人員

            string repositoryID = (this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim();   //倉別ID
            


            //修改倉別
            BLL.OuterBarCode bll = new BLL.OuterBarCode();
            Model.OuterBarCode model = new Model.OuterBarCode();

            model.WorkOrder = workOrder;
            model.Segment = segment;
            model.CurrentBoxTime = DateTime.Parse(currentboxtime);
            model.BarCode = barCode;
            model.WorkNo = workNo;
            model.RepositoryID = repositoryID;


            bool ret_v = bll.Update(model);


            if (ret_v)
            {
                MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改失敗", "提示", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
            }


            bll = null;
            model = null;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ModifyRepository_Load(object sender, EventArgs e)
        {
            this.ClearAll();

            this.BindRepository();    //加載倉別
            this.FillContent();       //填充內容
        }

        private void FillContent()
        {
            int DGV_rowIndex = FormsVar.repeatprtouterbc.dgv_DoubleClickedRowIndex;   //得到行索引

            DataGridView dgv=FormsVar.repeatprtouterbc.dataGridView1;  //得到DGV對象

            this.label12.Text = dgv["workorder", DGV_rowIndex].Value.ToString().Trim();      //工單
            this.label10.Text = dgv["deptlinecode", DGV_rowIndex].Value.ToString().Trim();   //課別與線別
            this.label13.Text = dgv["segment", DGV_rowIndex].Value.ToString().Trim();        //料號
            this.label15.Text = dgv.Rows[DGV_rowIndex].Cells["currentboxamount"].Value.ToString().Trim();   //數量
            this.label17.Text = dgv.Rows[DGV_rowIndex].Cells["ipqc"].Value.ToString().Trim();   //IPQC
            this.label11.Text = dgv.Rows[DGV_rowIndex].Cells["currentboxtime"].Value.ToString().Trim();   //日期時間
            this.label16.Text = dgv.Rows[DGV_rowIndex].Cells["barcode"].Value.ToString().Trim();   //條碼
            this.label18.Text = dgv.Rows[DGV_rowIndex].Cells["workno"].Value.ToString().Trim();   //掃描人員
            this.label19.Text = dgv.Rows[DGV_rowIndex].Cells["attribute7"].Value.ToString().Trim();   //模號

            string RepositoryID = dgv["repositoryid", DGV_rowIndex].Value.ToString().Trim();      //倉別ID
                        
            
            //設置倉別
            //(this.comboBox1.SelectedItem as ComboboxItem).Value = RepositoryID.Trim();     //報錯,不能這樣寫,只能取Value的值
            //var items = this.comboBox1.Items.Cast<ComboboxItem>();

            for (int i = 0; i < this.comboBox1.Items.Count; i++)      //循環每個項目
            {
                this.comboBox1.SelectedIndex = i;      //設置選定的索引
                if ((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim() == RepositoryID.Trim())      //判斷選定的項目的值
                {
                    break;      //值相等時,跳出循環
                }
            }
        }

        private void BindRepository()
        {            
            //加載Oracle DB中的所有倉庫

            BLL.InStore bll = new BLL.InStore();

            DataSet ds = bll.GetRepository();

            this.comboBox1.Items.Clear();                //清空comboBox1控件中的所有項,否則會累加項目，越來越多.
            this.comboBox1.Text = "";


            string repositoryid, repositoryname;

            if ((ds != null) && (ds.Tables.Count > 0))
            {
                //循環DS的行,將數據加入到comboBox
                for (int ds_row = 0; ds_row < ds.Tables[0].Rows.Count; ds_row++)
                {
                    //this.comboBox1.Items.Add(ds.Tables[0].Rows[ds_row]["line_code"].ToString().Trim());         //當為DBNull的時候,也是可以ToString()的.

                    repositoryid = ds.Tables[0].Rows[ds_row]["SECONDARY_INVENTORY_NAME"].ToString().Trim();                   //倉庫ID
                    repositoryname =
                        repositoryid + "   " + ds.Tables[0].Rows[ds_row]["DESCRIPTION"].ToString().Trim();      //倉庫名稱
                    this.comboBox1.Items.Add(new ComboboxItem(repositoryname, repositoryid));
                }
            }


            bll = null;

        }

        private void ClearAll()
        {
            this.label12.Text = "";
            this.label10.Text = "";
            this.label13.Text = "";
            this.label15.Text = "";
            this.label17.Text = "";
            this.label11.Text = "";
            this.label16.Text = "";
            this.label18.Text = "";
            this.label19.Text = "";

            this.comboBox1.Items.Clear();
            this.comboBox1.Text = "";
        }
    }
}
