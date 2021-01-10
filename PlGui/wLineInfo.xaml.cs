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
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for wLineInfo.xaml
    /// </summary>
    public partial class wLineInfo : Window
    {
        IBL bl;
        public wLineInfo(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
