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
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
/// <summary>
/// need to do PO!!!!!
/// </summary>
namespace PLGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBL bl = BlFactory.GetBL();
        PO.Lists Lists = new PO.Lists();
        BackgroundWorker Simulator;
        BackgroundWorker SimulatorPanelStation;
        BackgroundWorker blGetStopsAndLines;
        BackgroundWorker blGetBuses;
        BackgroundWorker blGetDrivers;
        BO.User MyUser;
        int AdminCode = 123;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Lists;
            setSimulatorPanelStation();
            setSimulator();
            setBlGetStopsAndLines();
            setBlGetBuses();
            setBlGetDrivers();
            blGetStopsAndLines.RunWorkerAsync();
            blGetBuses.RunWorkerAsync();
            blGetDrivers.RunWorkerAsync();
        }


        private void setBlGetDrivers()
        {
            blGetDrivers = new BackgroundWorker();
            blGetDrivers.WorkerReportsProgress = true;
            blGetDrivers.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                foreach (var item in bl.GetDrivers())
                {
                    worker.ReportProgress(1, item);
                }
            };
            blGetDrivers.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                var temp = e.UserState as Driver;
                Lists.Drivers.Add(temp);
            };
        }

        private void setBlGetBuses()
        {
            blGetBuses = new BackgroundWorker();
            blGetBuses.WorkerReportsProgress = true;
            blGetBuses.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                foreach (var item in bl.GetBuses())
                {
                    var temp = new PO.Bus();
                    Cloning.DeepCopyTo(item, temp);
                    worker.ReportProgress(1, temp);
                }
            };
            blGetBuses.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
            {
                var temp = e.UserState as PO.Bus;
                Lists.Buses.Add(temp);
            };
        }

        private void setBlGetStopsAndLines()
        {
            blGetStopsAndLines = new BackgroundWorker();
            blGetStopsAndLines.WorkerReportsProgress = true;
            blGetStopsAndLines.DoWork += (object sender, DoWorkEventArgs e) =>
              {
                  BackgroundWorker worker = (BackgroundWorker)sender;
                  foreach (var item in bl.GetLines())
                  {
                      var temp = new PO.Line();
                      Cloning.DeepCopyTo(item, temp);
                      worker.ReportProgress(1, temp);
                  }
                  foreach (var item in bl.GetBusStops())
                  {
                      var temp = new PO.BusStop();
                      Cloning.DeepCopyTo(item, temp);
                      foreach (var lineOnStop in temp.LinesPassInStop)
                      {
                          var tempLine = Lists.Lines.First(l => l.IdLine == lineOnStop.IdLine);
                          tempLine.DeepCopyTo(lineOnStop);
                      }
                      worker.ReportProgress(30, temp);
                  }
              };
            blGetStopsAndLines.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
              {
                  if (e.ProgressPercentage == 1)
                  {
                      var temp = e.UserState as PO.Line;
                      Lists.Lines.Add(temp);
                  }
                  else
                  {
                      var temp = e.UserState as PO.BusStop;
                      Lists.Stops.Add(temp);
                  }
              };
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
            MessageBox.Show("Still under construction", "Guest Mode", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
            //mainGrid.Visibility = Visibility.Hidden;
            //new Thread(() =>
            //{
            //    this.Dispatcher.Invoke(() => { loudGrid.Visibility = Visibility.Visible; });

            //    Thread.Sleep(500);
            //    this.Dispatcher.Invoke(() =>
            //    {
            //        loudGrid.Visibility = Visibility.Hidden;
            //        Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            //        Application.Current.MainWindow.Height = 640;
            //        Application.Current.MainWindow.Width = 850;
            //        Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //        Application.Current.MainWindow.Top = 100;
            //        Application.Current.MainWindow.Left = 200;
            //        guestModeGrid.Visibility = Visibility.Visible;
            //    });

            //}).Start();
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
                    tbUserName.Text = "";
                    tbpassword.Password = "";
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
                    Su2tbUserName.Text = "";
                    Su2tbpassword.Password = "";
                    Su2tbpassword2.Password = "";
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
                    SutbFirstName.Text = "";
                    SutbFirstName.BorderBrush = Brushes.Transparent;
                    SutbLastName.Text = "";
                    SutbLastName.BorderBrush = Brushes.Transparent;
                    FutureDatePicker.Text = "";
                    FutureDatePicker.BorderBrush = Brushes.Transparent;
                    SutbPhoneNumber.Text = "";
                    SutbPhoneNumber.BorderBrush = Brushes.Transparent;
                    cbAdmin.IsChecked = false;
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
            if (!DateTime.TryParse(FutureDatePicker.Text, out DateTime tmp))
            {
                FutureDatePicker.BorderBrush = Brushes.OrangeRed;
                returnback = true;
            }
            else FutureDatePicker.BorderBrush = Brushes.Transparent;
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
            if (Su2tbpassword2.Password != Su2tbpassword.Password)
            {
                tbworngPassword.Visibility = Visibility.Visible;
                Su2tbpassword.Password = "";
                Su2tbpassword2.Password = "";
                new Thread(() =>
                {
                    Thread.Sleep(10000);
                    this.Dispatcher.Invoke(() => tbworngPassword.Visibility = Visibility.Hidden);
                }).Start();
                return;
            }
            User user = new User()
            {
                Authorization = (cbAdmin.IsChecked == true) ? Authorizations.Admin : Authorizations.User,
                FirstName = SutbFirstName.Text,
                LastName = SutbLastName.Text,
                UserName = Su2tbUserName.Text,
                Password = Su2tbpassword.Password,
                Birthday = DateTime.Parse(FutureDatePicker.Text),
                Phone = SutbPhoneNumber.Text
            };
            try
            {
                BO.User u = bl.AddUser(user);
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(() => { tbDone.Visibility = Visibility.Visible; });
                    Thread.Sleep(2000);
                    this.Dispatcher.Invoke(() =>
                    {
                        tbDone.Visibility = Visibility.Hidden;
                        signUpGridPart2.Visibility = Visibility.Hidden;
                        loudGrid.Visibility = Visibility.Visible;
                        SutbFirstName.Text = "";
                        SutbLastName.Text = "";
                        Su2tbUserName.Text = "";
                        Su2tbpassword.Password = "";
                        Su2tbpassword2.Password = "";
                        FutureDatePicker.Text = "";
                        SutbPhoneNumber.Text = "";
                        cbAdmin.IsChecked = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() =>
                    {
                        loudGrid.Visibility = Visibility.Hidden;
                        mainGrid.Visibility = Visibility.Visible;
                    });
                }).Start();

            }
            catch (AddException ex)
            {

                Su2tbUserName.Text = "";
                Su2tbpassword.Password = "";
                Su2tbpassword2.Password = "";
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        tbworng.Text = ex.Message;
                        tbworng.Visibility = Visibility.Visible;
                    });
                    Thread.Sleep(10000);
                    this.Dispatcher.Invoke(() =>
                    {
                        tbworng.Text = "";
                        tbworng.Visibility = Visibility.Hidden;
                    });
                }).Start();
                return;
            }
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
            MyUser = bl.GetUser(tbUserName.Text, tbpassword.Password);
            DateTime oldLogIn = MyUser.LogIn;
            BO.User updaedUser = new BO.User()
            {
                UserName = MyUser.UserName,
                Authorization = MyUser.Authorization,
                Birthday = MyUser.Birthday,
                FirstName = MyUser.FirstName,
                LastName = MyUser.LastName,
                Password = MyUser.Password,
                Phone = MyUser.Phone,
                LogIn =DateTime.Now
            };
            try
            {
                MyUser = bl.UpdateUser(updaedUser);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "User ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong here", "User ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            tbUserName.Text = "";
            tbpassword.Password = "";
            if (MyUser != null)
            {
                if (MyUser.Authorization == Authorizations.User)
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
                            accountUser.ToolTip = MyUser.FirstName;
                            userGrid.Visibility = Visibility.Visible;
                        });
                    }).Start();

                }
                if (MyUser.Authorization == Authorizations.Admin)
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
                            accountAdmin.ToolTip = MyUser.FirstName;
                            tbHello.Text = $"Hello {MyUser.FirstName} your last login was at {oldLogIn}";
                            adminGrid.Visibility = Visibility.Visible;
                        });

                    }).Start();
                }

            }
            if (MyUser == null)
            {
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(() => { worngUserNameOrPassordTextBlock.Visibility = Visibility.Visible; });
                    Thread.Sleep(5000);
                    this.Dispatcher.Invoke(() => { worngUserNameOrPassordTextBlock.Visibility = Visibility.Hidden; });
                }).Start();
            }
        }
        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        #region Lines
        private void ListViewLines_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ListViewLines.SelectedItem is PO.Line)
            {
                PO.Line line = (PO.Line)ListViewLines.SelectedItem;
                ListViewStopsOfLine.DataContext = line;
                ListViewFrequency.DataContext = line;
                ListViewFrequency.Visibility = Visibility.Visible;
                ListViewStopsOfLine.Visibility = Visibility.Visible;
                AddStopLine.Visibility = Visibility.Visible;
                moreInfoOnLine.DataContext = line;
            }
            if (ListViewLinesUser.SelectedItem is PO.Line)
            {
                PO.Line line = (PO.Line)ListViewLinesUser.SelectedItem;
                ListViewStopsOfLineUser.DataContext = line;
                ListViewFrequencyUser.DataContext = line;
                ListViewFrequencyUser.Visibility = Visibility.Visible;
                ListViewStopsOfLineUser.Visibility = Visibility.Visible;
                moreInfoOnLineUser.DataContext = line;
            }
            return;
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            wAddLine addLine = new wAddLine(bl, Lists);
            addLine.ShowDialog();
        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            PO.Line l = (PO.Line)(sender as Button).DataContext;

            if (MessageBox.Show($"Are you sure to delete Line {l.NumLine}?"
                , "Delete Line", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            if (bl.DeleteLine(l.IdLine))
            {
                Lists.Lines.Remove(l);
                ListViewStopsOfLine.Visibility = Visibility.Hidden;
                AddStopLine.Visibility = Visibility.Hidden;
                new Thread(() =>
                {
                    var tempLST = from stopLine in l.StopsInLine
                                  select stopLine.CodeStop;
                    foreach (var item in tempLST)
                    {
                        var indexS = Lists.Stops.ToList().FindIndex((BusStop) => BusStop.Code == item);
                        var i = Lists.Stops[indexS].LinesPassInStop.ToList().FindIndex(line => line.IdLine == l.IdLine);
                        Lists.Stops[indexS].LinesPassInStop.RemoveAt(i);
                    }
                }).Start();
            }
        }

        private void AddStopLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lists)
            {
                DataContext = ListViewLines.SelectedItem
            };
            addStopLine.ShowDialog();

        }

        private void DeleteStopLine_Click(object sender, RoutedEventArgs e)
        {
            var StopLine = (PO.StopLine)ListViewStopsOfLine.SelectedItem;
            var idLine = (ListViewStopsOfLine.DataContext as PO.Line).IdLine;
            BO.Line upline;
            try
            {
                upline = bl.DeleteStopLine(idLine, StopLine.CodeStop, StopLine.NumStopInLine);
                if (upline == null)
                    return;
            }
            catch (DeleteException ex)
            {
                MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int indexLine = Lists.Lines.ToList().FindIndex((Line) => Line.IdLine == upline.IdLine);
            var linePO = new PO.Line();
            Cloning.DeepCopyTo(upline, linePO);
            Lists.Lines[indexLine].StopsInLine = linePO.StopsInLine;
            Lists.Lines[indexLine].NameFirstLineStop = linePO.NameFirstLineStop;
            Lists.Lines[indexLine].NameLastLineStop = linePO.NameLastLineStop;
            //for old stop
            var indexdeleteStop = Lists.Stops.ToList().FindIndex((BusStop) => BusStop.Code == StopLine.CodeStop);
            var i = Lists.Stops[indexdeleteStop].LinesPassInStop.ToList().FindIndex(l => l.IdLine == idLine);
            Lists.Stops[indexdeleteStop].LinesPassInStop.RemoveAt(i);
        }

        private void EditDAT_Click(object sender, RoutedEventArgs e)
        {
            PO.StopLine sl = (PO.StopLine)ListViewStopsOfLine.SelectedItem;
            if (sl.NextStop == 0) return;

            wEditSuccessiveStations wEdit = new wEditSuccessiveStations(bl);
            wEdit.tbcode1.Text = sl.CodeStop.ToString();
            wEdit.tbcode2.Text = sl.NextStop.ToString();
            wEdit.tbName1.Text = sl.Name;
            wEdit.tbName2.Text = bl.GetNameStop(sl.NextStop);
            wEdit.TimePicker.Text = sl.AvregeDriveTimeToNext.ToString(@"hh\:mm\:ss");
            wEdit.TBKmDis.Text = sl.DistanceToNext.ToString();
            wEdit.ShowDialog();
            if (wEdit.IsSave)
            {
                foreach (var item in Lists.Lines)
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

        private void ListViewLines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListViewLines.SelectedItem is PO.Line)
            {
                wLineInfo lineInfo = new wLineInfo(bl, Lists);
                lineInfo.DataContext = ListViewLines.SelectedItem;
                lineInfo.ComboBoxLineInfo.DataContext = Lists.Lines;
                lineInfo.ComboBoxLineInfo.SelectedItem = lineInfo.DataContext;
                lineInfo.ShowDialog();
            }
            return;
        }

        private void bStop_lineInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is PO.LineOnStop)
            {
                wLineInfo lineInfo = new wLineInfo(bl, Lists);
                var l = (PO.LineOnStop)(sender as Button).DataContext;
                var i = Lists.Lines.ToList().FindIndex((line) => line.IdLine == l.IdLine);
                lineInfo.DataContext = Lists.Lines[i];
                lineInfo.ComboBoxLineInfo.DataContext = Lists.Lines;
                lineInfo.ComboBoxLineInfo.SelectedItem = lineInfo.DataContext;
                lineInfo.ShowDialog();
            }
            return;
        }

        private void addAfterStopToLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lists);
            addStopLine.DataContext = ListViewLines.SelectedItem;
            addStopLine.tBNewIndex.Text = (ListViewStopsOfLine.SelectedIndex + 2).ToString();
            addStopLine.ShowDialog();
        }

        private void addBeforeStopToLine_Click(object sender, RoutedEventArgs e)
        {
            var addStopLine = new addStopLine(bl, Lists);
            addStopLine.DataContext = ListViewLines.SelectedItem;
            addStopLine.tBNewIndex.Text = (ListViewStopsOfLine.SelectedIndex + 1).ToString();
            addStopLine.ShowDialog();
        }

        private void HideDistanceAndTime_Click(object sender, RoutedEventArgs e)
        {
            DriveTime.Width = 0;
            Distance.Width = 0;
        }

        private void ShowDistanceAndTime_Click(object sender, RoutedEventArgs e)
        {
            DriveTime.Width = 100;
            Distance.Width = 100;
        }
        #endregion

        #region busStops
        private void ListViewStations_SelctionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewStations.SelectedItem is PO.BusStop)
            {
                PO.BusStop busStop = (PO.BusStop)ListViewStations.SelectedItem;
                if (SimulatorPanelStation.IsBusy)
                {
                    SimulatorPanelStation.CancelAsync();
                }
                if (busStop.LinesPassInStop.Count != 0)
                {
                    ListViewLinesInStop.DataContext = busStop;
                    ListViewLinesInStop.Visibility = Visibility.Visible;
                    Lists.PanelStation = new List<LineTiming>();
                    ListViewPanel.ItemsSource = null;
                    if (Simulator.IsBusy)
                    {
                        SimulatorPanelStation.RunWorkerAsync(busStop);
                        ListViewPanel.Visibility = Visibility.Visible;
                    }
                }
                if (busStop.LinesPassInStop.Count == 0)
                {
                    ListViewLinesInStop.Visibility = Visibility.Hidden;
                }
                StringBuilder stringB = new StringBuilder("https://www.google.co.il/maps/place/");
                stringB.Append($"{busStop.Latitude},{busStop.Longitude}");
                webStop.Address = stringB.ToString();
            }
            if (ListViewStationsUser.SelectedItem is PO.BusStop)
            {
                PO.BusStop busStop = (PO.BusStop)ListViewStationsUser.SelectedItem;
                if (SimulatorPanelStation.IsBusy)
                {
                    SimulatorPanelStation.CancelAsync();
                }
                if (busStop.LinesPassInStop.Count != 0)
                {
                    ListViewLinesInStopUser.DataContext = busStop;
                    ListViewLinesInStopUser.Visibility = Visibility.Visible;
                    Lists.PanelStation = new List<LineTiming>();
                    ListViewPanel.ItemsSource = null;
                    if (Simulator.IsBusy)
                    {
                        SimulatorPanelStation.RunWorkerAsync(busStop);
                        ListViewPanelUser.Visibility = Visibility.Visible;
                    }
                }
                if (busStop.LinesPassInStop.Count == 0)
                {
                    ListViewLinesInStopUser.Visibility = Visibility.Hidden;
                }
                StringBuilder stringB = new StringBuilder("https://www.google.co.il/maps/place/");
                stringB.Append($"{busStop.Latitude},{busStop.Longitude}");
                webStopUser.Address = stringB.ToString();
            }
            return;
        }

        private void AddStop_Click(object sender, RoutedEventArgs e)
        {
            wAddStop addStop = new wAddStop(bl, Lists);
            addStop.ShowDialog();
        }

        #endregion

        #region Buses
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

        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            wDelbus delbus = new wDelbus(bl, Lists);
            delbus.ShowDialog();

        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            wAddBus addBus = new wAddBus(bl, Lists);
            addBus.ShowDialog();
        }
        #endregion

        #region search
        private void SearchBus_Click(object sender, RoutedEventArgs e)
        {

            wSearchBus searchBus = new wSearchBus();
            searchBus.TbBusId.Focus();
            searchBus.Show();
            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SearchBus.IsEnabled = false;
                        ListViewBuses.SelectedItem = Lists.Buses.ToList().Find((Bus) => Bus.Id == searchBus.busID);
                        ListViewBuses.ScrollIntoView(ListViewBuses.SelectedItem);
                        if (!searchBus.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() => SearchBus.IsEnabled = true);
                }
            }).Start();

        }

        private void SearchDriver_Click(object sender, RoutedEventArgs e)
        {

            wSearchDriver searchDriverWindow = new wSearchDriver();
            searchDriverWindow.TbDriverId.Focus();
            searchDriverWindow.Show();
            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SearchDriver.IsEnabled = false;
                        ListViewDrivers.SelectedItem = Lists.Drivers.ToList().Find((Driver) => Driver.Id == searchDriverWindow.DriverID);
                        ListViewDrivers.ScrollIntoView(ListViewDrivers.SelectedItem);
                        if (!searchDriverWindow.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() => SearchDriver.IsEnabled = true);
                }
            }).Start();
        }

        private void SearchStop_Click(object sender, RoutedEventArgs e)
        {
            wSearchStop searchStop = new wSearchStop();
            searchStop.TbStopCode.Focus();
            var suggestionName = from Stop in Lists.Stops
                                 select Stop.Name;
            searchStop.AutoSuggestionList = suggestionName.ToList();
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
                            var item = Lists.Stops.ToList().Find((BusStop) => BusStop.Code == searchStop.CodeStop);
                            if (adminGrid.Visibility == Visibility.Visible)
                            {
                                ListViewStations.SelectedItem = item;
                                ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);
                            }
                            if (userGrid.Visibility == Visibility.Visible)
                            {
                                ListViewStationsUser.SelectedItem = item;
                                ListViewStationsUser.ScrollIntoView(ListViewStationsUser.SelectedItem);
                            }
                            if (!searchStop.IsVisible)
                                flag = false;
                        });
                    else
                        this.Dispatcher.Invoke(() =>
                        {
                            var item = Lists.Stops.ToList().Find((BusStop) => BusStop.Name == searchStop.NameStop);
                            if (adminGrid.Visibility == Visibility.Visible)
                            {
                                ListViewStations.SelectedItem = item;
                                ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);
                            }
                            if (userGrid.Visibility == Visibility.Visible)
                            {
                                ListViewStationsUser.SelectedItem = item;
                                ListViewStationsUser.ScrollIntoView(ListViewStationsUser.SelectedItem);
                            }

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
            searchLine.TbLineCode.Focus();
            searchLine.Show();
            new Thread(() =>
            {

                bool flag = true;
                while (flag)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SearchLine.IsEnabled = false;
                        var item = Lists.Lines.ToList().Find((Line) => Line.NumLine == searchLine.numLine);
                        if (adminGrid.Visibility == Visibility.Visible)
                        {
                            ListViewLines.SelectedItem = item;
                            ListViewLines.ScrollIntoView(ListViewLines.SelectedItem);
                        }
                        if (userGrid.Visibility == Visibility.Visible)
                        {
                            ListViewLinesUser.SelectedItem = item;
                            ListViewLinesUser.ScrollIntoView(ListViewLinesUser.SelectedItem);
                        }
                        if (!searchLine.IsVisible)
                            flag = false;
                    });
                    Thread.Sleep(500);
                }
                this.Dispatcher.Invoke(() => SearchLine.IsEnabled = true);
            }).Start();
        }
        #endregion


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



        private void bClock_Click(object sender, RoutedEventArgs e)
        {
            if (adminGrid.Visibility == Visibility.Visible)
            {
                if (programClock == null || clockSpeed.Text.Length == 0) return;
                if (!TimeSpan.TryParse(programClock.Text, out TimeSpan time))
                    return;
                if (bClock.Content.ToString() == "Start")
                {
                    bClock.Content = "Stop";
                    Simulator.RunWorkerAsync(new { Time = time, Speed = int.Parse(clockSpeed.Text) });
                    programClock.IsEnabled = false;
                    clockSpeed.IsEnabled = false;
                }
                else
                {
                    if (Simulator != null)
                    {
                        Simulator.CancelAsync();//stop BackgroundWorker
                    }

                }
            }
            if (userGrid.Visibility == Visibility.Visible)
            {
                if (programClockUser == null || clockSpeedUser.Text.Length == 0) return;
                if (!TimeSpan.TryParse(programClockUser.Text, out TimeSpan time))
                    return;
                if (bClockUser.Content.ToString() == "Start")
                {
                    bClockUser.Content = "Stop";
                    Simulator.RunWorkerAsync(new { Time = time, Speed = int.Parse(clockSpeedUser.Text) });
                    programClockUser.IsEnabled = false;
                    clockSpeedUser.IsEnabled = false;
                }
                else
                {
                    if (Simulator != null)
                    {
                        Simulator.CancelAsync();//stop BackgroundWorker
                    }

                }
            }
        }
        private void setSimulator()
        {
            Simulator = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            Simulator.DoWork += (object sender, DoWorkEventArgs e) =>
             {
                 dynamic anonimicType = e.Argument;

                 bl.StartSimulator((TimeSpan)anonimicType.Time, (int)anonimicType.Speed, t => Simulator.ReportProgress(1, t));
                 while (!Simulator.CancellationPending)
                     Thread.Sleep(1000);
             };
            Simulator.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
              {
                  TimeSpan t = (TimeSpan)e.UserState;
                  if (adminGrid.Visibility == Visibility.Visible)
                      programClock.Text = t.ToString(@"hh\:mm\:ss");
                  if (userGrid.Visibility == Visibility.Visible)
                      programClockUser.Text = t.ToString(@"hh\:mm\:ss");
              };
            Simulator.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                bClock.Content = "Start";
                bClockUser.Content = "Start";
                ListViewPanel.Visibility = Visibility.Hidden;
                ListViewPanelUser.Visibility = Visibility.Hidden;
                bl.StopSimulator();
                programClock.IsEnabled = true;
                programClockUser.IsEnabled = true;
                clockSpeed.IsEnabled = true;
                clockSpeedUser.IsEnabled = true;
                programClock.Text = "";
                programClockUser.Text = "";
                clockSpeed.Text = "";
                clockSpeedUser.Text = "";
            };
        }

        private void setSimulatorPanelStation()
        {
            SimulatorPanelStation = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            SimulatorPanelStation.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                bl.SetStationPanel((args.Argument as PO.BusStop).Code, lineTiming => SimulatorPanelStation.ReportProgress(1, lineTiming));
                while (!SimulatorPanelStation.CancellationPending)
                {
                    Thread.Sleep(1000);
                }
                args.Result = args.Argument;
            };
            SimulatorPanelStation.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
            {
                var lineTiming = (LineTiming)args.UserState;
                var i = Lists.PanelStation.IndexOf(lineTiming);
                if (i == -1)
                {
                    if (lineTiming.ArriveTime == TimeSpan.Zero)
                        return;
                    Lists.PanelStation.Add(lineTiming);
                    Lists.PanelStation.Sort((lt1, lt2) => (int)(lt1.ArriveTime - lt2.ArriveTime).TotalMilliseconds);
                }
                else
                {
                    if (lineTiming.ArriveTime == TimeSpan.Zero)
                        Lists.PanelStation.Remove(lineTiming);
                    else
                        Lists.PanelStation.Sort((lt1, lt2) => (int)(lt1.ArriveTime - lt2.ArriveTime).TotalMilliseconds);
                }
                int size = (Lists.PanelStation.Count < 5) ? Lists.PanelStation.Count : 5;
                if (adminGrid.Visibility == Visibility.Visible)
                {
                    ListViewPanel.ItemsSource = null;
                    ListViewPanel.ItemsSource = Lists.PanelStation.GetRange(0, size);
                }
                if (userGrid.Visibility == Visibility.Visible)
                {
                    ListViewPanelUser.ItemsSource = null;
                    ListViewPanelUser.ItemsSource = Lists.PanelStation.GetRange(0, size);
                }
            };
            SimulatorPanelStation.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
              {
                  if (args.Result is PO.BusStop)
                  {
                      Lists.PanelStation = new List<LineTiming>();
                      ListViewPanel.ItemsSource = null;
                      ListViewPanelUser.ItemsSource = null;
                      ListViewPanel.Visibility = Visibility.Hidden;
                      ListViewPanelUser.Visibility = Visibility.Hidden;
                      bl.SetStationPanel(-1);//Stop tracking
                  }
              };
        }

        private void clockSpeed_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;

        }

        private void accountAdmin_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow accuntW = new AccountWindow(bl);
            accuntW.DataContext = MyUser;
            accuntW.tbEditPhone.Text = MyUser.Phone;
            accuntW.tpEditBirthday.SelectedDate = MyUser.Birthday;
            accuntW.tbEditFirstName.Text = MyUser.FirstName;
            accuntW.tbEditLastName.Text = MyUser.LastName;
            if (userGrid.Visibility == Visibility.Visible)
                accuntW.accountIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountCircle;
            if(adminGrid.Visibility==Visibility.Visible)
                accuntW.accountIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AccountStar;
            accuntW.Show();
            new Thread(() =>
            {
                while (accuntW.IsVisible == true)
                {
                    Thread.Sleep(1000);
                    if (accuntW.GOBack == true)
                    {
                        if (Simulator.IsBusy)
                        {
                            Simulator.CancelAsync();
                        }
                        this.Dispatcher.Invoke(() =>
                        {
                            Application.Current.MainWindow.Height = 350;
                            Application.Current.MainWindow.Width = 500;
                            Application.Current.MainWindow.WindowState = WindowState.Normal;
                            MainWindow1.ResizeMode = ResizeMode.CanMinimize;
                            mainGrid.Visibility = Visibility.Visible;
                            userGrid.Visibility = Visibility.Hidden;
                            adminGrid.Visibility = Visibility.Hidden;
                            loudGrid.Visibility = Visibility.Hidden;
                            accuntW.Close();
                        });
                    }
                    if (accuntW.GetUpdated == true)
                    {
                        MyUser = accuntW.user1;
                        this.Dispatcher.Invoke(() =>
                        {
                            if (userGrid.Visibility == Visibility.Visible)
                                accountUser.ToolTip = MyUser.FirstName;
                            else
                                accountAdmin.ToolTip = MyUser.FirstName;
                        });
                    }
                }
            }).Start();

        }

        private void FutureDatePicker_PreKeyD(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void SutbPhoneNumber_PreKeyD(object sender, KeyEventArgs e)
        {
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }

        private void moreInfoOnLine_PreKeyU(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var upLine = ListViewLines.SelectedItem as PO.Line;
                if(bl.UpdateLine(upLine.IdLine, upLine.NumLine, upLine.Area, moreInfoOnLine.Text))
                    moreInfoOnLine.IsEnabled = false;
            }
        }

        private void editMoreInfoOnLine_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewLines.SelectedItem is PO.Line)
                moreInfoOnLine.IsEnabled = true;
        }

        private void adminCode_PreKeyD(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Down || e.Key == Key.Tab)
            {
                if (adminCode.Text == AdminCode.ToString())
                    cbAdmin.IsEnabled = true;
            }
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }
    }
}
