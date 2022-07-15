﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PLGui
//{
//    /// <summary>
//    /// Interaction logic for wStartDrive.xaml
//    /// </summary>
//    public partial class wStartDrive : Window
//    {
//        public wStartDrive()
//        {
//            InitializeComponent();
//        }
//        Thread thStartDrive;
//        public Thread ThStartDrive { get => thStartDrive; set => thStartDrive = value; }
//        private void tbMileage_PreviewKeyDown(object sender, KeyEventArgs e)
//        {
//            TextBox text = sender as TextBox;
//            if (text == null) return;
//            if (e == null) return;

//            if (e.Key == Key.Enter || e.Key == Key.Return)
//            {
//                double t = 0;
//                if (text.Text.Length > 0)
//                {
//                    var tmp = uint.Parse(text.Text);
//                    e.Handled = true;
//                    BO.Bus bus = (BO.Bus)this.DataContext;
//                    if (bus.CheckCare())
//                    {
//                        MessageBox.Show("You can't to start drive \ntake the bus to care immediately!"
//                           , "Care ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
//                        this.Close();
//                        return;
//                    }
//                    if (bus.CheckCare(tmp))
//                    {
//                        MessageBox.Show($"You can't to start drive of {tmp}km, \nconsider taking the bus for care"
//                            , "Care ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
//                        this.Close();
//                        return;
//                    }
//                    if (bus.CheckFuel(tmp))
//                    {
//                        MessageBox.Show("You can't to start drive because \nthere is not enough fuel for this trip!"
//                               , "Fuel ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
//                        this.Close();
//                        return;
//                    }
//                    ThStartDrive = new Thread(() =>
//                     {
//                         bus.State = States.drive;
//                         bus.Image = "images\\blue.png";
//                         var s = MyRandom.r.Next(20, 50);
//                         t = Math.Ceiling(((double)tmp / s) * 6);
//                         for (bus.Time = (int)t; bus.Time > 0; --bus.Time)
//                         {
//                             Thread.Sleep(new TimeSpan(0, 0, 1));
//                         }
//                         bus.StartDrive(tmp);
//                         if (bus.CheckCare())
//                         {
//                             bus.State = States.mustCare;
//                             bus.Image = "images\\red.png";
//                         }
//                         else if (bus.CheckFuel(0))
//                         {
//                             bus.State = States.mustRefuel;
//                             bus.Image = "images\\red.png";
//                         }
//                         else
//                         {
//                             bus.State = States.ready;
//                             bus.Image = "images\\green.png";
//                         }
//                     });
//                    ThStartDrive.Start();
//                    this.Close();
//                    return;
//                }
//            }
//            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 ||
//                e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 ||
//                e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
//                return;
//            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
//            if (char.IsControl(c)) return;
//            if (char.IsDigit(c))
//                return;
//            e.Handled = true;
//            MessageBox.Show("Only numbers are allowed", "Start drive", MessageBoxButton.OK, MessageBoxImage.Error);
//        }
//    }
//}
