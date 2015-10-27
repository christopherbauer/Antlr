using System.Windows;
using Antlr.Core;

namespace Antlr
{
    using System.IO;

    using ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var statusReader = new StatusReader(new AntRegexGenerator());
            var mainWindowViewModel = new MainWindowViewModel(new DirectoryCrawler(statusReader))
            {
                Recursive = true,
                ProjectUri = Directory.GetCurrentDirectory(),
                Filter = @"**\bin\**"
            };
            mainWindowViewModel.SetupCommands();
            
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }
    }
}
