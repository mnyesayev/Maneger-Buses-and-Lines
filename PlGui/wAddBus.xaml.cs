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
    /// Interaction logic for wAddBus.xaml
    /// </summary>
    public partial class wAddBus : Window
    {
        IBL bl;
        PO.Lists Lists;
        public wAddBus(IBL bl,PO.Lists lists)
        {
            InitializeComponent();
            this.bl = bl;
            Lists = lists;
        }
        private BO.Bus newBus = new BO.Bus();
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
                try
                {
                    BO.Bus bus;
                    if (rbOld.IsChecked == true)
                    {
                        bus = bl.AddBus(newBus);
                    }
                    else
                    {
                        bus = bl.AddBus(newBus,true);
                    }
                    var temp = new PO.Bus();
                    bus.DeepCopyTo(temp);
                    Lists.Buses.Add(temp);
                }
                catch (BO.AddException ex)
                {
                    MessageBox.Show(ex.Message, "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                this.Close();
            }
        }

        private void dPDRA_calendrer_closed(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (e == null) return;
            if (!DateTime.TryParse(dPDRA.Text, out DateTime DRA))
                return;
            newBus.DateRoadAscent = DRA;
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


        private void tBidBus_TextChanged(object sender, RoutedEventArgs e)
        {
            if (tBIdBus.Text.Length == 0 || tBIdBus.IsEnabled == false) return;
            if (sender == null) return;
            if (e == null) return;
            if (uint.TryParse(tBIdBus.Text, out uint id))
            {   
                newBus.Id = id;
                if (bl.CheckIdBus(newBus))
                {
                    ImIdBusError.Visibility = Visibility.Hidden;
                    ImIdBusOk.Visibility = Visibility.Visible;
                    tBMileage.IsEnabled = true;
                }
                else
                {
                    ImIdBusError.Visibility = Visibility.Visible;
                    ImIdBusError.ToolTip = "Date of ascent to the road does not match the license plate";
                    ImIdBusOk.Visibility = Visibility.Hidden;
                    tBMileage.IsEnabled = false;
                }
            }
           
        }

        private void tBMileage_TextChanged(object sender, RoutedEventArgs e)
        {
            if (tBMileage.IsEnabled == false) return;
            if (ImMileageOk.Visibility != Visibility.Visible && tBMileage.Text.Length == 0) return;
            if (sender == null) return;
            if (e == null) return;
            if (uint.TryParse(tBMileage.Text, out uint mileage))
            {
                newBus.Mileage = mileage;
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
                newBus.LastCare = DateTime.Parse(dPDateLastCare.Text);
                ImDateLastCareError.Visibility = Visibility.Hidden;
                ImDateLastCareOk.Visibility = Visibility.Visible;
                tBMileageLastCare.IsEnabled = true;
            }
            else
            {
                ImDateLastCareError.Visibility = Visibility.Visible;
                if (dPDateLastCare.SelectedDate <= dPDRA.SelectedDate)
                    ImDateLastCareError.ToolTip = "Date of last treatment before the date of ascent to the road?";
                if (dPDateLastCare.SelectedDate >= DateTime.Now)
                    ImDateLastCareError.ToolTip = "Really? Last treatment date is a future date?";
                ImDateLastCareOk.Visibility = Visibility.Hidden;
                tBMileageLastCare.IsEnabled = false;
            }
        }

        private void tBMileageLastCare_TextChanged(object sender, EventArgs e)
        {
            if ( tBMileageLastCare.IsEnabled == false) return;
            if (ImMileageLastCareOk.Visibility != Visibility.Visible && tBMileageLastCare.Text.Length == 0)
                return;
            if (sender == null) return;
            if (e == null) return;
            if (uint.TryParse(tBMileageLastCare.Text, out uint mileageLastCare) && newBus.Mileage >= mileageLastCare)
            {
                newBus.LastCareMileage = mileageLastCare;
                ImMileageLastCareError.Visibility = Visibility.Hidden;
                ImMileageLastCareOk.Visibility = Visibility.Visible;
            }
            else
            {
                ImMileageLastCareError.Visibility = Visibility.Visible;
                if (newBus.Mileage <= mileageLastCare)
                    ImMileageLastCareError.ToolTip = "Really? Mileage in the last treatment is greater than the mileage now?";

                ImMileageLastCareOk.Visibility = Visibility.Hidden;
            }
        }

        private void tBIdBus_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void tBMileage_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void tBMileageLastCare_preKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void DraPreKeyD(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void dPDateLastCare_PreKeyD(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void rbNew_PreMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (rbNew.IsChecked == true) return;
            if (newBus != default) newBus = new BO.Bus();
            tBIdBus.Text = "";
            tBIdBus.IsEnabled = false;
            tBMileage.Text = "";
            tBMileage.IsEnabled = false;
            tBMileageLastCare.Text = "";
            tBMileageLastCare.IsEnabled = false;
            dPDateLastCare.Text = "";
            dPDateLastCare.IsEnabled = false;
            dPDRA.Text = "";
            tbMileage.Visibility = Visibility.Hidden;
            tBMileage.Visibility = Visibility.Hidden;
            tbMileageLastCare.Visibility = Visibility.Hidden;
            tBMileageLastCare.Visibility = Visibility.Hidden;
            tbDateLastCare.Visibility = Visibility.Hidden;
            dPDateLastCare.Visibility = Visibility.Hidden;
            ImDRA.Visibility = Visibility.Hidden;
            ImIdBusError.Visibility = Visibility.Hidden;
            ImIdBusOk.Visibility = Visibility.Hidden;
            ImDateLastCareError.Visibility = Visibility.Hidden;
            ImDateLastCareOk.Visibility = Visibility.Hidden;
            ImMileageLastCareError.Visibility = Visibility.Hidden;
            ImMileageLastCareOk.Visibility = Visibility.Hidden;
            ImMileageOk.Visibility = Visibility.Hidden;
            ImMileageError.Visibility = Visibility.Hidden;
        }

        private void rbOld_PreMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (rbOld.IsChecked == true) return;
            if (newBus != default) newBus = new BO.Bus();
            tBIdBus.Text = "";
            tBIdBus.IsEnabled = false;
            tBMileage.Text = "";
            tBMileage.IsEnabled = false;
            tBMileageLastCare.Text = "";
            tBMileageLastCare.IsEnabled = false;
            dPDateLastCare.Text = "";
            dPDateLastCare.IsEnabled = false;
            dPDRA.Text = "";
            tbMileage.Visibility = Visibility.Visible;
            tBMileage.Visibility = Visibility.Visible;
            tbMileageLastCare.Visibility = Visibility.Visible;
            tBMileageLastCare.Visibility = Visibility.Visible;
            tbDateLastCare.Visibility = Visibility.Visible;
            dPDateLastCare.Visibility = Visibility.Visible;
            ImDRA.Visibility = Visibility.Hidden;
            ImIdBusError.Visibility = Visibility.Hidden;
            ImIdBusOk.Visibility = Visibility.Hidden;
            ImDateLastCareError.Visibility = Visibility.Hidden;
            ImDateLastCareOk.Visibility = Visibility.Hidden;
            ImMileageLastCareError.Visibility = Visibility.Hidden;
            ImMileageLastCareOk.Visibility = Visibility.Hidden;
            ImMileageOk.Visibility = Visibility.Hidden;
            ImMileageError.Visibility = Visibility.Hidden;
        }
    }
}
