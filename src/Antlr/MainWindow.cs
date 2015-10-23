using System.Windows;

namespace Antlr
{
    using Antlr.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var mainWindowViewModel = new MainWindowViewModel();
            mainWindowViewModel.Recursive = true;
            mainWindowViewModel.SetupCommands();
            
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}
