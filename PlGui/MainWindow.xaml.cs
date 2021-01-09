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
        public IBL bl = BlFactory.GetBL();
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
                foreach (var item in bl.GetBusStops())
                {
                    this.Dispatcher.Invoke(() => Stops.Add(new PO.BusStop()));
                    Cloning.DeepCopyTo(item, Stops[Stops.Count - 1]);
                }
            }).Start();
            foreach (var item in bl.GetBuses())
            {
                buses.Add(new PO.Bus());
                Cloning.DeepCopyTo(item, buses[buses.Count - 1]);
            }
            ListViewBuses.DataContext = buses;

            foreach (var item in bl.GetLines())
            {
                Lines.Add(new PO.Line());
                Cloning.DeepCopyTo(item, Lines[Lines.Count - 1]);
            }
            ListViewLines.DataContext = Lines;
            foreach (var item in bl.GetDrivers())
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
            User user = bl.GetUser(tbUserName.Text, tbpassword.Password);
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
                            Application.Current.MainWindow.WindowState = WindowState.Maximized;
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
                AddStopLine.Visibility = Visibility.Visible;
            }
            return;
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            wAddLine addLine = new wAddLine(bl, Lines);
            addLine.ShowDialog();
          //if ()
        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            PO.Line l = (PO.Line)(sender as Button).DataContext;

            if (MessageBox.Show($"Are you sure to delete Line {l.NumLine}?"
                , "Delete Line", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            if (bl.DeleteLine(l.IdLine))
            {
                Lines.Remove(l);
                ListViewStopsOfLine.Visibility = Visibility.Hidden;
                AddStopLine.Visibility = Visibility.Hidden;
                new Thread(() =>
                {
                    var tempLST = from stopLine in l.StopsInLine
                                  select stopLine.CodeStop;
                    foreach (var item in tempLST)
                    {
                        var index = Stops.ToList().FindIndex((BusStop) => BusStop.Code == item);
                        var upStop = bl.GetStop(item);
                        var temp = new PO.BusStop();
                        Cloning.DeepCopyTo(upStop, temp);
                        Stops[index].LinesPassInStop = temp.LinesPassInStop;
                    }
                }).Start();
            }
        }

        private void ChangeLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Still under construction", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            wAddBus addBus = new wAddBus();
            Bus newBus = null;
            addBus.ShowDialog();
            try
            {
                newBus = bl.AddBus(addBus.NewBus);
            }
            catch (AddException ex)
            {
                MessageBox.Show(ex.Message, "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var temp = new PO.Bus();
            Cloning.DeepCopyTo(newBus, temp);
            buses.Add(temp);
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
                bl.DeleteBus((int)delbus.IdDelbus);
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
                        SearchBus.IsEnabled = false;
                        ListViewBuses.SelectedItem = buses.ToList().Find((Bus) => Bus.Id == searchBus.busID);
                        ListViewBuses.ScrollIntoView(ListViewBuses.SelectedItem);
                        if (!searchBus.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() => SearchBus.IsEnabled = true);
                }
            }).Start();

        }

        private void AddStopLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lines, ListViewStopsOfLine)
            {
                DataContext = ListViewLines.SelectedItem
            };
            addStopLine.ShowDialog();
            if (addStopLine.IsSuccessed)
            {
                var indexStop = Stops.ToList().FindIndex((BusStop) => BusStop.Code == int.Parse(addStopLine.tBCode.Text));
                var upstop = bl.GetStop(int.Parse(addStopLine.tBCode.Text));
                PO.BusStop temp = new PO.BusStop();
                Cloning.DeepCopyTo(upstop, temp);
                Stops[indexStop].LinesPassInStop = temp.LinesPassInStop;
            }
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
                    this.Dispatcher.Invoke(() => SearchStop.IsEnabled = false);
                    if (searchStop.itsNumber == true)
                        this.Dispatcher.Invoke(() =>
                        {
                            ListViewStations.SelectedItem = Stops.ToList().Find((BusStop) => BusStop.Code == searchStop.CodeStop);
                            ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);

                            if (!searchStop.IsVisible)
                                flag = false;
                        });
                    else
                        this.Dispatcher.Invoke(() =>
                        {
                            ListViewStations.SelectedItem = Stops.ToList().Find((BusStop) => BusStop.Name == searchStop.NameStop);
                            ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);

                            if (!searchStop.IsVisible)
                                flag = false;
                        });
                    Thread.Sleep(500);
                }
                this.Dispatcher.Invoke(() => SearchStop.IsEnabled = true);
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
                        SearchLine.IsEnabled = false;
                        ListViewLines.SelectedItem = Lines.ToList().Find((Line) => Line.NumLine == searchLine.numLine);
                        ListViewLines.ScrollIntoView(ListViewLines.SelectedItem);

                        if (!searchLine.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                }
                this.Dispatcher.Invoke(() => SearchLine.IsEnabled = true);
            }).Start();
        }

        private void ListViewBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewBuses.SelectedItem is PO.Bus)
            {
                wBusInfo busInfo = new wBusInfo(bl);
                busInfo.DataContext = ListViewBuses.SelectedItem as PO.Bus;
                if ((ListViewBuses.SelectedItem as PO.Bus).Fuel < 1200)
                    busInfo.bRefuel.IsEnabled = true;
                busInfo.Show();
            }
        }

        private void DeleteStopLine_Click(object sender, RoutedEventArgs e)
        {
            var StopLine = (PO.StopLine)(sender as Button).DataContext;
            BO.Line upline;
            try
            {
                upline = bl.DeleteStopLine(StopLine.IdLine, StopLine.CodeStop, StopLine.NumStopInLine);
                if (upline == null)
                    return;
            }
            catch (DeleteException ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var upstop = bl.GetStop(StopLine.CodeStop);
            int indexLine = Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            int indexStop = Stops.ToList().FindIndex((BusStop) => BusStop.Code == StopLine.CodeStop);
            var temp = new PO.Line();
            var temp2 = new PO.BusStop();
            Cloning.DeepCopyTo(upline, temp);
            Cloning.DeepCopyTo(upstop, temp2);
            Lines[indexLine].StopsInLine = temp.StopsInLine;
            Lines[indexLine].NameFirstLineStop = temp.NameFirstLineStop;
            Lines[indexLine].NameLastLineStop = temp.NameLastLineStop;
            ListViewStopsOfLine.DataContext = Lines[indexLine].StopsInLine;
            Stops[indexStop].LinesPassInStop = temp2.LinesPassInStop;
        }

        private void tbUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                tbpassword.Focus();
        }

        private void tbpassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                blogInEnter_Click(sender, null);
        }

        private void EditDAT_Click(object sender, RoutedEventArgs e)
        {
            PO.StopLine sl = (PO.StopLine)(sender as Button).DataContext;
            wEditSuccessiveStations wEdit = new wEditSuccessiveStations(bl);
            wEdit.tbcode1.Text = sl.CodeStop.ToString();
            wEdit.tbcode2.Text = sl.NextStop.ToString();
            wEdit.TimePicker.Text = sl.AvregeDriveTimeToNext.ToString(@"hh\:mm\:ss");
            wEdit.TBKmDis.Text = sl.DistanceToNext.ToString();
            wEdit.ShowDialog();
            if (wEdit.IsSave)
            {
                foreach (var item in Lines)
                {
                    var index = item.StopsInLine.ToList().FindIndex((StopLine) =>
                      { return StopLine.CodeStop == sl.CodeStop && StopLine.NextStop == sl.NextStop; });
                    if (index != -1)
                    {
                        var upstopLine = bl.GetStopInLine(sl.CodeStop, item.IdLine);
                        var temp = new PO.StopLine();
                        upstopLine.DeepCopyTo(temp);
                        item.StopsInLine[index].DistanceToNext = temp.DistanceToNext;
                        item.StopsInLine[index].AvregeDriveTimeToNext = temp.AvregeDriveTimeToNext;
                    }
                }
            }
        }

        private void ListViewStations_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ListViewStations.SelectedItem is PO.BusStop)
            {
                PO.BusStop busStop = (PO.BusStop)ListViewStations.SelectedItem;
                if (busStop.LinesPassInStop.Count != 0)
                {
                    ListViewLinesInStop.DataContext = busStop.LinesPassInStop;
                    ListViewLinesInStop.Visibility = Visibility.Visible;
                }
                if (busStop.LinesPassInStop.Count == 0)
                {
                    ListViewLinesInStop.Visibility = Visibility.Hidden;
                }
                StringBuilder stringB = new StringBuilder("https://www.google.co.il/maps/place/");
                stringB.Append($"{busStop.Latitude},{busStop.Longitude}");
                try
                {
                    webStop.Address = stringB.ToString();
                }
                catch (Exception)
                {

                }
            }
            return;
        }
    }
}
