using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        public bool GOBack = false;
        public bool GetUpdated = false;
        public BO.User user1;
        IBL bl;
        
        public AccountWindow(IBL bL)
        {
            InitializeComponent();
            bl = bL;
        }

        private void blogOut_Click(object sender, RoutedEventArgs e)
        {
            GOBack = true;
        }

        private void bCloseAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bSaveAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            tbEditPhone.IsEnabled = false;
            tpEditBirthday.IsEnabled = false;
            tbEditFirstName.IsEnabled = false;
            tbEditLastName.IsEnabled = false;
            BO.User user = this.DataContext as BO.User;
            BO.User updaedUser = new BO.User()
            {
                UserName = user.UserName,
                Authorization = user.Authorization,
                Birthday = DateTime.Parse(tpEditBirthday.Text),
                FirstName = tbEditFirstName.Text,
                LastName = tbEditLastName.Text,
                Password = user.Password,
                Phone = tbEditPhone.Text
            };
            try
            {
                user1 = bl.UpdateUser(updaedUser);
                GetUpdated = true;
            }
            catch(BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "Update ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong here\nTry updating only one statistic", "Update ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void bEditPhone_Click(object sender, RoutedEventArgs e)
        {
            tbEditPhone.IsEnabled = true;
        }

        private void bEditBirthday_Click(object sender, RoutedEventArgs e)
        {
            tpEditBirthday.IsEnabled = true;
        }

        private void bEditFirstName_Click(object sender, RoutedEventArgs e)
        {
            tbEditFirstName.IsEnabled = true;
        }

        private void bEditLastName_Click(object sender, RoutedEventArgs e)
        {
            tbEditLastName.IsEnabled = true;
        }

        private void tpEditBirthday_PreKeyD(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
