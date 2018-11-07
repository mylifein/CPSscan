using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TimeSegment
    {
        private string _workorder;
        private int _departmentid;
        private int _lineid;
        private string _workno;
        private DateTime _time0;
        private DateTime _time1;
        private string _reason;
        private DateTime _autotime;

        public string WorkOrder
        {
            set { _workorder = value; }
            get { return _workorder; }
        }

        public int DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }

        public int LineID
        {
            set { _lineid = value; }
            get { return _lineid; }
        }

        public string WorkNo
        {
            set { _workno = value; }
            get { return _workno; }
        }

        public DateTime Time0
        {
            set { _time1 = value; }
            get { return _time1; }
        }

        public DateTime Time1
        {
            set { _time0 = value; }
            get { return _time0; }
        }

        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }

        public DateTime AutoTime
        {
            set { _autotime = value; }
            get { return _autotime; }
        }
    }
}
