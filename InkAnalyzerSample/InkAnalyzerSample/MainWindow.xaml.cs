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

namespace InkAnalyzerSample
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

        private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            InkAnalyzer theInkAnalyzer = new InkAnalyzer();

            // キャンバスに描かれた文字を認識するためにアナライザにストロークをセット
            theInkAnalyzer.AddStrokes(InkCanvas1.Strokes);
            theInkAnalyzer.SetStrokesType(InkCanvas1.Strokes, StrokeType.Writing);

            // 文字を解析
            theInkAnalyzer.Analyze();

            // 文字を解析した結果の第1候補を表示する
            MessageBox.Show(theInkAnalyzer.GetRecognizedString());

            // その他の候補を表示する
            AnalysisAlternateCollection alternates = theInkAnalyzer.GetAlternates();
            foreach (var alternate in alternates)
                MessageBox.Show(alternate.RecognizedString);
        }
    }
}
