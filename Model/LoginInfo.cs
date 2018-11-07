using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LoginInfo
    {
        private string _loginid;
        private string _ip;
        private DateTime _logintime;

        public string LoginID
        {
            set { _loginid = value; }
            get { return _loginid; }
        }

        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }

        public DateTime LoginTime
        {
            set { _logintime = value; }
            get { return _logintime; }
        }
    }
}
