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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using BO;
using BlApi;
using System.Collections.ObjectModel;
/// <summary>
/// need to do PO!!!!!
/// </summary>
namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL ibl = BlFactory.GetBL("1");
        ObservableCollection<PO.BusStop> Stops = new ObservableCollection<PO.BusStop>();
        ObservableCollection<PO.Bus> buses = new ObservableCollection<PO.Bus>();
        ObservableCollection<PO.Line> Lines = new ObservableCollection<PO.Line>();
        ObservableCollection<BO.Driver> drivers = new ObservableCollection<BO.Driver>();

        public MainWindow()
        {
            InitializeComponent();

            ListViewStations.DataContext = Stops;
            new Thread(() =>
            {
                foreach (var item in ibl.GetBusStops())
                {
                    this.Dispatcher.Invoke(() => Stops.Add(new PO.BusStop()));
                    Cloning.DeepCopyTo(item, Stops[Stops.Count - 1]);
                }
            }).Start();
            foreach (var item in ibl.GetBuses())
            {
                buses.Add(new PO.Bus());
                Cloning.DeepCopyTo(item, buses[buses.Count - 1]);
            }
            ListViewBuses.DataContext = buses;

            foreach (var item in ibl.GetLines())
            {
                Lines.Add(new PO.Line());
                Cloning.DeepCopyTo(item, Lines[Lines.Count - 1]);
            }
            ListViewLines.DataContext = Lines;
            foreach (var item in ibl.GetDrivers())
            {
                drivers.Add(new BO.Driver());
                Cloning.DeepCopyTo(item, drivers[drivers.Count - 1]);
            }
            ListViewDrivers.DataContext = drivers;
        }

        private void bLogIn_Click(object sender, RoutedEventArgs e)
        {

            mainGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    logInGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void bSignup_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    SignUpGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void bGuestMode_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                    Application.Current.MainWindow.Height = 640;
                    Application.Current.MainWindow.Width = 850;
                    Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    Application.Current.MainWindow.Top = 100;
                    Application.Current.MainWindow.Left = 200;
                    guestModeGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }
        private void blogInBack_Click(object sender, RoutedEventArgs e)
        {
            logInGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    mainGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void SignUpPart2Back_Click(object sender, RoutedEventArgs e)
        {
            signUpGridPart2.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    SignUpGrid.Visibility = Visibility.Visible;
                });

            }).Start();

        }

        private void SignUpBack_Click(object sender, RoutedEventArgs e)
        {
            SignUpGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    mainGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void signUpNext_Click(object sender, RoutedEventArgs e)
        {
            bool returnback = false;
            if (SutbFirstName.Text.Length < 1)
            {
                SutbFirstName.BorderBrush = Brushes.OrangeRed;
                returnback = true;
            }
            else SutbFirstName.BorderBrush = Brushes.Transparent;
            if (SutbLastName.Text.Length < 1)
            {
                SutbLastName.BorderBrush = Brushes.OrangeRed;
                returnback = true;
            }
            else SutbLastName.BorderBrush = Brushes.Transparent;
            if (SutbPhoneNumber.Text.Length != 10 || !int.TryParse(SutbPhoneNumber.Text, out int i))
            {
                SutbPhoneNumber.BorderBrush = Brushes.OrangeRed;
                returnback = true;
            }
            else SutbPhoneNumber.BorderBrush = Brushes.Transparent;
            if (returnback)
                return;

            SignUpGrid.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    signUpGridPart2.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {


            signUpGridPart2.Visibility = Visibility.Hidden;

            new Thread(() =>
            {
                this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                Thread.Sleep(1000);
                this.Dispatcher.Invoke(() =>
                {
                    loudGrid.Visibility = Visibility.Hidden;
                    Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                    Application.Current.MainWindow.Height = 640;
                    Application.Current.MainWindow.Width = 850;
                    Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    Application.Current.MainWindow.Top = 100;
                    Application.Current.MainWindow.Left = 200;
                    userGrid.Visibility = Visibility.Visible;
                });

            }).Start();
        }

        private void forgetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            forgetPasswordWindow forgetPasswordWindow = new forgetPasswordWindow();
            forgetPasswordWindow.Show();
        }

        private void blogInEnter_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text.Length == 0 || tbpassword.Password.Length == 0)
                return;
            User user = ibl.GetUser(tbUserName.Text, tbpassword.Password);
            if (user != null)
            {
                if (user.Authorization == Authorizations.User)
                    return; // must to fix!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
                if (user.Authorization == Authorizations.Admin)
                {
                    logInGrid.Visibility = Visibility.Hidden;
                    new Thread(() =>
                    {
                        this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

                        Thread.Sleep(500);
                        this.Dispatcher.Invoke(() =>
                        {
                            loudGrid.Visibility = Visibility.Hidden;
                            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
                            Application.Current.MainWindow.Height = 640;
                            Application.Current.MainWindow.Width = 850;
                            Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            Application.Current.MainWindow.Top = 100;
                            Application.Current.MainWindow.Left = 200;
                            accountAdmin.ToolTip = user.FirstName;
                            adminGrid.Visibility = Visibility.Visible;
                        });

                    }).Start();
                }

            }
            if (user == null)
            {
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(() => { worngUserNameOrPassordTextBlock.Visibility = Visibility.Visible; });
                    Thread.Sleep(5000);
                    this.Dispatcher.Invoke(() => { worngUserNameOrPassordTextBlock.Visibility = Visibility.Hidden; });
                }).Start();
            }
        }

        private void ListViewLines_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ListViewLines.SelectedItem is PO.Line)
            {
                PO.Line line = (PO.Line)ListViewLines.SelectedItem;
                ListViewStopsOfLine.DataContext = line.StopsInLine;
                ListViewStopsOfLine.Visibility = Visibility.Visible;
            }
            return;
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Still under construction", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Still under construction", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ChangeLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Still under construction", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            wAddBus addBus = new wAddBus();
            addBus.ShowDialog();

        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            wDelbus delbus = new wDelbus();
            delbus.ShowDialog();
            try
            {
                ibl.DeleteBus((int)delbus.IdDelbus);
                buses.Remove(buses.ToList().Find((Bus) => Bus.Id == delbus.IdDelbus));
            }
            catch (DeleteException ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeBus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBus_Click(object sender, RoutedEventArgs e)
        {

            wSearchBus searchBus = new wSearchBus();
            searchBus.Show();
            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ListViewBuses.SelectedItem = buses.ToList().Find((Bus) => Bus.Id == searchBus.busID);
                        ListViewBuses.ScrollIntoView(ListViewBuses.SelectedItem);

                        if (!searchBus.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                }
            }).Start();

        }

        private void AddStopLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(ibl);
            addStopLine.DataContext = ListViewLines.SelectedItem;
            addStopLine.ShowDialog();
        }

        private void SearchDriver_Click(object sender, RoutedEventArgs e)
        {

            uint id = 123456789;
            ListViewDrivers.SelectedItem = drivers.ToList().Find((Driver) => Driver.Id == id);
            ListViewDrivers.ScrollIntoView(ListViewDrivers.SelectedItem);

        }

        private void SearchStop_Click(object sender, RoutedEventArgs e)
        {
            wSearchStop searchStop = new wSearchStop();
            searchStop.Show();

            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ListViewStations.SelectedItem = Stops.ToList().Find((BusStop) => BusStop.Code == searchStop.CodeStop);
                        ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);

                        if (!searchStop.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                }
            }).Start();

        }

        private void SearchLine_Click(object sender, RoutedEventArgs e)
        {
            wSearchLine searchLine = new wSearchLine();
            searchLine.Show();
            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        ListViewLines.SelectedItem = Lines.ToList().Find((Line) => Line.NumLine == searchLine.numLine);
                        ListViewLines.ScrollIntoView(ListViewLines.SelectedItem);

                        if (!searchLine.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                }
            }).Start();
        }
    }
}
