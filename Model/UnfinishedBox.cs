using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UnfinishedBox
    {
        private string _workorder;
        private int _departmentid;
        private int _lineid;
        private DateTime _savetime;
        private string _workno;
        private int _itemno;

        public string WorkOrder
        {
            set { _workorder = value; }
            get { return _workorder; }
        }

        public int DepartmentID
        {
            set {_departmentid=value;}
            get { return _departmentid;}            
        }

        public int LineID
        {
            set { _lineid = value; }
            get { return _lineid; }
        }

        public DateTime SaveTime
        {
            set { _savetime = value; }
            get { return _savetime; }
        }

        public string WorkNo
        {
            set { _workno = value; }
            get { return _workno; }
        }

        public int ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }        
        }
    }
}
