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
    public partial class Login : Form
    {
        public string loginedid;      //用於保存登錄進來的人員的ID

        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {                    

            string loginid = this.textBox1.Text.Trim();
            string password = this.textBox2.Text.Trim();

            if ((loginid == "") || (password == ""))
            {
                MessageBox.Show("用戶名或密碼不能為空", "提示", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }


            BLL.User bll = new BLL.User();

            bool ret_val = bll.Exists(loginid, password);

            if (ret_val)
            {
                loginedid = loginid.ToUpper();          //登錄工號變成大寫;

                if ((FormsVar.mainfm == null) || (FormsVar.mainfm.IsDisposed))
                {
                    FormsVar.mainfm = new MainFm(loginedid);
                }


                //登錄成功后將登錄ID和本機IP寫入到DB中
                if (!this.SaveLoginInfo(loginedid))
                {
                    MessageBox.Show("登錄信息保存失敗,請重新登錄", "提示", MessageBoxButtons.OK, 
                        MessageBoxIcon.Exclamation);
                    this.Close();
                }

                FormsVar.mainfm.Show();
                FormsVar.login.Visible = false;
            }
            else
            {                 
                MessageBox.Show("用戶名或密碼錯誤", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginedid = "";
                this.textBox2.Text = "";
                this.textBox2.Focus();                
                return;
            }                       
           
        }

        private bool SaveLoginInfo(string loginid)
        {
            //將登錄ID和本機IP寫入到DB中

            string localip = LocalIP.GetLocalIP().Trim();

            if (localip == "")
            {
                return false;
            }


            Model.LoginInfo model=new Model.LoginInfo();
            BLL.LoginInfo bll = new BLL.LoginInfo();

            model.LoginID=loginedid.Trim();     //工號
            model.IP=localip.Trim();            //IP

            bll.Delete(model.LoginID);          //先刪除
            bool ret_v = bll.Add(model);        //后插入

            bll = null;
            model = null;

            return ret_v;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button2_Click(new object(), new EventArgs());
            }
        }
    }
}
