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
    public partial class MainFm : Form
    {
        string loginedid;
        public MainFm()
        {
            InitializeComponent();
        }

        public MainFm(string loginedid)
        {
            this.loginedid = loginedid;
            InitializeComponent();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormsVar.login.Close();        //關閉啟動窗體以退出程序
        }

        private void AboutThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.about == null) || (FormsVar.about.IsDisposed))
            {
                FormsVar.about = new About();
            }

            FormsVar.about.ShowDialog();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void HorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);                  //還有這個  MdiLayout.ArrangeIcons
        }

        private void VerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void ResetPwdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.resetpwd == null) || (FormsVar.resetpwd.IsDisposed))
            {
                FormsVar.resetpwd = new ResetPwd();
            }

            FormsVar.resetpwd.ShowDialog();
        }

        private void WOBCPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.wobcprint == null) || (FormsVar.wobcprint.IsDisposed))
            {
                FormsVar.wobcprint = new WOBCPrint();
            }

            FormsVar.wobcprint.MdiParent = this;

            FormsVar.wobcprint.Show();
            FormsVar.wobcprint.Activate();
        }

        private void ProductLineToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "產線菜單";
        }

        private void ProductLineToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void WOBCPrintToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "列印工單條碼";
        }

        private void WOBCPrintToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void ExitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "退出本系統";
        }

        private void ExitToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "就緒";

            //執行禁用相關的菜單
            this.DisableMenuItems();
        }

        private void DisableMenuItems()
        {
            //此方法是重點.特別注意.

            BLL.MenuPermission bll = new BLL.MenuPermission();

            List<string> disableMenus= bll.GetDisableMenuItems(FormsVar.login.loginedid.Trim());            //LinqToSql直接執行Linq擴展方法.
            //List<string> disableMenus = bll.GetDisableMenuItemsBySql(FormsVar.login.loginedid.Trim());    //讓LinqToSql執行SQL語句.

            this.CheckMenu(this.MainMenu, disableMenus);

            bll = null;
        }

        private void CheckSubMenu(ToolStripMenuItem menuItem, List<string> menuItemName)
        {

            /*
            if (menuItemName.Any(x => x == menuItem.Name))
            {
                //...
            }
            */


            if (menuItemName.Contains(menuItem.Name))        //menuItemName.Any();    //注意: Any方法,見上面代碼
            {
                menuItem.Enabled = false;
            }

            for (int i = 0; i < menuItem.DropDownItems.Count; i++)
            {
                if (menuItem.DropDownItems[i] is ToolStripSeparator)
                {
                    continue;
                }
                else
                {
                    //遞歸調用自己
                    CheckSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i], menuItemName);
                }
            }
        }

        //從DB中查詢出要禁用的菜單項,然後循環遞歸調用自己,檢查是否是要禁用項目.
        private void CheckMenu(MenuStrip Menu, List<string> menuItemName)
        {
            foreach (ToolStripMenuItem n in Menu.Items)
            {
                CheckSubMenu(n, menuItemName);
            }
        }
                
        /*   這是網上原來的代碼
        private void CheckSubMenu(ToolStripMenuItem menuItem, string menuItemName)
        {
            if (menuItem.Name.Equals(menuItemName))
            {
                menuItem.Enabled = false;
            }

            for (int i = 0; i < menuItem.DropDownItems.Count; i++)
            {
                if (menuItem.DropDownItems[i] is ToolStripSeparator)
                {
                    continue;
                }
                else
                {
                    //遞歸調用自己
                    CheckSubMenu((ToolStripMenuItem)menuItem.DropDownItems[i], menuItemName);
                }
            }
        }

        //從DB中查詢出要禁用的菜單項,然後循環遞歸調用自己,檢查是否是要禁用項目.
        private void CheckMenu(MenuStrip Menu, string menuItemName)
        {
            foreach (ToolStripMenuItem n in Menu.Items)
            {
                CheckSubMenu(n, menuItemName);
            }
        }
        */

        private void BCScanToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "進行條碼掃描";
        }

        private void BCScanToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void BCScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.bcscan == null) || (FormsVar.bcscan.IsDisposed))
            {
                FormsVar.bcscan = new BCScan();
            }

            FormsVar.bcscan.MdiParent = this;

            FormsVar.bcscan.Show();
            FormsVar.bcscan.Activate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("確定要退出系統嗎?", "詢問", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                //取消退出
                e.Cancel = true;
                BCPrint bcprint = BCPrint.getInstance();
                bcprint.close();
            }
        }

        private void SetupToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "設置菜單";
        }

        private void SetupToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void ResetPwdToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "重新設置密碼";
        }

        private void ResetPwdToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void AboutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "關於菜單";
        }

        private void AboutToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void AboutThisToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "關於本軟件的一些信息";
        }

        private void AboutThisToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void RepositoryToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "倉庫菜單";
        }

        private void RepositoryToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void InStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.instore == null) || (FormsVar.instore.IsDisposed))
            {
                FormsVar.instore = new InStore();
            }

            FormsVar.instore.MdiParent = this;

            FormsVar.instore.Show();
            FormsVar.instore.Activate();
        }

        private void InStoreToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "進行入庫";
        }

        private void InStoreToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void QueryToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "查詢菜單";
        }

        private void QueryToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void QueryStoredToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "查詢已入庫信息";
        }

        private void QueryStoredToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void QueryStoredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.querystored == null) || (FormsVar.querystored.IsDisposed))
            {

                FormsVar.querystored = new QueryStored();
            }

            FormsVar.querystored.MdiParent = this;

            FormsVar.querystored.Show();
            FormsVar.querystored.Activate();
        }

        private void RepeatPrttoolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "重新列印歷史貼紙";
        }

        private void RepeatPrttoolStripMenuItem1_MouseLeave(object sender, EventArgs e)
        {
            this.statusStrip1.Items[0].Text = "";
        }

        private void RepeatPrttoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((FormsVar.repeatprtouterbc == null) || (FormsVar.repeatprtouterbc.IsDisposed))
            {
                FormsVar.repeatprtouterbc = new RepeatPrtOuterBC();
            }

            FormsVar.repeatprtouterbc.MdiParent = this;

            FormsVar.repeatprtouterbc.Show();
            FormsVar.repeatprtouterbc.Activate();

        }

        private void ContainerNumSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((FormsVar.containerNumSet == null) || (FormsVar.containerNumSet.IsDisposed))
            {
                FormsVar.containerNumSet = new ContainerNumSet(this.loginedid);
            }

            FormsVar.containerNumSet.MdiParent = this;
            FormsVar.containerNumSet.Show();
            FormsVar.containerNumSet.Activate();
        }
    }
}
