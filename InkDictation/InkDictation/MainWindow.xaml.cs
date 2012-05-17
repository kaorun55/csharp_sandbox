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
using System.Windows.Ink;

namespace InkDictation
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<InkCanvas, TextBlock> viewTarget;

        public MainWindow()
        {
            InitializeComponent();

            viewTarget = new Dictionary<InkCanvas,TextBlock>();
            viewTarget[canvas1] = textBlock1;
            viewTarget[canvas2] = textBlock2;
            viewTarget[canvas3] = textBlock3;
            viewTarget[canvas4] = textBlock4;

            textBlock1.Text = "";
            textBlock2.Text = "";
            textBlock3.Text = "";
            textBlock4.Text = "";
        }

        // IACore,IAWinFx
        //  C:\Program Files\Reference Assemblies\Microsoft\Tablet PC\v1.7
        // IALoader
        //  C:\Program Files\Microsoft SDKs\Windows\v6.1\Bin
        // それぞれのライブラリは「Windows SDK for Windows Server 2008 and .NET Framework 3.5」で取得できる
        //  http://www.microsoft.com/en-us/download/confirmation.aspx?id=11310
        private string InkAnalyze( InkCanvas canvas )
        {
            InkAnalyzer theInkAnalyzer = new InkAnalyzer();

            // キャンバスに描かれた文字を認識するためにアナライザにストロークをセット
            theInkAnalyzer.AddStrokes( canvas.Strokes );
            theInkAnalyzer.SetStrokesType( canvas.Strokes, StrokeType.Writing );

            // 文字を解析
            theInkAnalyzer.Analyze();

            // 文字を解析した結果の第1候補を返す
            return theInkAnalyzer.GetRecognizedString();
        }

        private void buttonClear_Click( object sender, RoutedEventArgs e )
        {
            textBlock1.Text = "";
            textBlock2.Text = "";
            textBlock3.Text = "";
            textBlock4.Text = "";

            canvas1.Strokes.Clear();
            canvas2.Strokes.Clear();
            canvas3.Strokes.Clear();
            canvas4.Strokes.Clear();
        }

        private void canvas_MouseUp( object sender, MouseButtonEventArgs e )
        {
            var canvas = sender as InkCanvas;
            if ( canvas != null ) {
                viewTarget[canvas].Text = InkAnalyze( canvas );
            }
        }
    }
}
