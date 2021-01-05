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

namespace PlGui
{

    public partial class wSearchStop : Window
    {
        public int CodeStop;
        public string NameStop;
        public bool itsNumber = false;
        public wSearchStop()
        {
            InitializeComponent();
        }

        private void TbStopCode_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!int.TryParse(TbStopCode.Text, out CodeStop))
            {
                NameStop = TbStopCode.Text;
                itsNumber = false;
            }
            else
            {
                CodeStop = int.Parse(TbStopCode.Text);
                itsNumber = true;
            }
        }
    }
}
