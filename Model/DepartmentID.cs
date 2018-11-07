using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DepartmentID
    {
        private int _department_id;
        private string _department_code;
        private int _line_id;
        private string _line_code;

        public int Department_ID
        {
            set { _department_id = value; }
            get { return _department_id; }
        }

        public string Department_Code
        {
            set { _department_code = value; }
            get { return _department_code; }
        }

        public int Line_ID
        {
            set { _line_id = value; }
            get { return _line_id; }
        }

        public string Line_Code
        {
            set { _line_code = value; }
            get { return _line_code; }
        }
    }
}
