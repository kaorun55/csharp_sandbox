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
using System.Windows.Interop;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace send_keys
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

        private void button1_Click( object sender, RoutedEventArgs e )
        {
            textBox1.Focus();

            Thread.Sleep( 2000 );

            System.Windows.Forms.SendKeys.SendWait( "A" );
        }


        // http://www.ipentec.com/document/document.aspx?page=csharp-wpf-screen-capture-sendkey-winform
        private void button2_Click( object sender, RoutedEventArgs e )
        {
            //アクティブウィンドウをスクリーンキャプチャする場合
            System.Windows.Forms.SendKeys.SendWait("%{PRTSC}");

            //全画面をスクリーンキャプチャする場合
            //System.Windows.Forms.SendKeys.SendWait( "^{PRTSC}" );

            //全画面をスクリーンキャプチャする場合(こっち？)
            //System.Windows.Forms.SendKeys.SendWait( "{PRTSC}" );

            IDataObject dobj = Clipboard.GetDataObject();
            if ( dobj.GetDataPresent( DataFormats.Bitmap ) == true ) {
                InteropBitmap ibmp = (InteropBitmap)dobj.GetData( DataFormats.Bitmap );

                BmpBitmapEncoder enc = new BmpBitmapEncoder();
                //JpegBitmapEncoder enc = new JpegBitmapEncoder();//JPEGファイルで保存する場合
                enc.Frames.Add( BitmapFrame.Create( ibmp ) );
                using ( FileStream fs = new FileStream( @"screen2.bmp", FileMode.Create, FileAccess.Write ) ) {
                    enc.Save( fs );
                }
            }
        }

        private void button2_KeyDown( object sender, KeyEventArgs e )
        {
            Trace.WriteLine( e.Key.ToString() );
        }

        // Get a handle to an application window.
        [DllImport( "USER32.DLL" )]
        public static extern IntPtr FindWindow( string lpClassName,
            string lpWindowName );

        // Activate an application window.
        [DllImport( "USER32.DLL" )]
        public static extern bool SetForegroundWindow( IntPtr hWnd );

        #region EnumWindowsTest(http://muumoo.jp/news/2008/03/26/0enumwindows.html)
        private delegate int EnumWindowsDelegate( IntPtr hWnd, int lParam );

        [DllImport( "user32.dll" )]
        private static extern int EnumWindows( EnumWindowsDelegate lpEnumFunc, int lParam );
        [DllImport( "user32.dll" )]
        private static extern int IsWindowVisible( IntPtr hWnd );
        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int GetWindowText( IntPtr hWnd, StringBuilder lpString, int nMaxCount );
        [DllImport( "user32.dll" )]
        private static extern uint GetWindowThreadProcessId( IntPtr hWnd, out int lpdwProcessId );

        private void button3_Click( object sender, RoutedEventArgs e )
        {
            EnumWindows( new EnumWindowsDelegate( delegate( IntPtr hWnd, int lParam )
            {
                StringBuilder sb = new StringBuilder( 0x1024 );
                if ( IsWindowVisible( hWnd ) != 0 && GetWindowText( hWnd, sb, sb.Capacity ) != 0 ) {
                    string title = sb.ToString();
                    int pid;
                    GetWindowThreadProcessId( hWnd, out pid );
                    Process p = Process.GetProcessById( pid );

                    Debug.Print( title + " - " + p.ProcessName );
                    if ( title.StartsWith( "PowerPoint スライド ショー" ) ) {
                        SetForegroundWindow( hWnd );
                        System.Windows.Forms.SendKeys.SendWait( "{Right}" );
                    }
                }
                return 1;
            } ), 0 );
        }
        #endregion
    }
}
