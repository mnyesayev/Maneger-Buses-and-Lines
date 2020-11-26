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

namespace dotNet5781_03B_3729_1237
{
    /// <summary>
    /// Interaction logic for wAddBus.xaml
    /// </summary>
    public partial class wAddBus : Window
    {
        // DateTime tmpDra;
        uint id;
        DateTime Dra;//date road ascent
        uint mileage;
        uint mileageLastCare;
        DateTime dateLastCare;
        public wAddBus()
        {
            InitializeComponent();
        }
        private Bus newBus = null;
        public Bus NewBus { get => newBus; private set => newBus = value; }

        private void bDoneAddBus_Click(object sender, RoutedEventArgs e)
        {
            if (ImDRA.Visibility != Visibility.Visible
                || ImIdBusOk.Visibility != Visibility.Visible
                || ImMileageOk.Visibility != Visibility.Visible
                || ImDateLastCareOk.Visibility != Visibility.Visible
                || ImMileageLastCareOk.Visibility != Visibility.Visible)
            {
                MessageBox.Show("Fill in all the fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                newBus = new Bus(Dra, id);
                newBus.Mileage = mileage;
                newBus.LastCareMileage = mileageLastCare;
                newBus.LastCare = dateLastCare;
                this.Close();
            }
        }

        private void dPDRA_calendrer_closed(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (!DateTime.TryParse(dPDRA.Text, out Dra))
                return;
            if (Dra > DateTime.Now)
            {
                MessageBox.Show("you can not enter futher date!", "ERROR DATE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                tBIdBus.IsEnabled = true;
                ImDRA.Visibility = Visibility.Visible;
                //dPDRA.IsEnabled = false;
                tBidBus_TextChanged(sender, e);
                dPDateLastCare_CalendarClosed(sender, e);
                tBMileage_TextChanged(sender, e);
                tBMileageLastCare_TextChanged(sender, e);
            }
        }


        private void tBidBus_TextChanged(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (Dra.Year >= 2018 && tBIdBus.Text.Length == 8 && uint.TryParse(tBIdBus.Text, out id)
                && id > 10000000 && id < 99999999)
            {
                ImIdBusError.Visibility = Visibility.Hidden;
                ImIdBusOk.Visibility = Visibility.Visible;
                tBMileage.IsEnabled = true;
            }
            else if (Dra.Year < 2018 && tBIdBus.Text.Length == 7 && uint.TryParse(tBIdBus.Text, out id)
                && id > 1000000 && id < 9999999)
            {
                ImIdBusError.Visibility = Visibility.Hidden;
                ImIdBusOk.Visibility = Visibility.Visible;
                tBMileage.IsEnabled = true;
            }
            else
            {
                ImIdBusError.Visibility = Visibility.Visible;
                tBMileage.IsEnabled = false;
                //MessageBox.Show("Enter ligal format of bus id", "ERROR ID", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void tBMileage_TextChanged(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (uint.TryParse(tBMileage.Text, out mileage))
            {
                ImMileageError.Visibility = Visibility.Hidden;
                ImMileageOk.Visibility = Visibility.Visible;
                dPDateLastCare.IsEnabled = true;
                tBMileageLastCare_TextChanged(sender, e);
            }
            else
            {
                ImMileageOk.Visibility = Visibility.Hidden;
                ImMileageError.Visibility = Visibility.Visible;
                dPDateLastCare.IsEnabled = false;
            }
        }

        private void dPDateLastCare_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (dPDateLastCare.SelectedDate >= dPDRA.SelectedDate && dPDateLastCare.SelectedDate <= DateTime.Now)
            {
                ImDateLastCareError.Visibility = Visibility.Hidden;
                ImDateLastCareOk.Visibility = Visibility.Visible;
                tBMileageLastCare.IsEnabled = true;
                dateLastCare = DateTime.Parse(dPDateLastCare.Text);
            }
            else
            {
                ImDateLastCareError.Visibility = Visibility.Visible;
                ImDateLastCareOk.Visibility = Visibility.Hidden;
                tBMileageLastCare.IsEnabled = false;
            }
        }

        private void tBMileageLastCare_TextChanged(object sender, EventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (uint.TryParse(tBMileageLastCare.Text, out mileageLastCare) && mileage >= mileageLastCare)
            {
                ImMileageLastCareError.Visibility = Visibility.Hidden;
                ImMileageLastCareOk.Visibility = Visibility.Visible;
            }
            else
            {
                ImMileageLastCareError.Visibility = Visibility.Visible;
                ImMileageLastCareOk.Visibility = Visibility.Hidden;
            }
        }
    }
}
