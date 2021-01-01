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
        private BO.Bus newBus = null;
        public BO.Bus NewBus { get => newBus; private set => newBus = value; }

        private void bDoneAddBus_Click(object sender, RoutedEventArgs e)
        {
            if (rbOld.IsChecked == true && (ImDRA.Visibility != Visibility.Visible
                || ImIdBusOk.Visibility != Visibility.Visible
                || ImMileageOk.Visibility != Visibility.Visible
                || ImDateLastCareOk.Visibility != Visibility.Visible
                || ImMileageLastCareOk.Visibility != Visibility.Visible))
            {
                MessageBox.Show("Fill in all fields correctly", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (rbNew.IsChecked == true && (ImDRA.Visibility != Visibility.Visible
                || ImIdBusOk.Visibility != Visibility.Visible))
            {
                MessageBox.Show("Fill in all fields correctly", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                newBus = new BO.Bus();
                newBus.DateRoadAscent = Dra;
                newBus.Id = id;
                if (rbOld.IsChecked == true)
                {
                    newBus.Mileage = mileage;
                    newBus.LastCareMileage = mileageLastCare;
                    newBus.LastCare = dateLastCare;
                }
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
                MessageBox.Show("You can not enter futher date!", "ERROR DATE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                tBIdBus.IsEnabled = true;
                ImDRA.Visibility = Visibility.Visible;
                tBidBus_TextChanged(sender, e);
                if (rbOld.IsChecked == true)
                {
                    dPDateLastCare_CalendarClosed(sender, e);
                    tBMileage_TextChanged(sender, e);
                    tBMileageLastCare_TextChanged(sender, e);
                }
            }
        }


        private void tBidBus_TextChanged(object sender, RoutedEventArgs e)
        {
            if (tBIdBus.Text.Length == 0 || tBIdBus.IsEnabled == false) return;
            if (sender == null) return;
            if (e == null) return;
            if (Dra.Year >= 2018 && tBIdBus.Text.Length == 8 && uint.TryParse(tBIdBus.Text, out id)
                && id >= 10000000 && id <= 99999999)
            {
                ImIdBusError.Visibility = Visibility.Hidden;
                ImIdBusOk.Visibility = Visibility.Visible;
                tBMileage.IsEnabled = true;
            }
            else if (Dra.Year < 2018 && tBIdBus.Text.Length == 7 && uint.TryParse(tBIdBus.Text, out id)
                && id >= 1000000 && id <= 9999999)
            {
                ImIdBusError.Visibility = Visibility.Hidden;
                ImIdBusOk.Visibility = Visibility.Visible;
                tBMileage.IsEnabled = true;
            }
            else
            {
                ImIdBusError.Visibility = Visibility.Visible;
                ImIdBusOk.Visibility = Visibility.Hidden;
                tBMileage.IsEnabled = false;
            }
        }

        private void tBMileage_TextChanged(object sender, RoutedEventArgs e)
        {
            if (tBMileage.Text.Length == 0 || tBMileage.IsEnabled == false) return;
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
            if (dPDateLastCare.SelectedDate == null || dPDateLastCare.IsEnabled == false) return;
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
            if (tBMileageLastCare.Text.Length == 0 || tBMileageLastCare.IsEnabled == false) return;
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

        private void rbNew_Click(object sender, RoutedEventArgs e)
        {
            tbMileage.Visibility = Visibility.Hidden;
            tBMileage.Visibility = Visibility.Hidden;
            tbMileageLastCare.Visibility = Visibility.Hidden;
            tBMileageLastCare.Visibility = Visibility.Hidden;
            tbDateLastCare.Visibility = Visibility.Hidden;
            dPDateLastCare.Visibility = Visibility.Hidden;
            ImDateLastCareError.Visibility = Visibility.Hidden;
            ImDateLastCareOk.Visibility = Visibility.Hidden;
            ImMileageLastCareError.Visibility = Visibility.Hidden;
            ImMileageLastCareOk.Visibility = Visibility.Hidden;
            ImMileageOk.Visibility = Visibility.Hidden;
            ImMileageError.Visibility = Visibility.Hidden;
        }

        private void rbOld_Click(object sender, RoutedEventArgs e)
        {
            tbMileage.Visibility = Visibility.Visible;
            tBMileage.Visibility = Visibility.Visible;
            tbMileageLastCare.Visibility = Visibility.Visible;
            tBMileageLastCare.Visibility = Visibility.Visible;
            tbDateLastCare.Visibility = Visibility.Visible;
            dPDateLastCare.Visibility = Visibility.Visible;
            dPDateLastCare_CalendarClosed(sender, e);
            tBMileage_TextChanged(sender, e);
            tBMileageLastCare_TextChanged(sender, e);
        }
    }
}
