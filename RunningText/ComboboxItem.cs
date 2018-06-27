using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunningText
{
    class ComboboxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
