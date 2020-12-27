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
using System.Threading;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for porgetPassword.xaml
    /// </summary>
    public partial class forgetPasswordWindow : Window
    {
        public forgetPasswordWindow()
        {
            InitializeComponent();
        }

        private void bShowmMyPassword_Click(object sender, RoutedEventArgs e)
        {

            showPasswordTextBlock.Visibility = Visibility.Visible;
            showPasswordTextBlock.Text = "123456789";
            new Thread(() =>
            {
                Thread.Sleep(10000);
                this.Dispatcher.Invoke(() =>
                {
                    
                    this.Close();
                });
                
            }).Start();
        }
    }
}
