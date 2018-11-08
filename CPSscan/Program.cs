using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OAUS.Core;
using System.Configuration;

namespace CPSscan
{
    static class Program
    {        
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LoginForm());

            string serverIP = ConfigurationManager.AppSettings["ServerIP"];
            int serverPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]);
            string projectName = AppDomain.CurrentDomain.BaseDirectory;
            projectName = projectName.Substring(0, projectName.Length - 1);
            projectName = projectName.Substring(projectName.LastIndexOf("\\") + 1);

            try
            {
                if (VersionHelper.HasNewVersion(serverIP, serverPort, projectName))
                {
                    string updateExePath = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\AppUpdate.exe";
                    System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(updateExePath);
                    System.Environment.Exit(0);
                }
                else
                {
                    if ((FormsVar.login == null) || (FormsVar.login.IsDisposed))
                    {
                        FormsVar.login = new Login();
                    }

                    Application.Run(FormsVar.login);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("更新失敗！e=" + e.Message);
                if ((FormsVar.login == null) || (FormsVar.login.IsDisposed))
                {
                    FormsVar.login = new Login();
                }

                Application.Run(FormsVar.login);
            }

            
        }
    }
}
