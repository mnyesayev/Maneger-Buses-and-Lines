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
    /// Interaction logic for addStopLine.xaml
    /// </summary>
    public partial class addStopLine : Window
    {
        IBL bl;
        public addStopLine(IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void buttonAddStopLine_Click(object sender, RoutedEventArgs e)
        {
            if (tBNewCode.Text.Length == 0 || tBNewIndex.Text.Length == 0)
                return;
            var idLine=(this.DataContext as PO.Line).IdLine;
            bool isSuccessed=false;
            try
            {
                bl.AddStopLine(idLine,int.Parse(tBNewCode.Text),int.Parse(tBNewIndex.Text));
                isSuccessed = true;
            }
            catch (BO.ConsecutiveStopsException ex)
            {
                 
            }
            while(isSuccessed==false)
            {
                try
                {
                    bl.AddStopLine(idLine, int.Parse(tBNewCode.Text), int.Parse(tBNewIndex.Text));
                    isSuccessed = true;
                }
                catch (BO.ConsecutiveStopsException ex)
                {

                }
            }

        }
    }
}
