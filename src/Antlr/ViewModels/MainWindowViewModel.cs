using System.Linq;

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
        private readonly DirectoryCrawler _directoryCrawler;

        public MainWindowViewModel(StatusReader statusReader, DirectoryCrawler directoryCrawler)
        {
            _statusReader = statusReader;
            _directoryCrawler = directoryCrawler;
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
            var filterResults = new List<FilterResult>();
            var exists = Directory.Exists(ProjectUri);
            if (exists)
            {
                var directories = Directory.EnumerateDirectories(ProjectUri); //could depth-first from here, but I want the root-level folders to have ./ instead of /

                foreach (var directory in directories)
                {
                    var level = 1;
                    var filterStatus = _statusReader.GetFilterStatus(directory, Filter, FilterStatus.Found, ProjectUri, FilterRemoves);
                    filterResults.Add(
                        new FilterResult
                        {
                            FullPath = directory,
                            RelativePath = "." + directory.Remove(0, _projectUri.Length),
                            Level = level,
                            Status = filterStatus
                        });

                    if (Recursive)
                    {
                        _directoryCrawler.DepthFirstSearch(filterResults, directory, level, filterStatus, Filter, ProjectUri, FilterRemoves, HideChildren);
                    }

                }
            }
            else
            {
                return;
            }
            LastFilterResult = filterResults.Select(result => new FilterResultViewModel
            {
                FullPath = result.FullPath,
                Level = result.Level,
                RelativePath = result.RelativePath,
                Status = result.Status
            });
        }
    }
}