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
    public partial class InStore : Form
    {
        private DateTime _dt, tempdt;           //用於判斷兩次按鍵間的間隔時間

        public InStore()
        {
            InitializeComponent();
        }

        private void InStore_Load(object sender, EventArgs e)
        {
            this.ClearContent();

            //加載所有倉庫
            //this.GetRepository();
        }

        private void ClearContent()
        {
            //清空標簽內容
            this.label5.Text = "";
            this.label9.Text = "";
            this.label11.Text = "";
            this.label7.Text = "";
            this.label21.Text = "";
            this.label13.Text = "";
            this.label15.Text = "";
            this.label19.Text = "";
            this.label17.Text = "";
            this.label23.Text = "";
            this.label24.Text = "";
            this.label26.Text = "";
            this.label28.Text = "";
            this.label30.Text = "";
        }

        private void GetRepository()
        {
            //加載Oracle DB中的所有倉庫

            BLL.InStore bll = new BLL.InStore();

            DataSet ds = bll.GetRepository();

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
                }
            }

            
            bll = null;

        }

        private string GetRepositoryName(string repositoryID)
        {
            //加載Oracle DB中的所有倉庫

            BLL.InStore bll = new BLL.InStore();

            string repositoryname = bll.GetRepositoryName(repositoryID);

            return repositoryname;

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "選擇倉庫";
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "進行條碼掃描";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private bool GetBarCodeInfo(string barcode)
        {
            //得到條碼信息
            BLL.OuterBarCode bll = new BLL.OuterBarCode();

            Model.OuterBarCode model = bll.GetModel(barcode.Trim());

            bll = null;


            if (model == null)
            {
                //提示未查到該條碼的信息
                this.ClearContent();                
                MessageBox.Show("沒有得到條碼信息,條碼可能無效", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
            else
            {
                //給條碼信息欄位賦值
                this.label23.Text = model.BarCode.Trim();                       //條碼內容
                this.label5.Text = model.WorkOrder.Trim();                      //工單
                this.label9.Text = model.Segment.Trim();                        //料號
                this.label11.Text = model.CurrentBoxAmount.ToString().Trim();   //當前箱數量
                this.label7.Text = model.DeptLineCode.Trim();                   //課別與線別
                this.label13.Text = model.IPQC.Trim();                          //IPQC
                this.label15.Text = model.CurrentBoxTime.ToString("yyyy-MM-dd HH:mm:ss").Trim();   //當前箱上的日期時間
                this.label19.Text = model.WorkNo.Trim();                        //工單掃描人員
                this.label30.Text = model.RepositoryID.Trim();                  //倉庫
                return true;
            }
        }

        private bool GetWOTotalAmount()
        {
            //得到工單總數量
            if (this.label5.Text.Trim() == "")
            {
                MessageBox.Show("沒有得到工單,不能入庫", "信息", MessageBoxButtons.OK,                   
                    MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                long totalAmount = this.GetTotalAmount(this.label5.Text.Trim());

                if (totalAmount == -1)
                {
                    this.label24.Text = "";
                    MessageBox.Show("沒有得到工單總數量,不能入庫", "信息", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);                    
                    return false;
                }
                else
                {
                    this.label24.Text = totalAmount.ToString().Trim();
                    return true;
                }                
            }  
        }

        private long GetTotalAmount(string workorder)
        {
            //通過工單得到總數量,沒有得到時返回數值-1
            BLL.WorkOrderBC bll = new BLL.WorkOrderBC();
            DataSet myds = bll.GetWorkOrderInfo(workorder.Trim());   //得到工單總數量

            long totalAmount=
            (myds.Tables[0].Rows[0]["START_QUANTITY"] == DBNull.Value ? -1 :
                    long.Parse(myds.Tables[0].Rows[0]["START_QUANTITY"].ToString().Trim()));            //總數量 
            
            bll = null;          
  
                
            return totalAmount;        
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //檢查條碼是否是用掃描槍輸入
            if (!this.CheckInput())
            {
                return;
            }


            if (e.KeyValue == 13)    //如果輸入的是回車鍵就執行相關的動作
            {
                string barcode = "";

                if (this.textBox1.Text.Trim() == "")
                {
                    return;          //返回,后面代碼不執行
                }
                else
                {
                    barcode = this.textBox1.Text.Trim();         //得到當前輸入條碼
                }


                /*
                //判斷必要的信息是否合法
                if (!this.CheckInfo())
                {
                    this.textBox1.Text = "";
                    return;
                }
                */


                //判斷工單是否已完工
                /*
                if (!this.JudgeWOComplete())
                {
                    return;
                }
                */


                //得到條碼信息
                if (!this.GetBarCodeInfo(barcode))
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    return;
                }


                //得到工單總數量
                if (!this.GetWOTotalAmount())
                {
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                    return;
                }


                //進行入庫
                this.StoreAction(barcode);


                this.textBox1.Text = "";
            }
        }

        /*
        private bool JudgeWOComplete()
        {
            //判斷工單是否完工            

            //得到當前工單已完工量            
            long finishedAmount = this.GetFinishedAmount(this.textBox2.Text.Trim());

            //當前工單總數量            
            long totalAmount = long.Parse(this.textBox5.Text.Trim());


            if (finishedAmount >= totalAmount)       //已完工量大於總數量,則不能掃描       
            {
                MessageBox.Show("不能掃描,工單已完工", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;     //已完工,返回false
            }
            else
            {
                return true;      //未完工,返回true
            }
        }
        */

        private void StoreAction(string barcode)
        {
            //先判斷倉別是否一致
            //得到用戶選擇的倉別
            //string selectRepositoryid = (this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim(); 

            //得到條碼貼紙上的倉別
            string bcRepositoryid = this.label30.Text.Trim();

            /*
            if (!selectRepositoryid.Equals(bcRepositoryid))
            {
                //倉別不一致,不允許入庫
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "";

                MessageBox.Show("倉別不一致,不能入庫", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            */



            //進行入庫動作
            BLL.InStore bll = new BLL.InStore();
            Model.InStore model = new Model.InStore();

            model.BarCode = barcode.Trim();
            model.NowScannedWorkNo = FormsVar.login.loginedid.Trim();
            model.RepositoryID = bcRepositoryid;
            model.RepositoryName = this.GetRepositoryName(bcRepositoryid);


            int mark;     //此變量用於判斷入庫是否成功,因為成功后要取結果集,不成功后不取結果集

            DataSet ds = bll.PutInStore(model,out mark);


            if (mark == 1)
            {
                //入庫成功
                //得到需要的值
                if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                {
                    string repositoryid = ds.Tables[0].Rows[0]["repositoryid"].ToString().Trim();
                    string currentboxamount = ds.Tables[0].Rows[0]["currentboxamount"].ToString().Trim();
                    string storedtime = ds.Tables[0].Rows[0]["storedtime"].ToString().Trim();

                    this.label21.Text = repositoryid;
                    this.label28.Text = currentboxamount;
                    this.label17.Text = storedtime;
                    this.label26.Text = "成功";
                    this.label26.ForeColor = Color.Green;
                }
                else
                {
                    this.label21.Text = "";
                    this.label28.Text = "";
                    this.label17.Text = "";
                    this.label26.Text = "";
                    //this.label26.ForeColor = Color.Red;                                        

                    MessageBox.Show("沒有得到入庫的相關信息", "信息", MessageBoxButtons.OK,                   
                        MessageBoxIcon.Exclamation);
                }
            }
            else if (mark == -1)
            {
                //入庫失敗,不能入庫,條碼為空
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,條碼不能為空", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else if (mark == -2)
            {
                //入庫失敗,不能入庫,條碼無效,條碼不在列印貼紙表中
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,條碼不在列印貼紙表中", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else if (mark == -3)
            {
                //入庫失敗,不能入庫,條碼已入過庫,不能重複入庫
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,條碼已入過庫,不能重複入庫", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }            
            else if (mark == -4)
            {
                //入庫失敗,不能入庫,因為工單已經全部入完庫
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,因為工單已經全部入完庫", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else if (mark == -5)
            {
                //入庫失敗,能入庫,因為已入庫總數加上當前要入庫的數量后大於工單總數
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,因為已入庫總數加上當前要入庫的數量后大於工單總數", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }            
            else if (mark == -6)
            {
                //入庫失敗,不能入庫,因為系統參數不正確
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,因為系統參數不正確", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }            
            else if (mark == -7)
            {
                //入庫失敗,工單總數量為0或沒有查詢到
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,工單總數量為0或沒有查詢到", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }            
            else if (mark == -30)
            {
                //入庫失敗
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,發生了未知的錯誤(-30)", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                //入庫失敗
                this.label21.Text = "";
                this.label28.Text = "";
                this.label17.Text = "";
                this.label26.Text = "失敗";
                this.label26.ForeColor = Color.Red;

                MessageBox.Show("入庫失敗,發生了未知的錯誤", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);

            }
                            
               


            bll = null;
            model = null;

        }

        /*
        public bool CheckInfo()
        {
            #region 檢查必要的信息是否合法
            //檢查必要的信息是否合法

            if (this.comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("請選擇倉庫", "信息", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return false;
            }
            else
            {               
                return true;
            }
            #endregion
        }
         */ 

        public bool CheckInput()                                          //判斷兩次按鍵間的時間間隔,從而判斷是掃描槍輸入還是鍵盤輸入
        {
            #region 判斷是否是掃描槍的輸入
            //MessageBox.Show(textBox1.Text.Trim().Length.ToString().Trim());       //文本框為空時,返回值為0,不包括當前按鍵所輸入的字符
            //判斷兩次按鍵間的時間間隔,從而判斷是掃描槍輸入還是鍵盤輸入
            if (textBox1.Text.Trim().Length == 0)
            {
                _dt = DateTime.Now;
            }
            else
            {
                tempdt = DateTime.Now;




                TimeSpan ts = tempdt.Subtract(_dt);     //兩個時間相減
                if (ts.Milliseconds > 50)
                {

                    MessageBox.Show("無效條碼,請用掃描槍輸入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = "";
                    return false;   //返回,后面代碼不執行
                }
                else
                {
                    _dt = tempdt;
                }

            }


            return true;
            #endregion
        }

    }
}
