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
    public partial class ContainerNumSet : Form
    {
        string loginedid;
        string model;
        string department;
        public string num;
        public ContainerNumSet()
        {
            InitializeComponent();
        }

        public ContainerNumSet(string model, string department)
        {
            this.model = model;
            this.department = department;
            InitializeComponent();
        }

        public ContainerNumSet(string loginedid)
        {
            this.loginedid = loginedid;
            InitializeComponent();
        }

        private void ContainerNumSet_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loginedid))
            {
                this.textBox1.Text = getUserDepartment(loginedid);
            }
            else if (!string.IsNullOrEmpty(model))
            {
                this.textBox1.Text = department;
                this.textBox2.Text = model;
            }
        }

        private string getUserDepartment(string loginedid)
        {
            BLL.ContainerNumSet bll = new BLL.ContainerNumSet();
            return bll.getUserDepartment(loginedid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLL.ContainerNumSet bll = new BLL.ContainerNumSet();
            string dept = textBox1.Text;
            string model = textBox2.Text;
            this.num = textBox3.Text;
            bll.save(dept, model, num);

            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
