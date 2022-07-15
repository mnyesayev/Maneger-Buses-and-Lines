using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLGui
{
    /// <summary>
    /// Interaction logic for wSearchLine.xaml
    /// </summary>
    public partial class wSearchLine : Window
    {
        public string numLine;
        public wSearchLine()
        {
            InitializeComponent();
        }

        private void TbLineCode_TextChanged(object sender, TextChangedEventArgs e)
        {
           numLine = TbLineCode.Text;
        }

        private void TbLineCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Close();
        }
    }
}
