using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OuterBarCode
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
        private string _workno;
        private DateTime _savetime;
        private string _repositoryid;
        private string _attribute7;
        private string _ndescription;
        private int _cartonno;


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

        public string WorkNo
        {
            set { _workno = value; }
            get { return _workno; }
        }

        public DateTime SaveTime
        {
            set { _savetime = value; }
            get { return _savetime; }
        }

        public string RepositoryID
        {
            set { _repositoryid = value; }
            get { return _repositoryid; }
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

        public int CartonNO
        {
            set { _cartonno = value; }
            get { return _cartonno; }        
        }
    }
}
