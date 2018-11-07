using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CPSscan.Common;

namespace CPSscan
{
    public partial class PauseReason : Form
    {
        public PauseReason()
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
            if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("請輸入暫停原因", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }


            //將原因保存到DB
            Model.TimeSegment model = new Model.TimeSegment();
            BLL.TimeSegment bll = new BLL.TimeSegment();

            model.WorkOrder = FormsVar.bcscan.textBox2.Text.Trim();    //工單
            model.DepartmentID = int.Parse(FormsVar.bcscan.textBox9.Text.Trim());   //課別ID
            model.LineID =
                Convert.ToInt32((FormsVar.bcscan.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());     //線別ID
            model.WorkNo = FormsVar.login.loginedid.Trim();            //工號
            model.Reason = this.textBox1.Text.Trim();                  //原因

            bool ret_v = bll.Update(model);

            bll = null;
            model = null;


            if (!ret_v)
            {
                //更新暫停時間失敗的處理
                //不處理,放在開始按鈕中處理
            }


            //停用Timer1
            FormsVar.bcscan.timer1.Stop();


            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void PauseReason_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "";            
        }

        private void PauseReason_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }
    }
}
