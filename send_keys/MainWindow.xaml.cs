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
    }
}
