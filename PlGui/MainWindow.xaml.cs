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
            
            tbTitleMainWindow.Visibility = Visibility.Hidden;
            bGuestMode.Visibility = Visibility.Hidden;
            bSignup.Visibility = Visibility.Hidden;
            bLogIn.Visibility = Visibility.Hidden;
            bLogIn.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    BAlouded.Visibility = Visibility.Visible;
                    
                    
                });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    BAlouded.Visibility = Visibility.Hidden;
                    blogIn.Visibility = Visibility.Visible;
                    tBPassword.Visibility = Visibility.Visible;
                    tbpassword.Visibility = Visibility.Visible;
                    tbUserName.Visibility = Visibility.Visible;
                    tBUserName.Visibility = Visibility.Visible;
                    blogInBack.Visibility = Visibility.Visible;
                    tbTitleLOgInWindow.Visibility = Visibility.Visible;
                });

            }).Start();
            
        }

        private void blogInBack_Click(object sender, RoutedEventArgs e)
        {
            blogIn.Visibility = Visibility.Hidden;
            tBPassword.Visibility = Visibility.Hidden;
            tbpassword.Visibility = Visibility.Hidden;
            tbUserName.Visibility = Visibility.Hidden;
            tBUserName.Visibility = Visibility.Hidden;
            blogInBack.Visibility = Visibility.Hidden;
            tbTitleLOgInWindow.Visibility = Visibility.Hidden;
            new Thread(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    BAlouded.Visibility = Visibility.Visible;


                });

                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    BAlouded.Visibility = Visibility.Hidden;

                    tbTitleMainWindow.Visibility = Visibility.Visible;
                    bGuestMode.Visibility = Visibility.Visible;
                    bSignup.Visibility = Visibility.Visible;
                    bLogIn.Visibility = Visibility.Visible;
                    bLogIn.Visibility = Visibility.Visible;

                    
                });

            }).Start();
        }
    }
}
