namespace Antlr.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;

    public class MainWindowViewModel
    {
        public void SetupCommands()
        {
            FilterResultCommand = new RelayCommand(RefreshFilterResults);
        }

        public string ProjectUri { get; set; }
        public string AntLikeFilter { get; set; }
        public IEnumerable<FilterResultViewModel> LastFilterResult { get; set; }
        public ICommand FilterResultCommand { get; set; }

        public void RefreshFilterResults()
        {

        }
    }
}