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
using System.Runtime.InteropServices;
using System.Diagnostics;
using kaorun55;

namespace ppt_controller
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        PowerPointController ppt = new PowerPointController();
        
        public MainWindow()
        {
            InitializeComponent();
        }


        private void buttonFindSlideShow_Click( object sender, RoutedEventArgs e )
        {
            try {
                ppt.FindSlideShow();
            }
            catch ( Exception ex ) {
                MessageBox.Show( ex.Message );
            }
        }

        private void buttonNext_Click( object sender, RoutedEventArgs e )
        {
            try {
                ppt.Next();
            }
            catch ( Exception ex ) {
                MessageBox.Show( ex.Message );
            }
        }

        private void buttonPrev_Click( object sender, RoutedEventArgs e )
        {
            try {
                ppt.Prev();
            }
            catch ( Exception ex ) {
                MessageBox.Show( ex.Message );
            }
        }
    }
}
