using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CPSscan.Common
{
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public ComboboxItem()
        {
        }

        public ComboboxItem(string text, object value)
        {
            this.Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
