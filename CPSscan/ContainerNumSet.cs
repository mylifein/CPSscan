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
        public ContainerNumSet()
        {
            InitializeComponent();
        }

        public ContainerNumSet(string loginedid)
        {
            this.loginedid = loginedid;
            InitializeComponent();
        }

        private void ContainerNumSet_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = getUserDepartment(loginedid);
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
            string num = textBox3.Text;
            bll.save(dept, model, num);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
