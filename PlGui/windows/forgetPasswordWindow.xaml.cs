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
using BlApi;
using BO;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for porgetPassword.xaml
    /// </summary>
    public partial class forgetPasswordWindow : Window
    {
        public IBL ibl = BlFactory.GetBL("1");
        public forgetPasswordWindow()
        {

            InitializeComponent();
        }

        private void bShowmMyPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                showPasswordTextBlock.Text = ibl.RecoverPassword(tbPhone.Text, tbBirthday.DisplayDate);
                var user = ibl.GetUser(tbUserName.Text, showPasswordTextBlock.Text);
                if (user == null)
                    return;
            }
            catch(PasswordRecoveryException)
            {
                this.Close();
            }
            showPasswordTextBlock.Visibility = Visibility.Visible;
           
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
