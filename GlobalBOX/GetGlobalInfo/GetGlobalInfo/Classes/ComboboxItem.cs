using System;
using System.Collections.Generic;
using System.Text;

namespace Pulsar
{
    public class ComboboxItem 
    { 
        public string Text { get; set; } 
        public object Value { get; set; }
        public override string ToString()
        { 
            return Text; 
        }

        //private void Test()
        //{ 
        //    ComboboxItem item = new ComboboxItem(); 
        //    item.Text = "Item text1"; item.Value = 12;
        //    comboBox1.Items.Add(item);
        //    comboBox1.SelectedIndex = 0;
        //    MessageBox.Show((comboBox1.SelectedItem as ComboboxItem).Value.ToString());
        //} 
    } 
}
