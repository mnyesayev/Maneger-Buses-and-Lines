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
namespace PlGui
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
                    SutbLastName.Text = "";
                    FutureDatePicker.Text = "";
                    SutbPhoneNumber.Text = "";
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
                Authorization = Authorizations.User,
                FirstName = SutbFirstName.Text,
                LastName = SutbLastName.Text,
                UserName = Su2tbUserName.Text,
                Password = Su2tbpassword.Password,
                Active = true,
                Birthday =DateTime.Parse(FutureDatePicker.Text),
                Phone = SutbPhoneNumber.Text
            };
            BO.User u = bl.AddUser(user);
            if (u == null)
            {
                Su2tbUserName.Text = "";
                Su2tbpassword.Password = "";
                Su2tbpassword2.Password = "";
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(() => { tbworng.Visibility = Visibility.Visible; });
                    Thread.Sleep(10000);
                    this.Dispatcher.Invoke(() => { tbworng.Visibility = Visibility.Hidden; });
                }).Start();
                return;
            }
            else
            {
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
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() =>
                    {
                        loudGrid.Visibility = Visibility.Hidden;
                        mainGrid.Visibility = Visibility.Visible;
                    });
                }).Start();
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
                            accountAdmin.ToolTip = MyUser.FirstName;
                            userGrid.Visibility = Visibility.Visible;
                            MessageBox.Show("We will resolve any challenge before us \n" +
                                            "and plan to welcome all of you back soon.", "User Technical Difficulties"
                                            ,MessageBoxButton.OK,MessageBoxImage.Exclamation);
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
            return;
        }

        private void AddStop_Click(object sender, RoutedEventArgs e)
        {
            wAddStop addStop = new wAddStop(bl, Lists);
            addStop.ShowDialog();
        }

        #endregion
        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
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
            wDelbus delbus = new wDelbus(bl,Lists);
            delbus.ShowDialog();
            
        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            wAddBus addBus = new wAddBus(bl,Lists);
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
                            ListViewStations.SelectedItem = Lists.Stops.ToList().Find((BusStop) => BusStop.Code == searchStop.CodeStop);
                            ListViewStations.ScrollIntoView(ListViewStations.SelectedItem);

                            if (!searchStop.IsVisible)
                                flag = false;
                        });
                    else
                        this.Dispatcher.Invoke(() =>
                        {
                            ListViewStations.SelectedItem = Lists.Stops.ToList().Find((BusStop) => BusStop.Name == searchStop.NameStop);
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
                        ListViewLines.SelectedItem = Lists.Lines.ToList().Find((Line) => Line.NumLine == searchLine.numLine);
                        ListViewLines.ScrollIntoView(ListViewLines.SelectedItem);

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
            if (programClock == null || clockSpeed.Text.Length == 0) return;
            if (!TimeSpan.TryParse(programClock.Text, out TimeSpan time))
                return;
            if (bClock.Content.ToString() == "Start")
            {
                bClock.Content = "Stop";
                Simulator.RunWorkerAsync(new {Time=time,Speed= int.Parse(clockSpeed.Text)});
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
               
                 bl.StartSimulator((TimeSpan)anonimicType.Time ,(int) anonimicType.Speed, t => Simulator.ReportProgress(1, t));
                 while (!Simulator.CancellationPending)
                     Thread.Sleep(1000);
             };
            Simulator.ProgressChanged += (object sender, ProgressChangedEventArgs e) =>
              {
                  TimeSpan t = (TimeSpan)e.UserState;
                  programClock.Text = t.ToString(@"hh\:mm\:ss");
              };
            Simulator.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                bClock.Content = "Start";
                ListViewPanel.Visibility = Visibility.Hidden;
                bl.StopSimulator();
                programClock.IsEnabled = true;
                clockSpeed.IsEnabled = true;
                programClock.Text = "";
                clockSpeed.Text = "";
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
                ListViewPanel.ItemsSource = null;
                int size = (Lists.PanelStation.Count < 5) ? Lists.PanelStation.Count : 5;
                ListViewPanel.ItemsSource = Lists.PanelStation.GetRange(0, size);
            };
            SimulatorPanelStation.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
              {
                  if (args.Result is PO.BusStop)
                  {
                      Lists.PanelStation = new List<LineTiming>();
                      ListViewPanel.ItemsSource = null;
                      ListViewPanel.Visibility = Visibility.Hidden;
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
            adminWindow adminW = new adminWindow(bl);
            adminW.DataContext = MyUser;
            adminW.tbEditPhone.Text = MyUser.Phone;
            adminW.tpEditBirthday.SelectedDate = MyUser.Birthday;
            adminW.tbEditFirstName.Text = MyUser.FirstName;
            adminW.tbEditLastName.Text = MyUser.LastName;
            adminW.Show();
            new Thread(() =>
            {
                while (adminW.IsVisible == true)
                {
                    Thread.Sleep(1000);
                    if (adminW.GOBack == true)
                    {
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
                            adminW.Close();
                        });
                    }
                    if (adminW.GetUpdated == true)
                    {
                        MyUser = adminW.user1;
                        this.Dispatcher.Invoke(() =>
                        {
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
    }
}
