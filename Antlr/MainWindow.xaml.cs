using System.Windows;

namespace Antlr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new DesignMainWindowViewModel();
            InitializeComponent();
        }
    }
}
