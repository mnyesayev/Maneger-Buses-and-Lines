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
using System.Threading;
using System.Collections.ObjectModel;
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for addStopLine.xaml
    /// </summary>
    public partial class addStopLine : Window
    {
        IBL bl;
        ObservableCollection<PO.Line> Lines;
        ListView ListViewStops;
        public addStopLine(IBL bl,ObservableCollection<PO.Line> lines,ListView listStops)
        {
            InitializeComponent();
            this.bl = bl;
            Lines = lines;
            ListViewStops = listStops;
        }

        private void buttonAddStopLine_Click(object sender, RoutedEventArgs e)
        {
            if (tBCode.Text.Length == 0 || tBNewIndex.Text.Length == 0)
                return;

            var idLine = (this.DataContext as PO.Line).IdLine;
            bool isSuccessed = false;
            BO.Line upline = null;
            try
            {
                int code = int.Parse(tBCode.Text);
                upline=bl.AddStopLine(idLine, code, int.Parse(tBNewIndex.Text));
                if (upline == null)
                {
                    MessageBox.Show($"stop {code} not exits in system", "Add Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                isSuccessed = true;
            }
            catch (BO.ConsecutiveStopsException ex)
            {
                wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                wEditSuccessive.ShowDialog();
                if (wEditSuccessive.IsVisible == false)
                    return;
            }
            while (isSuccessed == false)
            {
                try
                {
                    int code =int.Parse(tBCode.Text);
                    upline=bl.AddStopLine(idLine, code, int.Parse(tBNewIndex.Text));
                    isSuccessed = true;
                }
                catch (BO.ConsecutiveStopsException ex)
                {
                    wEditSuccessiveStations wEditSuccessive = new wEditSuccessiveStations(bl);
                    wEditSuccessive.tbcode1.Text = ex.Code1.ToString();
                    wEditSuccessive.tbcode2.Text = ex.Code2.ToString();
                    wEditSuccessive.ShowDialog();
                    if (wEditSuccessive.IsVisible == false)
                        return;
                }
            }
            int index=Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            var temp = new PO.Line();
            Cloning.DeepCopyTo(upline, temp);
            Lines[index].StopsInLine=temp.StopsInLine;
            Lines[index].NameFirstLineStop = temp.NameFirstLineStop;
            Lines[index].NameLastLineStop = temp.NameLastLineStop;
            ListViewStops.DataContext = Lines[index].StopsInLine;
            this.Close();
        }
    }
}
