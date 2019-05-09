using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LabelManager2;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace CPSscan.Common
{
    public class BCPrint
    {
        ApplicationClass lbl;
        static BCPrint instance;
        private static PrintDocument fPrintDocument = new PrintDocument();

        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }

        public BCPrint()
        {
            lbl = new ApplicationClass();
        }

        public static BCPrint getInstance()
        {
            if (instance == null)
                instance = new BCPrint();
            return instance;
        }

        public void close()
        {
            if (lbl != null)
            {
                lbl.Quit();
            }
            killProcess();

        }

        void killProcess()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程
            
            for (int i = 0; i < pro.Length; i++)
            {
                
                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString().ToLower() == "lppa")
                {
                    pro[i].Kill();//结束进程
                }
            }
        }


        /*  要註冊32位的VB 6編寫的ActiveX DLL文件才行,但在64位的OS系統中,此DLL文件註冊后無法被調用,故用下面的方法.
        public bool PrintBC(string templateFileName, string[] BCArray)
        {
            //實際打印條碼
            //調用自定義類打印條碼
            try
            {
                LMCL_cgClass LMCL_cC = new LMCL_cgClass();


                LMCL_cC.bc_print24(templateFileName.Trim(), BCArray[0], BCArray[1], BCArray[2], BCArray[3], BCArray[4], BCArray[5],
                    BCArray[6], BCArray[7], BCArray[8], BCArray[9], BCArray[10], BCArray[11], BCArray[12], BCArray[13],
                        BCArray[14], BCArray[15], BCArray[16], BCArray[17], BCArray[18], BCArray[19], BCArray[20], BCArray[21],
                        BCArray[22], BCArray[23], BCArray[24], BCArray[25], BCArray[26], 1);

                


                //釋放 COM 對象實例,   在.NET环境(托管环境,Managed)中释放COM组件对象与释放.NET对象不同.            
                System.Runtime.InteropServices.Marshal.ReleaseComObject(LMCL_cC);

                //將對象變量實例毀掉.              
                LMCL_cC = null;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        */

        public bool PrintBC(string templateFileName, string[] BCArray)
        {
            //未加載模板文件或者模板發生變更時，重新加載新的模板
            if (lbl.Documents.Count == 0 || templateFileName.IndexOf(lbl.ActiveDocument.Name) == -1 ) 
            {                               
                lbl.Documents.Open(templateFileName, false);// 调用设计好的label文件

            }
            Document doc = lbl.ActiveDocument;
            try
            { 
                doc.Variables.FormVariables.Item("Var0").Value = BCArray[0].Trim();   //给参数传值             可以不傳參數
                doc.Variables.FormVariables.Item("Var1").Value = BCArray[1].Trim();   //给参数传值
                doc.Variables.FormVariables.Item("Var2").Value = BCArray[2].Trim();
                doc.Variables.FormVariables.Item("Var3").Value = BCArray[3].Trim();
                doc.Variables.FormVariables.Item("Var4").Value = BCArray[4].Trim();
                doc.Variables.FormVariables.Item("Var5").Value = BCArray[5].Trim();
                doc.Variables.FormVariables.Item("Var6").Value = BCArray[6].Trim();
                doc.Variables.FormVariables.Item("Var7").Value = BCArray[7].Trim();
                doc.Variables.FormVariables.Item("Var8").Value = BCArray[8].Trim();
                doc.Variables.FormVariables.Item("Var9").Value = BCArray[9].Trim();
                doc.Variables.FormVariables.Item("Var10").Value = BCArray[10].Trim();
                doc.Variables.FormVariables.Item("Var11").Value = BCArray[11].Trim();
                doc.Variables.FormVariables.Item("Var12").Value = BCArray[12].Trim();
                doc.Variables.FormVariables.Item("Var13").Value = BCArray[13].Trim();
                doc.Variables.FormVariables.Item("Var14").Value = BCArray[14].Trim();
                doc.Variables.FormVariables.Item("Var15").Value = BCArray[15].Trim();
                doc.Variables.FormVariables.Item("Var16").Value = BCArray[16].Trim();
                doc.Variables.FormVariables.Item("Var17").Value = BCArray[17].Trim();
                doc.Variables.FormVariables.Item("Var18").Value = BCArray[18].Trim();
                doc.Variables.FormVariables.Item("Var19").Value = BCArray[19].Trim();
                doc.Variables.FormVariables.Item("Var20").Value = BCArray[20].Trim();
                doc.Variables.FormVariables.Item("Var21").Value = BCArray[21].Trim();
                doc.Variables.FormVariables.Item("Var22").Value = BCArray[22].Trim();
                doc.Variables.FormVariables.Item("Var23").Value = BCArray[23].Trim();
                doc.Variables.FormVariables.Item("Var24").Value = BCArray[24].Trim();
                doc.Variables.FormVariables.Item("Var25").Value = BCArray[25].Trim();
                doc.Variables.FormVariables.Item("Var26").Value = BCArray[26].Trim();


                int Num = 1;                      //打印数量
                doc.Printer.SwitchTo(DefaultPrinter()); 
                doc.PrintLabel(1, 1, 1, Num, 1, "");
                //doc.PrintDocument(Num);           //打印               

            }
            catch (Exception ex)
            {                
                return false;                          //返回,後面代碼不執行
            }
            finally
            {
                doc.FormFeed();

                /*
                lbl.Quit();                                         //退出
                //釋放 COM 對象實例
                System.Runtime.InteropServices.Marshal.ReleaseComObject(lbl);
                if (lbl != null)
                {                    
                    lbl = null;
                }
                */
            }

            
            return true;

        }
    }
}
