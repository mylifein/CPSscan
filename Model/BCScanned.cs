using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BCScanned
    {
        private string _workorder;
        private string _segment;
        private long _startquantity;
        private int _departmentid;
        private string _departmentcode;
        private int _lineid;
        private string _linecode;
        private string _ipqc;
        private DateTime _currenttime;
        private long _amountperbox;
        private string _barcode;
        private string _workno;
        private DateTime _savetime;
        private string _repositoryid;
        private string _attribute7;
        private string _ndescription;
        private string _isBatchScan;

        public string IsBatchScan
        {
            get { return _isBatchScan; }
            set { _isBatchScan = value; }
        }

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

        public int LineID
        {
            set { _lineid = value; }
            get { return _lineid; }
        }

        public string LineCode
        {
            set { _linecode = value; }
            get { return _linecode; }
        }

        public string IPQC
        {
            set { _ipqc = value; }
            get { return _ipqc; }
        }

        public DateTime CurrentTime
        {
            set { _currenttime = value; }
            get { return _currenttime; }
        }

        public long AmountPerBox
        {
            set { _amountperbox = value; }
            get { return _amountperbox; }
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
    }
}
