using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        private string _loginid;
        private string _username;
        private string _deptname;
        private string _pwd;

        public string LoginID
        {
            set { _loginid = value; }
            get { return _loginid; }
        }

        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        
        public string DeptName
        {
            set { _deptname = value; }
            get { return _deptname; }            
        }

        public string Pwd
        {
            set { _pwd = value; }
            get { return _pwd; }

        }
    }
}
