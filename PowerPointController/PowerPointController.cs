// Thanks : http://muumoo.jp/news/2008/03/26/0enumwindows.html
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace kaorun55
{
    public class PowerPointController
    {
        IntPtr slideWindow = IntPtr.Zero;
        const string defaultPrefix = "PowerPoint スライド ショー";

        public PowerPointController()
        {
            Prefix = defaultPrefix;
        }

        public string Prefix
        {
            get;
            set;
        }

        public void FindSlideShow()
        {
            slideWindow = IntPtr.Zero;

            EnumWindows( new EnumWindowsDelegate( delegate( IntPtr hWnd, int lParam )
            {
                StringBuilder sb = new StringBuilder( 0x1024 );
                if ( (IsWindowVisible( hWnd ) != 0) && (GetWindowText( hWnd, sb, sb.Capacity ) != 0) ) {
                    string title = sb.ToString();
                    Debug.Print( title );
                    if ( title.StartsWith( Prefix ) ) {
                        slideWindow = hWnd;
                    }
                }
                return 1;
            } ), 0 );

            if ( slideWindow == IntPtr.Zero ) {
                throw new Exception( "PowerPointのスライドショーが見つかりませんでした" );
            }
        }

        public void Next()
        {
            SendKey( "{Right}" );
        }

        public void Prev()
        {
            SendKey( "{Left}" );
        }

        private void SendKey( string key )
        {
            if ( slideWindow != IntPtr.Zero ) {
                bool ret = SetForegroundWindow( slideWindow );
                if ( !ret ) {
                    throw new Exception( "ウィンドウを前面に出せません" );
                }

                System.Windows.Forms.SendKeys.SendWait( key );
            }
        }

        private delegate int EnumWindowsDelegate( IntPtr hWnd, int lParam );

        [DllImport( "USER32.DLL" )]
        public static extern bool SetForegroundWindow( IntPtr hWnd );
        [DllImport( "user32.dll" )]
        private static extern int EnumWindows( EnumWindowsDelegate lpEnumFunc, int lParam );
        [DllImport( "user32.dll" )]
        private static extern int IsWindowVisible( IntPtr hWnd );
        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int GetWindowText( IntPtr hWnd, StringBuilder lpString, int nMaxCount );
    }
}
