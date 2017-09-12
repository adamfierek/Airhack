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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Airhack
{
    /// <summary>
    /// Interaction logic for AirControl.xaml
    /// </summary>
    public partial class AirControl : UserControl
    {
        private Alpha alpha;
        private UIElement back;

        public UIElement Back
        {
            get
            {
                return back;
            }
            set
            {
                back = value;
                contentGrid.Children.Clear();
                contentGrid.Children.Add(back);
            }
        }

        private UIElement front;

        public UIElement Front
        {
            get
            {
                return front;
            }
            set
            {
                front = value;
                alpha.Content = front;
            }
        }


        public AirControl()
        {
            InitializeComponent();
            alpha = new Alpha(this);
        }
    }
}
