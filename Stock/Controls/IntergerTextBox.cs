using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stock
{
    public class IntergerTextBox : TextBox
    {
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            Text = new string(Text.Where(c => Char.IsDigit(c)).ToArray());
            this.SelectionStart = this.Text.Length; 
        }

    }
}
