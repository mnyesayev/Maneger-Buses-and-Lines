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
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for wDelbus.xaml
    /// </summary>
    public partial class wDelbus : Window
    {
        uint idDelbus;
        public wDelbus()
        {
            InitializeComponent();

        }

        public uint IdDelbus { get => idDelbus; private set => idDelbus = value; }

        private void tbBusId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {
                    IdDelbus = uint.Parse(text.Text);
                    e.Handled = true;
                    this.Close();
                }

            }
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 ||
                    e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 ||
                    e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c)) return;
            if (char.IsDigit(c))
                return;
            e.Handled = true;
            MessageBox.Show("Only numbers are allowed", "Delete Bus", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
