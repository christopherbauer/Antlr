using System.Windows;

namespace Antlr
{
    using Antlr.ViewModels.Designer;

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
