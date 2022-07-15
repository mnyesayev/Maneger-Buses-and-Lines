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
namespace PLGui
{
    /// <summary>
    /// Interaction logic for wDelbus.xaml
    /// </summary>
    public partial class wDelbus : Window
    {
        PO.Lists Lists;
        readonly IBL bl;
        public wDelbus(IBL bl,PO.Lists lists)
        {
            InitializeComponent();
            Lists = lists;
            this.bl = bl;
        }
        private void tbBusId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (text.Text.Length > 0)
                {
                    var id= int.Parse(text.Text);
                    try
                    {
                        bl.DeleteBus((int)id);
                        Lists.Buses.Remove(Lists.Buses.ToList().Find((Bus) => Bus.Id == id));
                    }
                    catch (BO.DeleteException ex)
                    {
                        MessageBox.Show(ex.Message, "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    e.Handled = true;
                    this.Close();
                }

            }

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (char.IsControl(c) || char.IsDigit(c) || e.Key == Key.Right || e.Key == Key.Left)
                return;
            if ((e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && (e.Key < Key.D0 || e.Key > Key.D9))
                e.Handled = true;
        }
    }
}
