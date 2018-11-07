using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WorkOrderBC
    {
        private string _workorder;
        private string _segment;
        private long _startquantity;
        private int _departmentid;
        private string _departmentcode;
        private string _classcode;
        private string _ddescription;
        private string _workno;        
        private DateTime _printtime;
        private string _completionsubinventory;
        private string _attribute7;
        private string _ndescription;

        public string WorkOrder
        {
            set { _workorder = value; }
            get { return _workorder; }
        }

        public string Segment
        {
            set { _segment = value; }
            get { return _segment; }
        }

        public long StartQuantity
        {
            set { _startquantity = value; }
            get { return _startquantity; }
        }

        public int DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }

        public string DepartmentCode
        {
            set { _departmentcode = value; }
            get { return _departmentcode; }
        }

        public string ClassCode
        {
            set { _classcode = value; }
            get { return _classcode; }
        }

        public string DDescription
        {
            set { _ddescription = value; }
            get { return _ddescription; }
        }

        public string WorkNo
        {
            set { _workno = value; }
            get { return _workno; }
        }       

        public DateTime PrintTime
        {
            set { _printtime = value; }
            get { return _printtime; }
        }

        public string CompletionSubinventory
        {
            set { _completionsubinventory = value; }
            get { return _completionsubinventory; }
        }

        public string Attribute7
        {
            set { _attribute7 = value; }
            get { return _attribute7; }
        }

        public string NDescription
        {
            set { _ndescription = value; }
            get { return _ndescription; }
        }
    }
}
