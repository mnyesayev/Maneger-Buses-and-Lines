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

namespace dotNet5781_03B_3729_1237
{
    /// <summary>
    /// Interaction logic for wAddBus.xaml
    /// </summary>
    public partial class wAddBus : Window
    {
       // DateTime tmpDra;
        uint id;
        DateTime Dra;//date road ascent
        uint mileage;
        uint mileageLastCare;
        DateTime dateLastCare;
        public wAddBus()
        {
            InitializeComponent();
        }
        private Bus newBus = null;
        public Bus NewBus { get => newBus; private set=>newBus= value; }
        
        private void bDoneAddBus_Click(object sender, RoutedEventArgs e)
        {
            //uint id;
            //DateTime Dra;//date road ascent
            //uint mileage;
            //uint mileageLastCare;
            //DateTime dateLastCare;
            //if(!DateTime.TryParse(dPDRA.Text,out Dra))
            //{
            //    MessageBox.Show("Enter positive number!");
            //}
            //if (!uint.TryParse(tBIdBus.Text,out id))
            //{
            //   MessageBox.Show("Enter positive Id bus!"); 
            //}
            //if (!uint.TryParse(tBMileage.Text, out mileage))
            //{
            //    MessageBox.Show("Enter positive Total Mileage !");
            //}
            //if (!uint.TryParse(tBIdBus.Text, out mileageLastCare))
            //{
            //    MessageBox.Show("Enter positive Mileage Last Care!");
            //}
            //if (!DateTime.TryParse(dPDateLastCare.Text, out dateLastCare))
            //{
            //    MessageBox.Show("Enter positive number!");
            //}
            newBus = new Bus(Dra,id);
            newBus.Mileage = mileage;
            newBus.LastCareMileage = mileageLastCare;
            newBus.LastCare = dateLastCare;
            this.Close();
        }

        private void dPDRA_calendrer_closed (object sender, RoutedEventArgs e)
        {
            Dra = DateTime.Parse(dPDRA.Text);
            if(Dra > DateTime.Now)
            {
                MessageBox.Show("you can not enter futher date!","ERROR DATE",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                tBIdBus.IsEnabled = true;
            }
        }

        private void afterEnterId(object sender, TextChangedEventArgs e)
        {
            //tBMileage.IsEnabled = true;
        }
    }
}
