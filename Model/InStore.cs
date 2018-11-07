using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class InStore
    {
        private string _workorder;
        private int _departmentid;
        private int _lineid;
        private string _deptlinecode;
        private string _segment;
        private long _currentboxamount;
        private string _ipqc;
        private DateTime _currentboxtime;
        private string _barcode;
        private string _originalworkno;
        private string _nowscannedworkno;
        private string _repositoryid;
        private string _repositoryname;
        private DateTime _storedtime;

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

        public string DeptLineCode
        {
            set { _deptlinecode = value; }
            get { return _deptlinecode; }
        }

        public string Segment
        {
            set { _segment = value; }
            get { return _segment; }
        }

        public long CurrentBoxAmount
        {
            set { _currentboxamount = value; }
            get { return _currentboxamount; }
        }

        public string IPQC
        {
            set { _ipqc = value; }
            get { return _ipqc; }
        }

        public DateTime CurrentBoxTime
        {
            set { _currentboxtime = value; }
            get { return _currentboxtime; }
        }

        public string BarCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }

        public string OriginalWorkNo
        {
            set { _originalworkno = value; }
            get { return _originalworkno; }
        }

        public string NowScannedWorkNo
        {
            set { _nowscannedworkno = value; }
            get { return _nowscannedworkno; }
        }

        public string RepositoryID
        {
            set { _repositoryid = value; }
            get { return _repositoryid; }
        }

        public string RepositoryName
        {
            set { _repositoryname = value; }
            get { return _repositoryname; }
        }

        public DateTime StoredTime
        {
            set { _storedtime = value; }
            get { return _storedtime; }
        }

    }
}
