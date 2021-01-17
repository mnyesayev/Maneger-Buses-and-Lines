using BlApi;
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
    /// Interaction logic for wEditTrip.xaml
    /// </summary>
    public partial class wEditTrip : Window
    {
        IBL bl;
        PO.Lists Lists;
        public PO.Line line;
        public wEditTrip(IBL bL, PO.Lists lists)
        {
            InitializeComponent();
            bl = bL;
            Lists = lists;
            this.DataContext = line;
            
           
        }

        private void bAddNewfrequency_Click(object sender, RoutedEventArgs e)
        {
            if (TPstartTime == null || TPendTime == null || TBfrequency.Text.Length == 0)
                return;
            if (!TimeSpan.TryParse(TPstartTime.Text, out TimeSpan ss) || !TimeSpan.TryParse(TPendTime.Text, out TimeSpan ee))
                return;

            BO.LineTrip lineTrip = bl.AddLineTrip(line.IdLine, TimeSpan.Parse(TPstartTime.Text), TimeSpan.Parse(TPendTime.Text), int.Parse(TBfrequency.Text));
            PO.LineTrip newLIneTrip = new PO.LineTrip();
            lineTrip.DeepCopyTo(newLIneTrip);
           

            int index = Lists.Lines.ToList().FindIndex(l => l.IdLine == line.IdLine);
            Lists.Lines[index].Trips.Add(newLIneTrip);
            
            
        }
    }
}
