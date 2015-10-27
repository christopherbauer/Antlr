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
            var mainWindowViewModel = new MainWindowViewModel(new StatusReader(new AntRegexGenerator()))
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
