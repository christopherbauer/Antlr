namespace Antlr.ViewModels
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;

    using Core;

    public class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<FilterResultViewModel> _lastFilterResult;
        private string _projectUri;
        private string _filter;

        private bool _recursive;

        private bool _hideChildren;

        private bool _filterRemoves;
        private readonly StatusReader _statusReader;

        public MainWindowViewModel(StatusReader statusReader)
        {
            _statusReader = statusReader;
        }

        public void SetupCommands()
        {
            FilterResultCommand = new RelayCommand(RefreshFilterResults);
        }

        public string ProjectUri
        {
            get
            {
                return _projectUri;
            }
            set
            {
                SetValue(ref _projectUri, value);
            }
        }

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                SetValue(ref _filter, value);
            }
        }

        public bool Recursive
        {
            get
            {
                return _recursive;
            }
            set
            {
                SetValue(ref _recursive, value);
            }
        }

        public bool HideChildren
        {
            get
            {
                return _hideChildren;
            }
            set
            {
                SetValue(ref _hideChildren, value);
            }
        }

        public bool FilterRemoves
        {
            get
            {
                return _filterRemoves;
            }
            set
            {
                SetValue(ref _filterRemoves, value);
            }
        }

        public IEnumerable<FilterResultViewModel> LastFilterResult
        {
            get
            {
                return _lastFilterResult;
            }
            set
            {
                SetValue(ref _lastFilterResult, value);
            }
        }

        public ICommand FilterResultCommand { get; set; }

        public void RefreshFilterResults()
        {
            var filterResultViewModels = new List<FilterResultViewModel>();
            var exists = Directory.Exists(ProjectUri);
            if (exists)
            {
                var directories = Directory.EnumerateDirectories(ProjectUri); //could depth-first from here, but I want the root-level folders to have ./ instead of /

                foreach (var directory in directories)
                {
                    var level = 1;
                    var filterStatus = _statusReader.GetFilterStatus(directory, Filter, FilterStatus.Found, ProjectUri, FilterRemoves);
                    filterResultViewModels.Add(
                        new FilterResultViewModel
                        {
                            FullPath = directory,
                            RelativePath = "." + directory.Remove(0, _projectUri.Length),
                            Level = level,
                            Status = filterStatus
                        });

                    if (Recursive)
                    {
                        DepthFirstSearch(directory, filterResultViewModels, level, filterStatus);
                    }

                }
            }
            else
            {
                return;
            }
            LastFilterResult = filterResultViewModels;
        }

        private void DepthFirstSearch(string directory, List<FilterResultViewModel> filterResultViewModels, int level, FilterStatus parentFilterStatus)
        {
            if (HideChildren
                && (parentFilterStatus == FilterStatus.Ignored || parentFilterStatus == FilterStatus.ParentIgnored))
            {
                return;
            }
            var subDirectories = Directory.EnumerateDirectories(directory);
            var thisLevel = level + 1;
            foreach (var subDirectory in subDirectories)
            {
                var filterStatus = _statusReader.GetFilterStatus(subDirectory, Filter, parentFilterStatus, ProjectUri, FilterRemoves);
                filterResultViewModels.Add(
                    new FilterResultViewModel
                        {
                            FullPath = subDirectory,
                            RelativePath = subDirectory.Remove(0, directory.Length),
                            Level = thisLevel,
                            Status = filterStatus
                        });

                DepthFirstSearch(subDirectory, filterResultViewModels, thisLevel, filterStatus);

                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    filterResultViewModels.Add(
                        new FilterResultViewModel
                            {
                                FullPath = file,
                                RelativePath = file.Remove(0, directory.Length),
                                Level = thisLevel,
                                Status = _statusReader.GetFilterStatus(file, Filter, parentFilterStatus, ProjectUri, FilterRemoves)
                            });
                }
            }
        }
    }
}