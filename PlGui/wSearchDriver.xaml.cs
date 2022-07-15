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
    /// Interaction logic for wSearchDriver.xaml
    /// </summary>
    public partial class wSearchDriver : Window
    {
        public uint DriverID;
        public wSearchDriver()
        {
            InitializeComponent();
        }

        private void TbDriverId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse(TbDriverId.Text, out DriverID))
                return;
            else DriverID = uint.Parse(TbDriverId.Text);
        }

        private void TbDriverId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                this.Close();
        }
    }
}
