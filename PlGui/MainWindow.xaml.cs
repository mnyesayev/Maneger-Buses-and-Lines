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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            if (SutbFirstName.Text.Length < 1 )
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
            if (SutbPhoneNumber.Text.Length != 10 || !int.TryParse(SutbPhoneNumber.Text, out int i) )
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
    }
}
