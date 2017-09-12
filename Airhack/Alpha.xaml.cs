using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Airhack
{
    /// <summary>
    /// Interaction logic for Alpha.xaml
    /// </summary>
    public partial class Alpha : Window
    {
        Window wndhost;
        UserControl bckgnd;
        private UIElement _content;

        public new UIElement Content
        {
            get { return _content; }
            set
            {
                _content = value;
                gridContent.Children.Clear();
                gridContent.Children.Add(_content);
            }
        }

        internal Alpha(UserControl Background)
        {
            InitializeComponent();
            bckgnd = Background;
            bckgnd.Loaded += Bckgnd_Loaded;
            bckgnd.Unloaded += Bckgnd_Unloaded;
        }

        private void Bckgnd_Unloaded(object sender, RoutedEventArgs e)
        {
            wndhost.Closing -= Wndhost_Closing;
            wndhost.SizeChanged -= Wndhost_SizeChanged;
            wndhost.LocationChanged -= Wndhost_LocationChanged;
            this.Hide();
        }

        private void Bckgnd_Loaded(object sender, RoutedEventArgs e)
        {
            wndhost = Window.GetWindow(bckgnd);
            this.Owner = wndhost;
            wndhost.Closing += Wndhost_Closing;
            wndhost.SizeChanged += Wndhost_SizeChanged;
            wndhost.LocationChanged += Wndhost_LocationChanged;
            try
            {
                Point locationFromScreen = bckgnd.PointToScreen(new Point(0, 0));
                PresentationSource source = PresentationSource.FromVisual(wndhost);
                System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
                this.Left = targetPoints.X;
                this.Top = targetPoints.Y;
                Vector size = bckgnd.PointToScreen(new Point(bckgnd.ActualWidth, bckgnd.ActualHeight)) - bckgnd.PointToScreen(new Point(0, 0));
                this.Height = size.Y;
                this.Width = size.X;
                this.Show();
                wndhost.Focus();
            }
            catch
            {
                this.Hide();
            }
        }

        private void Wndhost_LocationChanged(object sender, EventArgs e)
        {
            Point locationFromScreen = bckgnd.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(wndhost);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            this.Left = targetPoints.X;
            this.Top = targetPoints.Y;
        }

        private void Wndhost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Point locationFromScreen = bckgnd.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(wndhost);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            this.Left = targetPoints.X;
            this.Top = targetPoints.Y;
            Vector size = bckgnd.PointToScreen(new Point(bckgnd.ActualWidth, bckgnd.ActualHeight)) - bckgnd.PointToScreen(new Point(0, 0));
            this.Height = size.Y;
            this.Width = size.X;
        }

        private void Wndhost_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }
    }
}
