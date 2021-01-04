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
    /// <summary>
    /// Interaction logic for wSearchBus.xaml
    /// </summary>
    public partial class wSearchBus : Window
    {
        public uint busID;

        public wSearchBus()
        {
            InitializeComponent();
        }

        private void TbBusId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!uint.TryParse(TbBusId.Text, out busID))
                return;
            else busID = uint.Parse(TbBusId.Text);
        }
    }
}
