using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace WpfApplication5
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(10);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke( new Action( () =>
            {
                arc.EndAngle += 2;
            }) );
        }
    }
}
