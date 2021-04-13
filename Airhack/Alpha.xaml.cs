using System;
using System.Windows;
using System.Windows.Controls;

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
            bckgnd.LayoutUpdated += Bckgnd_LayoutUpdated;
            bckgnd.Unloaded += Bckgnd_Unloaded;
        }


        private void Bckgnd_Unloaded(object sender, RoutedEventArgs e)
        {
            wndhost.Closing -= Wndhost_Closing;
            wndhost.SizeChanged -= Wndhost_SizeChanged;
            wndhost.LocationChanged -= Wndhost_LocationChanged;
            this.Hide();
        }

        private double GetSourceScaleX(PresentationSource source)
        {
            return source != null ? source.CompositionTarget.TransformToDevice.M11 : 1;
        }
        private double GetSourceScaleY(PresentationSource source)
        {
            return source != null ? source.CompositionTarget.TransformToDevice.M22 : 1;
        }

        private void UpdateOwnPosition()
        {
            if (wndhost == null)
            {
                return;
            }
            Point locationFromScreen = bckgnd.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(wndhost);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            this.Left = targetPoints.X;
            this.Top = targetPoints.Y;
        }

        private void UpdateOwnSize()
        {
            if (wndhost == null)
            {
                return;
            }
            PresentationSource source = PresentationSource.FromVisual(wndhost);
            Vector size = bckgnd.PointToScreen(new Point(bckgnd.ActualWidth, bckgnd.ActualHeight)) - bckgnd.PointToScreen(new Point(0, 0));
            this.Height = size.Y / GetSourceScaleY(source);
            this.Width = size.X / GetSourceScaleX(source);
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
                this.Show();
                wndhost.Focus();
            }
            catch
            {
                this.Hide();
            }
        }
        private void Bckgnd_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateOwnPosition();
            UpdateOwnSize();
        }

        private void Wndhost_LocationChanged(object sender, EventArgs e)
        {
            UpdateOwnPosition();
        }

        private void Wndhost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateOwnPosition();
            UpdateOwnSize();
        }

        private void Wndhost_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }
    }
}
