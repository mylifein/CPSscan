using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using System.IO;
using CPSscan.Common;

namespace CPSscan
{
    public partial class BCScan : Form
    {
        private DateTime _dt, tempdt;           //用於判斷兩次按鍵間的間隔時間

        public BCScan()
        {
            InitializeComponent();
        }

        private void BCScan_Shown(object sender, EventArgs e)
        {
            this.textBox2.Focus();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "輸入一個工單以便程式帶出相關信息,注意區分英文字母大小寫";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";

            //通過工單將相關的信息帶出來
            string workorder = this.textBox2.Text.Trim();

            if ((workorder == null) || (workorder == ""))
            {
                this.ClearAll();
            }
            else
            {
                BLL.WorkOrderBC bll = new BLL.WorkOrderBC();
                DataSet myds = bll.GetWorkOrderInfo(workorder);

                if ((myds != null) && (myds.Tables.Count == 2) && (myds.Tables[0].Rows.Count > 0))
                {
                    string status = (myds.Tables[0].Rows[0]["STATUS_TYPE"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["STATUS_TYPE"].ToString().Trim());
                    string statusDescription = (myds.Tables[0].Rows[0]["STATUS_TYPE_DISP"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["STATUS_TYPE_DISP"].ToString().Trim());
                    //判断工单状态
                    if (!status.Equals("3"))
                    {
                        string message = "工單狀態為  '" + statusDescription + "'  請聯係現場物料員或生管處理";
                        MessageBox.Show(message, "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.textBox2.Text = "";
                        this.textBox2.Focus();
                        return;
                    }
                    string model = (myds.Tables[0].Rows[0]["SEGMENT1"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["SEGMENT1"].ToString().Trim());

                    string department = (myds.Tables[0].Rows[0]["DEPARTMENT_CODE"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["DEPARTMENT_CODE"].ToString().Trim());

                    this.textBox4.Text = model;                  //料號

                    this.textBox9.Text =
                        (myds.Tables[0].Rows[0]["DEPARTMENT_ID"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["DEPARTMENT_ID"].ToString().Trim());             //課別ID

                    this.textBox3.Text = department;           //課別名稱                

                    this.textBox5.Text =
                        (myds.Tables[0].Rows[0]["START_QUANTITY"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["START_QUANTITY"].ToString().Trim());            //總數量  


                    //得到預設倉庫
                    string repositoryID =
                        (myds.Tables[0].Rows[0]["COMPLETION_SUBINVENTORY"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["COMPLETION_SUBINVENTORY"].ToString().Trim());            //預設倉庫

                    this.textBox10.Text =
                        (myds.Tables[0].Rows[0]["ATTRIBUTE7"] == DBNull.Value ? "" :
                        myds.Tables[0].Rows[0]["ATTRIBUTE7"].ToString().Trim());                //模號  

                    this.textBox11.Text =
                        (myds.Tables[0].Rows[0]["NDESCRIPTION"] == DBNull.Value ? "" :
                        Auxiliary.GetStringByBytes(myds.Tables[0].Rows[0]["NDESCRIPTION"].ToString().Trim(), 43));              //品名,取43字節長度的字符串                     

                    this.textBox7.Text =
                        (myds.Tables[1].Rows[0]["currentTime"] == DBNull.Value ? "" :
                        myds.Tables[1].Rows[0]["currentTime"].ToString().Trim());               //第2個DataTable中的服務器上的時間


                    this.textBox1.Text = bll.getContainerNum(model, department);
                    //如果沒有抓到容器數量，則立即設置數量，并輸出到textBox1
                    if (this.textBox1.Text == "0")
                    {
                        if (MessageBox.Show("該料號未設置容器數量，請先設置數量", "信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            ContainerNumSet containerNumSet = new ContainerNumSet(model, department);
                            DialogResult dialogResult = containerNumSet.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                this.textBox1.Text = containerNumSet.num;
                            }
                        }
                    }

                    //帶出已掃描數量和剩餘量
                    //this.ShowInformation();   //現在不用帶出這個信息了


                    //查詢DB,檢查是否有最后一次掃描記錄,有則把最后一次的每箱數量帶出來,無則顯示為空串
                    this.SetLastAmountPerBox();


                    //帶出項次號
                    this.GetItemNo();


                    //設置預設倉庫                                                        
                    this.SetDefaultRepository(repositoryID);


                }
                else
                {
                    //沒有查詢到工單或其它的原因的時候
                    this.ClearAll();
                    MessageBox.Show("沒有查到工單或其它原因,請確認工單是否正確,英文字母需要區分大小寫", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox2.Text = "";
                    this.textBox2.Focus();
                }

                bll = null;
            }
        }

        private void SetDefaultRepository(string repositoryID)
        {
            //設置預設倉庫

            if (repositoryID.Trim() != "")
            {
                //設置倉別
                for (int i = 0; i < this.comboBox2.Items.Count; i++)      //循環每個項目
                {
                    this.comboBox2.SelectedIndex = i;      //設置選定的索引
                    if ((this.comboBox2.SelectedItem as ComboboxItem).Value.ToString().Trim() == repositoryID.Trim())      //判斷選定的項目的值
                    {
                        break;      //值相等時,跳出循環
                    }
                }
            }
        }

        #region 現在不用帶出這個信息了
        /*
        private void ShowInformation()           //帶出已掃描數量和剩餘量       
        {            
            if (this.textBox5.Text.Trim() != "")       //如果數量不為空時
            {
                long finishedAmount = this.GetFinishedAmount(this.textBox2.Text.Trim());          //已掃描數量
                this.label10.Text = finishedAmount.ToString().Trim();
                this.label11.Text =
                    (long.Parse(this.textBox5.Text.Trim()) - finishedAmount).ToString().Trim();     //剩餘量
            }
            else
            {
                this.label10.Text = "";
                this.label11.Text = "";
                this.label16.Text = "";
            }            
        }
        */
        #endregion

        private void ClearAll()
        {
            this.textBox1.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            this.textBox6.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";

            this.label10.Text = "";
            this.label11.Text = "";
            this.label16.Text = "";
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單上的課別名稱";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單上的料號";
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單上的數量";
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
            FormsVar.mainfm.statusStrip1.Items[0].Text = "輸入IPQC的工號";
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示當前的日期時間";
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "輸入每箱個數";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }



        private void textBox8_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "進行條碼掃描";
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private bool CheckLoginInfo()
        {
            //檢查登錄信息是否是自己的IP,如果不是自己的IP則表示后面有人用此帳號登錄了系統,那么本機掃描終止.
            BLL.LoginInfo bll = new BLL.LoginInfo();

            string ret_IP = bll.GetIP(FormsVar.login.loginedid.Trim());    //得到當前登錄帳號在DB中的IP
            string localip = LocalIP.GetLocalIP().Trim();    //得到本機IPv4地址

            bll = null;

            if (ret_IP == localip)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isContinuousScan(Model.BCScanned model)
        {
            BLL.BCScanned bll = new BLL.BCScanned();
            long timediff = bll.getScanTimeDiff(model);
            if (timediff < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {

            //檢查條碼是否是用掃描槍輸入
            if (!this.CheckInput())
            {
                return;
            }


            if (e.KeyValue == 13)    //如果輸入的是回車鍵就執行相關的動作
            {

                //先檢查登錄信息是否是自己的IP,如果不是自己的IP則表示后面有人用此帳號登錄了系統,那么本機掃描終止.
                if (!this.CheckLoginInfo())
                {
                    MessageBox.Show("本帳號已在其它電腦上登錄了,本機無法掃描,掃描窗口即將關閉\r\n\r\n如果要" +
                        "繼續掃描,請重新登錄本系統", "錯誤",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.textBox8.Text = "";
                    this.Close();
                }



                string barcode = "";

                if (this.textBox8.Text.Trim() == "")
                {
                    return;          //返回,后面代碼不執行
                }
                else
                {
                    barcode = this.textBox8.Text.Trim();         //得到當前輸入條碼
                }
                if(this.textBox8.Text != this.textBox2.Text)
                {
                    MessageBox.Show("條碼掃描與工單號不一致,請確認掃描的工單是否正確！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //判斷必要的信息是否合法
                if (!this.CheckInfo())
                {
                    this.textBox8.Text = "";               
                    this.textBox8.Focus();
                    return;
                }

                



                //判斷工單是否已掃描完工
                if (!this.JudgeWOComplete())
                {
                    this.textBox8.Text = "";
                    this.textBox8.Focus();
                    return;
                }

                //得到批量掃描信息
                bool IsBatchScan = this.checkBox1.Checked;

                //得到信息
                Model.BCScanned model = new Model.BCScanned();
                model.WorkOrder = this.textBox2.Text.Trim();                                            //工單
                model.Segment = this.textBox4.Text.Trim();                                              //料號
                model.IPQC = this.textBox6.Text.Trim().ToUpper();                                       //IPQC
                model.AmountPerBox = long.Parse(this.textBox1.Text.Trim());                             //每箱數量 (用戶手動輸入的)
                model.DepartmentID = Convert.ToInt32(this.textBox9.Text.Trim());                        //課別ID
                model.DepartmentCode = this.textBox3.Text.Trim();                                       //課別名稱
                model.LineID =
                    int.Parse((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());   //線別ID
                model.LineCode = this.comboBox1.Text.Trim();                                            //線別名稱
                model.StartQuantity = long.Parse(this.textBox5.Text.Trim());                            //總數量     (系統自動帶出來的)
                model.BarCode = barcode.Trim();                                                         //條碼
                model.WorkNo = FormsVar.login.loginedid.Trim();                                         //已登錄的工號
                model.RepositoryID =
                    (this.comboBox2.SelectedItem as ComboboxItem).Value.ToString().Trim();              //倉別ID
                model.Attribute7 = this.textBox10.Text.Trim();                                          //模號
                model.NDescription = this.textBox11.Text.Trim();                                        //品名
                model.IsBatchScan = IsBatchScan.ToString();

                //判斷是否連續掃描
                if (!this.isContinuousScan(model) && !model.DepartmentCode.Equals("射出課"))
                {
                    this.textBox8.Text = "";
                    this.textBox8.Focus();
                    MessageBox.Show("請依實際生產狀況掃描，不允許連續掃描", "錯誤", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    return;
                }

                



                //保存到DB
                BLL.BCScanned bll = new BLL.BCScanned();
                bool ret_v = bll.Add(model);

                if (ret_v)
                {
                    //插入成功,得到服務器上此條記錄的日期時間
                    string dt = bll.GetLastTime(model).Trim();

                    if (dt == "")
                    {
                        //提示得到日期時間失效
                        MessageBox.Show("不能從服務器得到日期時間,無法掃描,掃描窗口即將關閉", "錯誤", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        this.textBox8.Text = "";
                        this.Close();
                    }
                    else
                    {
                        //更新界面上的日期時間
                        this.textBox7.Text = Convert.ToDateTime(dt.Trim()).ToString("yyyy-MM-dd HH:mm:ss").Trim();

                        //處理項次字符串
                        if (this.label16.Text.Trim() == "")
                        {
                            if (IsBatchScan)
                            {
                                //是批量掃描,將項次號直接變成 每箱個數
                                this.label16.Text = this.textBox1.Text.Trim();
                            }
                            else
                            {
                                //非批量掃描,將項次加1
                                this.label16.Text = "1";
                            }
                        }
                        else
                        {
                            if (IsBatchScan)
                            {
                                //批量掃描,將項次號加上每箱個數
                                this.label16.Text = (Convert.ToInt32(this.label16.Text.Trim()) +
                                    Convert.ToInt32(this.textBox1.Text.Trim())).ToString().Trim();
                            }
                            else
                            {
                                //非批量掃描,則加1
                                this.label16.Text = (Convert.ToInt32(this.label16.Text.Trim()) + 1).ToString().Trim();
                            }
                        }

                        this.Refresh();        //刷新窗體    為什麼要刷新窗體？


                        if (!IsBatchScan)        //未批量掃描時,才有此箱未完成的情況
                        {
                            //此時,此箱未完成,將項次號的相關信息保存到DB,以便某箱未完成時關閉了程式,再打開程式無法知道那箱掃描完成了沒有
                            this.SaveItemNoInfo(true);
                        }


                        //確定是否打印外箱貼紙
                        this.JudgeBCPrint();
                    }
                }
                else
                {
                    //提示保存失敗
                    MessageBox.Show("保存信息失敗,無法打印外箱貼紙", "信息", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                this.textBox8.Text = "";


                model = null;
                bll = null;
            }
        }

        

        private void SaveItemNoInfo(bool mark)
        {

            //當一箱未完成時,將項次號相關信息保存到DB,此時mark的值為true             
            //當一箱完成時,將項次號相關信息從DB中刪除,此時mark的值為false
            //以便某箱未完成時關閉了程式,再打開程式無法知道那箱掃描完成了沒有


            bool ret_v;

            Model.UnfinishedBox model = new Model.UnfinishedBox();
            BLL.UnfinishedBox bll = new BLL.UnfinishedBox();

            model.WorkOrder = this.textBox2.Text.Trim();                 //工單
            model.DepartmentID = int.Parse(this.textBox9.Text.Trim());   //課別ID
            model.LineID =
                Convert.ToInt32((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());    //線別ID
            model.WorkNo = FormsVar.login.loginedid.Trim();              //工號



            if (mark)
            {

                model.ItemNo = int.Parse(this.label16.Text.Trim());          //項次號, 注意是保存記錄時才有正常的項次號,故在此賦值

                ret_v = bll.Add(model);      //保存記錄

                if (!ret_v)
                {
                    //提示保存項次號失敗
                    MessageBox.Show("保存項次號信息失敗,無法繼續掃描,窗口即將關閉", "信息", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    this.Close();
                }
            }
            else
            {
                if (bll.Exists(model))
                {
                    //如果存在記錄,則刪除
                    ret_v = bll.Delete(model);   //刪除記錄                               注意:刪除記錄時是不需要項次號的,此時的項次號被賦值為空串了,無法得到項次號了,項次號為int類型.

                    if (!ret_v)
                    {
                        //提示刪除項次號失敗
                        MessageBox.Show("刪除項次號信息失敗,無法繼續掃描,窗口即將關閉", "信息", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        this.Close();
                    }
                }

            }


            bll = null;
            model = null;
        }

        public bool CheckInput()                                          //判斷兩次按鍵間的時間間隔,從而判斷是掃描槍輸入還是鍵盤輸入
        {
            #region 判斷是否是掃描槍的輸入
            //MessageBox.Show(textBox1.Text.Trim().Length.ToString().Trim());       //文本框為空時,返回值為0,不包括當前按鍵所輸入的字符
            //判斷兩次按鍵間的時間間隔,從而判斷是掃描槍輸入還是鍵盤輸入
            if (textBox8.Text.Trim().Length == 0)
            {
                _dt = DateTime.Now;
            }
            else
            {
                tempdt = DateTime.Now;




                TimeSpan ts = tempdt.Subtract(_dt);     //兩個時間相減
                if (ts.Milliseconds > 500)
                {

                    MessageBox.Show("無效條碼,請用掃描槍輸入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox8.Text = "";
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

        public bool CheckInfo()
        {
            #region 檢查必要的信息是否合法
            //檢查必要的信息是否合法
            if (this.textBox2.Text.Trim() == "")     //工單                
            {
                MessageBox.Show("請輸入工單號", "信息", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                this.textBox2.Focus();
                return false;
            }

            if (this.textBox4.Text.Trim() == "")   //料號
            {
                MessageBox.Show("料號不能為空", "信息", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return false;
            }

            if (this.textBox6.Text.Trim() == "")   //IPQC
            {
                MessageBox.Show("IPQC不能為空", "信息", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                this.textBox6.Focus();
                return false;
            }

            //判斷每箱數量是否合法
            if (!this.JudgetAmountPerBox())
            {
                return false;
            }

            if (this.textBox3.Text.Trim() == "")    //生產課別
            {
                MessageBox.Show("生產課別不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (this.textBox5.Text.Trim() == "")    //總數量
            {
                MessageBox.Show("總數量不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (!Auxiliary.isNumber2(this.textBox5.Text.Trim()))
            {
                MessageBox.Show("數量必須為純數字,不能是小數或其它字符", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            //排除0
            if (Convert.ToInt32(this.textBox5.Text.Trim()) == 0)
            {
                MessageBox.Show("數量不能為0", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }


            if (this.textBox7.Text.Trim() == "")    //日期時間
            {
                MessageBox.Show("日期時間不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (this.textBox9.Text.Trim() == "")    //課別ID
            {
                MessageBox.Show("課別ID不能為空", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (this.comboBox1.Text.Trim() == "")      //線別名稱
            {
                MessageBox.Show("線別不能為空,請選擇線別", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (this.comboBox2.Text.Trim() == "")      //倉別名稱
            {
                MessageBox.Show("倉別不能為空,請選擇倉別", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            return true;
            #endregion
        }

        private void JudgeBCPrint()
        {
            //確定是否要打印外箱貼紙

            //得到工單完工量和工單數量
            int finishedAmount = this.GetFinishedAmount(this.textBox2.Text.Trim());
            int WorkOrderNum = int.Parse(this.textBox5.Text.Trim());
            int perBoxNum = int.Parse(this.textBox1.Text.Trim());

            //現改用前臺界面上直接計數
            if (int.Parse(this.label16.Text.Trim()) >= int.Parse(this.textBox1.Text.Trim()))
            {
                //兩個值相等時就打印外箱貼紙

                int printAmount;    //真正的打量數量

                if (this.checkBox1.Checked)
                {
                    //批量掃描
                    //printAmount = int.Parse(this.textBox1.Text.Trim());    //直接打印每箱個數的值
                    //打印每箱個數的值，增加尾數箱數判斷   Simon 2019.01.22 add
                    if ((finishedAmount + perBoxNum) > WorkOrderNum)
                    {
                        printAmount = WorkOrderNum - finishedAmount;
                    }
                    else
                    {
                        printAmount = perBoxNum;
                    }                        
                }                               
                else
                {
                    //非批量掃描
                    printAmount = int.Parse(this.label16.Text.Trim());     //按項次值打印
                }

                bool ret_v = this.OuterBCPrint(printAmount);               //打印外箱貼紙


                this.label16.Text = "";     //項次號標簽


                //刪除unfinishedbox某箱未完成表中的此條記錄
                this.SaveItemNoInfo(false);


                if (!ret_v)
                {
                    this.Close();   //無法保存打印信息或無法打印時即關閉本窗口.
                }
            }


            //帶出掃描完工量和剩餘量
            //this.ShowInformation();     //現在不用帶這個信息了

        }

        private bool OuterBCPrint(int currentBoxAmount)
        {
            //打印外箱貼紙            

            string[] BC_Array = new string[27];               //實例化為27個元素
            for (int c = 0; c < 27; c++)
            {
                BC_Array[c] = "";                             //為數組賦值為空串""
            }


            //將參數值放入數組中
            BC_Array[0] = this.textBox3.Text.Trim() + this.comboBox1.Text.Trim();       //課別與線別;
            BC_Array[1] = this.textBox2.Text.Trim();                                    //工單
            BC_Array[2] = this.textBox4.Text.Trim();                                    //料號

            string amount = currentBoxAmount.ToString().Trim();                         //當前箱個數
            BC_Array[3] = amount;

            BC_Array[4] = this.textBox6.Text.Trim().ToUpper();                          //IPQC
            BC_Array[5] = this.textBox7.Text.Trim();                                    //日期時間


            //要打印的條碼    (工單+日期時間+當前箱個數)
            /*     取日期時間對象的各種屬性后會得到,如 08月會得到8,02號會得到2,因為它們都返回int ,  不用這种方式取值日期時間的組合值
            string barcode = this.textBox2.Text.Trim() + DateTime.Parse(this.textBox7.Text.Trim()).Year.ToString().Trim() +
                DateTime.Parse(this.textBox7.Text.Trim()).Month.ToString().Trim()+
                DateTime.Parse(this.textBox7.Text.Trim()).Day.ToString().Trim() +
                DateTime.Parse(this.textBox7.Text.Trim()).Hour.ToString().Trim()+
                DateTime.Parse(this.textBox7.Text.Trim()).Minute.ToString().Trim()+
                DateTime.Parse(this.textBox7.Text.Trim()).Second.ToString().Trim()+amount.Trim();
            */


            //直接取日期時間的字符串中的位置
            /*
            string barcode = this.textBox2.Text.Trim() + (this.textBox7.Text.Trim()).Substring(2,2).ToString().Trim() +
                (this.textBox7.Text.Trim()).Substring(5,2).ToString().Trim() +
                (this.textBox7.Text.Trim()).Substring(8,2).ToString().Trim() +
                (this.textBox7.Text.Trim()).Substring(11,2).ToString().Trim() +
                (this.textBox7.Text.Trim()).Substring(14,2).ToString().Trim() +
                (this.textBox7.Text.Trim()).Substring(17,2).ToString().Trim() + amount.Trim();
            */


            //條碼的內容再次修改,內容為登登帳號(即工號)+日期時間,因為一個工號只能在一臺電腦上掃描,在某個時刻,只有一條記錄產生
            string barcode = FormsVar.login.loginedid.Trim() +
               (this.textBox7.Text.Trim()).Substring(2, 2).ToString().Trim() +
               (this.textBox7.Text.Trim()).Substring(5, 2).ToString().Trim() +
               (this.textBox7.Text.Trim()).Substring(8, 2).ToString().Trim() +
               (this.textBox7.Text.Trim()).Substring(11, 2).ToString().Trim() +
               (this.textBox7.Text.Trim()).Substring(14, 2).ToString().Trim() +
               (this.textBox7.Text.Trim()).Substring(17, 2).ToString().Trim();

            BC_Array[6] = barcode.Trim();                 //條碼

            BC_Array[7] = (this.comboBox2.SelectedItem as ComboboxItem).Value.ToString().Trim();    //倉別ID

            BC_Array[8] = this.textBox10.Text.Trim();     //模號

            BC_Array[9] = this.textBox11.Text.Trim();     //品名

            BC_Array[10] =
                (this.GetMaxCartonNO(this.textBox2.Text.Trim()) + 1).ToString().Trim();      //箱號




            //先將相關信息保存到DB,然後再打印,這樣,當打印失敗時可以再重印歷史貼紙
            if (!this.SaveBCPrintInfo(BC_Array))
            {
                return false;
            }



            //得到模板文件
            //string templateFileName = @".\LangChao2.qdf";                //條碼打印模板直接放在當前目錄下即可
            string templateFileName = System.IO.Directory.GetCurrentDirectory() + "\\OuterBarCode.lab";            //“System.IO.Directory.GetCurrentDirectory”:获取当前应用程序的路径，最后不包含“\”；

            //判斷文件存在否
            if (!File.Exists(templateFileName))
            {
                MessageBox.Show("打印模板文件不存在,無法打印外箱貼紙", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }


            BCPrint bcprint = new BCPrint();
            bool ret_v = bcprint.PrintBC(templateFileName, BC_Array);

            bcprint = null;

            if (ret_v)
            {
            }
            else
            {
                MessageBox.Show("打印外箱貼紙失敗,請檢查各項數據是否正確", "信息", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
            }
            return ret_v;

        }

        private int GetMaxCartonNO(string workorder)
        {
            //通過工單號得到最大箱號

            BLL.OuterBarCode bll = new BLL.OuterBarCode();

            int maxCartonNO = bll.GetMaxCartonNO(workorder.Trim());

            bll = null;

            if (maxCartonNO == -1)
            {
                MessageBox.Show("在查詢工單 " + workorder.Trim() +
                    " 的最大箱號時,得到了錯誤的值(-1),請檢查網絡是否正常或聯繫開發人員",
                    "信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                maxCartonNO = 0;                    //當一個工單沒有掃描時,最到的最大箱號是0. 這里仍然讓用戶將當前外箱貼紙打印出來,但要報錯.
            }

            return maxCartonNO;
        }

        private bool SaveBCPrintInfo(string[] bcArray)
        {
            //保存要打印的外箱貼紙信息到DB
            Model.OuterBarCode model = new Model.OuterBarCode();

            model.WorkOrder = bcArray[1];                                                                 //工單
            model.DepartmentID = Convert.ToInt32(this.textBox9.Text.Trim());                              //課別ID
            model.LineID =
                Convert.ToInt32((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());   //線別ID
            model.DeptLineCode = bcArray[0];                                                              //課別名稱與線別名稱
            model.Segment = bcArray[2];                                                                   //料號
            model.CurrentBoxAmount = long.Parse(bcArray[3]);                                              //當前箱個數
            model.IPQC = bcArray[4];                                                                      //IPQC
            model.CurrentBoxTime = Convert.ToDateTime(bcArray[5]);                                        //日期時間
            model.BarCode = bcArray[6];                                                                   //條碼
            model.WorkNo = FormsVar.login.loginedid.Trim();                                               //工號
            model.RepositoryID = bcArray[7];                                                              //倉別ID
            model.Attribute7 = bcArray[8];                                                                //模號
            model.NDescription = bcArray[9];                                                              //品名
            model.CartonNO = int.Parse(bcArray[10]);                                                      //箱號


            BLL.OuterBarCode bll = new BLL.OuterBarCode();

            bool ret_v = bll.Add(model);

            bll = null;
            model = null;


            if (ret_v)
            {
                return true;
            }
            else
            {
                //保存要打印的外箱貼紙信息到數據庫失敗了
                MessageBox.Show("保存要打印的外箱貼紙信息到數據庫失敗了,無法打印外箱貼紙,請聯繫開發人員",
                    "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
        }

       
        private bool JudgeWOComplete()
        {
            //判斷工單是否掃描完工            

            //得到當前工單已掃描完工量            
            int finishedAmount = this.GetFinishedAmount(this.textBox2.Text.Trim());
            
            //當前工單總數量            
            int totalAmount = int.Parse(this.textBox5.Text.Trim());

            
            if (finishedAmount >= totalAmount)       //已掃描的完工量大於總數量,則不能掃描       
            {
                MessageBox.Show("不能掃描,工單已掃描完", "信息", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;     //已掃描完工,返回false
            }
            else
            {
                return true;      //未掃描完工,返回true
            }            
        }

        private bool JudgetAmountPerBox()        //判斷每箱數量是否合法
        {
            //判斷每箱數量是否是純數字
            if (this.textBox1.Text.Trim() == "")    //每箱數量
            {
                MessageBox.Show("請輸入每箱數量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBox1.Text = "";
                this.textBox1.Focus();
                return false;
            }

            if (!Auxiliary.isNumber2(this.textBox1.Text.Trim()))
            {
                MessageBox.Show("每箱數量必須為純數字,不能是小數或其它字符", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.textBox1.Text = "";
                this.textBox1.Focus();
                return false;
            }

            //排除0
            if (Convert.ToInt32(this.textBox1.Text.Trim()) == 0)
            {
                MessageBox.Show("每箱數量不能為0", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.textBox1.Text = "";
                this.textBox1.Focus();
                return false;
            }


            return true;
        }

        private void BCScan_Load(object sender, EventArgs e)
        {
            this.label10.Text = "";
            this.label11.Text = "";
            this.label16.Text = "";

            this.GetRepository();


            this.textBox8.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
        }

        private void GetRepository()
        {
            //加載Oracle DB中的所有倉庫

            BLL.InStore bll = new BLL.InStore();

            DataSet ds = bll.GetRepository();

            this.comboBox2.Items.Clear();                //清空comboBox1控件中的所有項,否則會累加項目，越來越多.
            this.comboBox2.Text = "";


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
                    this.comboBox2.Items.Add(new ComboboxItem(repositoryname, repositoryid));
                }
            }


            bll = null;

        }

        
        
        private int GetFinishedAmount(string workorder)
        {
            //通過工單得到已掃描數量
            BLL.BCScanned bll = new BLL.BCScanned();
            int finishedAmount = bll.FinishedAmount(workorder.Trim());
            bll = null;

            return finishedAmount;
        }   

        private void SetLastAmountPerBox()
        {
            //判斷有沒有選擇線別
            if (this.comboBox1.Text.Trim() == "")
            {
                return;
            }


            //設置每箱數量為此工單此工號最后一次掃描時的每箱數量
            BLL.BCScanned bll = new BLL.BCScanned();
            Model.BCScanned model = new Model.BCScanned();

            string workorder = this.textBox2.Text.Trim();       //工單
            string departmentid = this.textBox9.Text.Trim();    //課別ID
            string lineid =
                (this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim();   //線別ID
            string workno = FormsVar.login.loginedid.Trim();    //工號

            model.WorkOrder = workorder;
            model.DepartmentID = Convert.ToInt32(departmentid);
            model.LineID = Convert.ToInt32(lineid);
            model.WorkNo = workno;

            string lastAmountPerBox = bll.GetLastAmountPerBox(model);

            if (!string.IsNullOrEmpty(lastAmountPerBox))
            {
                this.textBox1.Text = lastAmountPerBox.Trim();
            }

            bll = null;
            model = null;
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "顯示工單上的課別ID";
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "選擇線別";
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //為comboBox加載線別

            //採用DataReader逐個將機種號加入到comboBox控件中
            this.comboBox1.Items.Clear();                //清空comboBox1控件中的所有項,否則會累加項目，越來越多.
            this.comboBox1.Text = "";


            if (this.textBox9.Text.Trim() == "")
            {
                return;
            }


            BLL.DepartmentID bll = new BLL.DepartmentID();
            DataSet ds = bll.GetList(this.textBox9.Text.Trim());


            string line_id, line_code;

            if ((ds != null) && (ds.Tables.Count > 0))
            {
                //循環DS的行,將數據加入到comboBox
                for (int ds_row = 0; ds_row < ds.Tables[0].Rows.Count; ds_row++)
                {
                    //this.comboBox1.Items.Add(ds.Tables[0].Rows[ds_row]["line_code"].ToString().Trim());         //當為DBNull的時候,也是可以ToString()的.

                    line_id = ds.Tables[0].Rows[ds_row]["line_id"].ToString().Trim();               //線別ID
                    line_code = ds.Tables[0].Rows[ds_row]["line_code"].ToString().Trim();           //線別名稱
                    this.comboBox1.Items.Add(new ComboboxItem(line_code, line_id));
                }
            }

            bll = null;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.Text.Trim() == "")
            {           
                this.label16.Text = "";
                return;
            }


            //根據工單和工號,查詢DB,檢查是否有最后一次掃描記錄,有則把最后一次的每箱數量帶出來,無則顯示為空串
            this.SetLastAmountPerBox();

            //帶出項次號
            this.GetItemNo();
        }

        private void GetItemNo()
        {
            //帶出項次號,這樣才能知道上一箱是否有完成,如果沒有沒有完成,那么程序會接著一箱繼續掃描


            if ((this.textBox2.Text.Trim() == "") || (this.textBox9.Text.Trim() == "") ||
                (this.comboBox1.Text.Trim() == ""))
            {
                //如果工單或課別ID或線別ID不合法時,不查詢
                return;
            }


            string workorder = this.textBox2.Text.Trim();          //工單
            string departmentid = this.textBox9.Text.Trim();       //課別ID
            string lineid = (this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim();  //線別ID
            string workno = FormsVar.login.loginedid.Trim();   //工號

            Model.UnfinishedBox model = new Model.UnfinishedBox();
            BLL.UnfinishedBox bll = new BLL.UnfinishedBox();

            model.WorkOrder = workorder;
            model.DepartmentID = int.Parse(departmentid);
            model.LineID = int.Parse(lineid);
            model.WorkNo = workno;

            string itemno = bll.GetItemNo(model);

            this.label16.Text = itemno.Trim();


            bll = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //先檢查登錄信息是否是自己的IP,如果不是自己的IP則表示后面有人用此帳號登錄了系統,那么本機掃描終止.
            if (!this.CheckLoginInfo())
            {
                MessageBox.Show("本帳號已在其它電腦上登錄了,本機無法掃描,掃描窗口即將關閉\r\n\r\n如果要" +
                    "繼續掃描,請重新登錄本系統,再點擊 工單開始 按鈕", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }




            //判斷必要的信息是否合法
            if (!this.CheckInfo())
            {
                return;
            }



            //禁用上面的已填寫好的信息控件
            this.DisableActiveControls(true);

            if (checkBox1.Checked)
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }

            this.button2.Enabled = true;
            this.textBox8.Enabled = true;

            this.button1.Enabled = false;

            this.textBox8.Text = "";
            this.textBox8.Focus();






            //將相關信息保存到DB
            Model.TimeSegment model = new Model.TimeSegment();
            BLL.TimeSegment bll = new BLL.TimeSegment();

            model.WorkOrder = this.textBox2.Text.Trim();                           //工單
            model.DepartmentID = Convert.ToInt32(this.textBox9.Text.Trim());       //課別ID
            model.LineID =
                Convert.ToInt32((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());   //線別ID
            model.WorkNo = FormsVar.login.loginedid.Trim();                        //工號


            //先檢查上一次按暫停按鈕或結束按鈕保存時間失敗的記錄,將它們結束時間改為和AutoTime時間一樣
            bll.UpdateAbnormal(model);


            //將相關信息保存到DB
            bool ret_v = bll.Add(model);

            bll = null;
            model = null;


            if (!ret_v)
            {
                //插入開始掃描時間失敗,提示,關閉窗體,再試
                MessageBox.Show("保存開始時間失敗,請重新再試", "提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
            }




            //開啟計時器
            this.timer1.Start();
        }

        private void DisableActiveControls(bool mark)
        {
            if (mark)
            {
                //禁用上面的已填寫好的信息控件
                this.textBox2.Enabled = false;
                this.comboBox1.Enabled = false;
                this.textBox6.Enabled = false;
                this.textBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.checkBox1.Enabled = false;
            }
            else
            {
                //啟用上面的已填寫好的信息控件
                this.textBox2.Enabled = true;
                this.comboBox1.Enabled = true;
                this.textBox6.Enabled = true;
                this.textBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.checkBox1.Enabled = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((FormsVar.pausereason == null) || (FormsVar.pausereason.IsDisposed))
            {
                FormsVar.pausereason = new PauseReason();
            }

            FormsVar.pausereason.ShowDialog();


            //MessageBox.Show(FormsVar.pausereason.DialogResult.ToString());


            if (FormsVar.pausereason.DialogResult == DialogResult.Cancel)
            {
                //模態窗口按取消按鈕關閉時
                this.textBox8.Text = "";
                this.textBox8.Focus();
                return;
            }





            //啟用上面的已填寫好的信息控件
            this.DisableActiveControls(false);



            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.textBox8.Enabled = false;

            this.button1.Enabled = true;

            this.textBox8.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //啟用上面的已填寫好的信息控件
            this.DisableActiveControls(false);



            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.textBox8.Enabled = false;

            this.button1.Enabled = true;

            this.textBox8.Text = "";



            //停用Timer
            this.timer1.Stop();


            //更新結束時間
            Model.TimeSegment model = new Model.TimeSegment();
            BLL.TimeSegment bll = new BLL.TimeSegment();

            model.WorkOrder = this.textBox2.Text.Trim();   //工單
            model.DepartmentID = int.Parse(this.textBox9.Text.Trim());    //課別ID
            model.LineID =
                Convert.ToInt32((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());   //線別ID
            model.WorkNo = FormsVar.login.loginedid.Trim();   //工號


            bll.UpdateEndTime(model);



            bll = null;
            model = null;



            //打印外箱貼紙
            if ((this.label16.Text.Trim() != "") && (int.Parse(this.label16.Text.Trim()) != 0))
            {
                this.OuterBCPrint(int.Parse(this.label16.Text.Trim()));
            }



            //刪除unfinishedbox某箱未完成表中的此條記錄
            this.SaveItemNoInfo(false);


            //清除項次號
            this.label16.Text = "";
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "";
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            FormsVar.mainfm.statusStrip1.Items[0].Text = "選擇倉別";
        }

        private void BCScan_FormClosed(object sender, FormClosedEventArgs e)
        {
            //停用Timer1
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //更新自動時間   

            Model.TimeSegment model = new Model.TimeSegment();
            BLL.TimeSegment bll = new BLL.TimeSegment();

            model.WorkOrder = this.textBox2.Text.Trim();    //工單
            model.DepartmentID = int.Parse(this.textBox9.Text.Trim());   //課別ID
            model.LineID =
                Convert.ToInt32((this.comboBox1.SelectedItem as ComboboxItem).Value.ToString().Trim());     //線別ID
            model.WorkNo = FormsVar.login.loginedid.Trim();            //工號


            bool ret_v = bll.UpdateAutoTime(model);

            bll = null;
            model = null;
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
            }
        }
    }
}
