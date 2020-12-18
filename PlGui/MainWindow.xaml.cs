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
    }
}
