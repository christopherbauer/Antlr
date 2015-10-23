using System.Windows;

namespace Antlr
{
    using System.IO;

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
            mainWindowViewModel.ProjectUri = Directory.GetCurrentDirectory();
            mainWindowViewModel.Filter = @"**\bin\**";
            mainWindowViewModel.SetupCommands();
            
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}
