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
    public partial class ResetPwd : Form
    {
        public ResetPwd()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }

        private void ResetPwdForm_Shown(object sender, EventArgs e)
        {
            this.ClearAll();

            string loginedid = FormsVar.login.loginedid.Trim();       //得到已登錄的ID

            this.textBox1.Text = loginedid;

            if (loginedid != "")
            {            
                BLL.User bll = new BLL.User();
                Model.User model = bll.GetModel(loginedid);

                this.textBox2.Text = model.UserName.Trim();
                this.textBox5.Text = (model.DeptName == null ? "" : model.DeptName.Trim());

                bll = null;
                model = null;
            }

            this.textBox3.Text = "";
            this.textBox4.Text = "";

            this.textBox3.Focus();
        }

        private void ClearAll()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginid = this.textBox1.Text.Trim();
            string pwd = this.textBox3.Text.Trim();
            string newpwd = this.textBox4.Text.Trim();

            if (loginid=="")
            {
                MessageBox.Show("登錄帳號不能為空", "提示", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }
            
            if (pwd == "")
            {
                MessageBox.Show("原密碼不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            
            if (newpwd == "")
            {
                MessageBox.Show("新密碼不能為空", "提示", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;
            }


            //更新密碼
            BLL.User bll = new BLL.User();

            bool ret_val = bll.Update(loginid, pwd, newpwd);

            if (ret_val)
            {
                MessageBox.Show("更新成功", "提示", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("更新失敗,可能是原密碼不正確", "提示", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
            }

            bll = null;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button1_Click(new object(), new EventArgs());
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button1_Click(new object(), new EventArgs());
            }
        }
    }
}
