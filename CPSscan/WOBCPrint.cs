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
using Common;

namespace CPSscan
{
    public partial class WOBCPrint : Form
    {
        public WOBCPrint()
        {
            InitializeComponent();
        }

        private void WOBCPrint_Load(object sender, EventArgs e)
        {
            
        }

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    string workorder = this.textBox4.Text.Trim();

        //    if ((workorder == null) || (workorder == ""))
        //    {
        //        MessageBox.Show("請輸入要查詢的工單", "提示", MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //        this.textBox4.Focus();
        //        return;
        //    }


        //    //查詢工單,可能是一批工單

        //}

        private void textBox4_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "輸入一個工單,注意要區分英文字母大小寫";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";

            //通過工單將相關的信息帶出來
            string workorder = this.textBox4.Text.Trim();

            if ((workorder == null) || (workorder == ""))
            {
                this.textBox4.Text = "";
                this.ClearAll();
            }
            else
            {
                BLL.WorkOrderBC bll = new BLL.WorkOrderBC();
                DataSet myds = bll.GetWorkOrderInfo(workorder);

                if ((myds != null) && (myds.Tables.Count == 2) && (myds.Tables[0].Rows.Count > 0))
                {
                    this.textBox1.Text =
                        (myds.Tables[0].Rows[0]["WIP_ENTITY_NAME"] == DBNull.Value ? "" : 
                        myds.Tables[0].Rows[0]["WIP_ENTITY_NAME"].ToString().Trim());           //工單

                    this.textBox2.Text =
                        (myds.Tables[0].Rows[0]["SEGMENT1"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["SEGMENT1"].ToString().Trim());                  //料號

                    this.textBox3.Text =
                        (myds.Tables[0].Rows[0]["START_QUANTITY"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["START_QUANTITY"].ToString().Trim());            //數量

                    this.textBox8.Text =
                        (myds.Tables[0].Rows[0]["DEPARTMENT_ID"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["DEPARTMENT_ID"].ToString().Trim());             //部門ID
                    
                    this.textBox5.Text =
                        (myds.Tables[0].Rows[0]["DEPARTMENT_CODE"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString().Trim());           //部門

                    this.textBox6.Text =
                        (myds.Tables[0].Rows[0]["CLASS_CODE"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["CLASS_CODE"].ToString().Trim());                //工單類型

                    this.textBox7.Text =
                        (myds.Tables[0].Rows[0]["DESCRIPTION"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["DESCRIPTION"].ToString().Trim());               //建單者

                    this.textBox9.Text =
                        (myds.Tables[0].Rows[0]["COMPLETION_SUBINVENTORY"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["COMPLETION_SUBINVENTORY"].ToString().Trim());          //倉庫

                    this.textBox10.Text =
                      (myds.Tables[0].Rows[0]["ATTRIBUTE7"] == DBNull.Value ? "" :
                      myds.Tables[0].Rows[0]["ATTRIBUTE7"].ToString().Trim());                  //模號

                    this.textBox11.Text =
                      (myds.Tables[0].Rows[0]["NDESCRIPTION"] == DBNull.Value ? "" :
                      Auxiliary.GetStringByBytes(myds.Tables[0].Rows[0]["NDESCRIPTION"].ToString().Trim(),43));                //品名,取43字節長度的字符串
                }
                else
                {
                    //沒有查詢到的時候
                    this.ClearAll();
                    MessageBox.Show("沒有查到工單或其它原因,請確認工單是否正確,英文字母需要區分大小寫", "提示",                   
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox4.Text = "";
                    this.textBox4.Focus();
                }

                bll = null;
            }
        } 

        private void ClearAll()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string workorder = this.textBox1.Text.Trim();            //工單
            string segment = this.textBox2.Text.Trim();              //料號
            string startquantity = this.textBox3.Text.Trim();        //數量
            string departmentid = this.textBox8.Text.Trim();         //部門ID
            string departmentcode=this.textBox5.Text.Trim();         //部門
            string classcode=this.textBox6.Text.Trim();              //工單類型
            string ddescription=this.textBox7.Text.Trim();           //建單者
            string completionsubinventory = this.textBox9.Text.Trim();    //倉庫
            string attribute7 = this.textBox10.Text.Trim();          //模號
            string ndescription = this.textBox11.Text.Trim();        //品名
            



            if ((workorder == null) || (segment == null) || (startquantity == null) || (departmentid==null) ||
                (workorder == "") || (segment == "") || (startquantity == "") || (departmentid==""))
            {
                MessageBox.Show("不能打印工單條碼,因為工單 料號 數量 部門ID都不允許為空", "提示", 
                    MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                this.textBox4.Focus();
                return;
            }




            string[] BC_Array = new string[27];               //實例化為27個元素
            for (int c = 0; c < 27; c++)
            {
                BC_Array[c] = "";                             //為數組賦值為空串""
            }


            //將參數值放入數組中
            BC_Array[0] = workorder;             //工單,只列印工單的條碼
            //BC_Array[1] = segment;
            //BC_Array[2] = startquantity;


            //得到模板文件
            //string templateFileName = @".\LangChao2.qdf";                //條碼打印模板直接放在當前目錄下即可
            string templateFileName = System.IO.Directory.GetCurrentDirectory() + "\\WorkOrderBC.Lab";            //“System.IO.Directory.GetCurrentDirectory”:获取当前应用程序的路径，最后不包含“\”；

            //判斷文件存在否
            if (!File.Exists(templateFileName))
            {
                MessageBox.Show("打印模板文件不存在,無法打印工單條碼", "信息", MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);                
                return;
            }


            BCPrint bcprint = BCPrint.getInstance();
            bool ret_v=bcprint.PrintBC(templateFileName, BC_Array);

            if (ret_v)
            {
                //打印成功后,將本次打印的信息保存到DB,以備查詢之用
                Model.WorkOrderBC model = new Model.WorkOrderBC();
                BLL.WorkOrderBC bll = new BLL.WorkOrderBC();

                model.WorkOrder = workorder;
                model.Segment = segment;
                model.StartQuantity =long.Parse(startquantity);
                model.DepartmentID = Convert.ToInt32(departmentid);
                model.DepartmentCode=departmentcode;                
                model.ClassCode=classcode;                    
                model.DDescription = ddescription;
                model.CompletionSubinventory = completionsubinventory;
                model.Attribute7 = attribute7;
                model.NDescription = ndescription;
                model.WorkNo = FormsVar.login.loginedid;


                if (!bll.Add(model))
                {                    
                    MessageBox.Show("保存打印信息時失敗,請聯繫管理員", "信息", MessageBoxButtons.OK,                   
                        MessageBoxIcon.Exclamation); 
                }
                
                bll = null;
                model = null;

            }

            bcprint = null;
           
        }

        private void WOBCPrint_Shown(object sender, EventArgs e)
        {
            this.textBox4.Focus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單號內容";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示料號內容";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單的數量";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示課別ID";
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示課別的名稱";
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單的類型";
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示建單人的信息";
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單上預設的倉庫";
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示當前工單上的模號";
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示當前工單上的品名";
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }
    }
}
