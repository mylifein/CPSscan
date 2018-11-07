using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CPSscan.Common
{
    public class LocalIP
    {
        ///<summary>
        ///獲取本機IP地址(IPv4)
        ///</summary>
        ///<returns>本地IPv4地址</returns>
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName();   //得到主機名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);

                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //從IP地址列表中篩選IPv4類型的IP地址
                    //AddressFamily.InterNetwork表示此IP為IPv4
                    //AddressFamily.InterNetworkV6表示此IP為IPv6類型

                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return ""; 
            }
        }

    }
}
