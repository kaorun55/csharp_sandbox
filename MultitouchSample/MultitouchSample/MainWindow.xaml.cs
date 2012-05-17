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

namespace MultitouchSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        int maxZIndex;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ManipulationStarting( object sender,
                                                     ManipulationStartingEventArgs e )
        {
            e.ManipulationContainer = this;
            e.Handled = true;

            var element = e.OriginalSource as FrameworkElement;
            Canvas.SetZIndex( element, maxZIndex++ );
        }


        private void Window_ManipulationDelta( object sender,
                                                 ManipulationDeltaEventArgs e )
        {
            var element = e.OriginalSource as FrameworkElement;
            Matrix rectsMatrix = ((MatrixTransform)element.RenderTransform).Matrix;


            rectsMatrix.RotateAt( e.DeltaManipulation.Rotation,
                                  e.ManipulationOrigin.X, e.ManipulationOrigin.Y );


            rectsMatrix.ScaleAt( e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.X,
                                 e.ManipulationOrigin.X, e.ManipulationOrigin.Y );


            rectsMatrix.Translate( e.DeltaManipulation.Translation.X,
                                   e.DeltaManipulation.Translation.Y );


            element.RenderTransform = new MatrixTransform( rectsMatrix );


            e.Handled = true;
        }
    }
}
