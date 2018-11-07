using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace Model
{
    [Table(Name = "MenuPermission")]
    public class MenuPermission
    {
        private string _workno;
        private string _disablemenuitem;
        private string _menuitemdesc;

        [Column(Name = "WorkNo")]
        public string WorkNo
        {
            set { _workno = value; }
            get { return _workno; }
        }

        [Column(Name = "DisableMenuItem")]
        public string DisableMenuItem
        {
            set { _disablemenuitem = value; }
            get { return _disablemenuitem; }
        }

        [Column(Name = "MenuItemDesc")]
        public string MenuItemDesc
        {
            set { _menuitemdesc = value; }
            get { return _menuitemdesc; }
        }



        //public string WorkNo { get; set; }
        //public string DisableMenuItem { get; set; }
        //public string MenuItemDesc { get; set; }

    }
}
