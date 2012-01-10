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

namespace ppt_controller
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region EnumWindows(http://muumoo.jp/news/2008/03/26/0enumwindows.html)
        private delegate int EnumWindowsDelegate( IntPtr hWnd, int lParam );

        [DllImport( "USER32.DLL" )]
        public static extern bool SetForegroundWindow( IntPtr hWnd );
        [DllImport( "user32.dll" )]
        private static extern int EnumWindows( EnumWindowsDelegate lpEnumFunc, int lParam );
        [DllImport( "user32.dll" )]
        private static extern int IsWindowVisible( IntPtr hWnd );
        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int GetWindowText( IntPtr hWnd, StringBuilder lpString, int nMaxCount );

        private void FindSlideShow()
        {
            EnumWindows( new EnumWindowsDelegate( delegate( IntPtr hWnd, int lParam )
            {
                StringBuilder sb = new StringBuilder( 0x1024 );
                if ( (IsWindowVisible( hWnd ) != 0) && (GetWindowText( hWnd, sb, sb.Capacity ) != 0) ) {
                    string title = sb.ToString();
                    Debug.Print( title );
                    if ( title.StartsWith( "PowerPoint スライド ショー" ) ) {
                        slideWindow = hWnd;
                    }
                }
                return 1;
            } ), 0 );

            if ( slideWindow == IntPtr.Zero ) {
                MessageBox.Show( "PowerPointのスライドショーが見つかりませんでした" );
            }
        }

        IntPtr slideWindow = IntPtr.Zero;
        #endregion

        private void buttonFindSlideShow_Click( object sender, RoutedEventArgs e )
        {
            FindSlideShow();
        }

        private void buttonNext_Click( object sender, RoutedEventArgs e )
        {
            SendKey( "{Right}" );
        }

        private void buttonPrev_Click( object sender, RoutedEventArgs e )
        {
            SendKey( "{Left}" );
        }

        private void SendKey( string key )
        {
            if ( slideWindow != IntPtr.Zero ) {
                SetForegroundWindow( slideWindow );
                System.Windows.Forms.SendKeys.SendWait( key );
            }
        }
    }
}
