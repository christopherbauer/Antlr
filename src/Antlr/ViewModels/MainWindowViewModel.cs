namespace Antlr.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;

    using Antlr.Core;

    public class MainWindowViewModel : ViewModelBase
    {
        private IEnumerable<FilterResultViewModel> lastFilterResult;
        private string projectUri;
        private string filter;

        private bool recursive;

        private bool hideChildren;

        private AntRegexGenerator antRegexGenerator = new AntRegexGenerator();

        private bool filterRemoves;

        public void SetupCommands()
        {
            FilterResultCommand = new RelayCommand(RefreshFilterResults);
        }

        public string ProjectUri
        {
            get
            {
                return this.projectUri;
            }
            set
            {
                SetValue(ref this.projectUri, value);
            }
        }

        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                SetValue(ref this.filter, value);
            }
        }

        public bool Recursive
        {
            get
            {
                return this.recursive;
            }
            set
            {
                this.SetValue(ref this.recursive, value);
            }
        }

        public bool HideChildren
        {
            get
            {
                return this.hideChildren;
            }
            set
            {
                this.SetValue(ref this.hideChildren, value);
            }
        }

        public bool FilterRemoves
        {
            get
            {
                return this.filterRemoves;
            }
            set
            {
                SetValue(ref this.filterRemoves, value);
            }
        }

        public IEnumerable<FilterResultViewModel> LastFilterResult
        {
            get
            {
                return this.lastFilterResult;
            }
            set
            {
                SetValue(ref this.lastFilterResult, value);
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
                    var filterStatus = this.GetFilterStatus(directory, this.Filter, FilterStatus.Found);
                    filterResultViewModels.Add(
                        new FilterResultViewModel
                        {
                            FullPath = directory,
                            RelativePath = "." + directory.Remove(0, projectUri.Length),
                            Level = level,
                            Status = filterStatus
                        });

                    DepthFirstSearch(directory, filterResultViewModels, level, filterStatus);

                }
            }
            else
            {
                return;
            }
            this.LastFilterResult = filterResultViewModels;
        }

        private FilterStatus GetFilterStatus(string directory, string filter, FilterStatus parentFilterStatus)
        {
            filter = filter.Replace("\\", "\\\\");
            var tempDirectory = directory.Remove(0, ProjectUri.Length);
            if (parentFilterStatus != FilterStatus.Ignored && parentFilterStatus != FilterStatus.ParentIgnored)
            {
                var regex = this.antRegexGenerator.GetRegexForFilter(filter);
                if (FilterRemoves)
                {
                    return regex.IsMatch(tempDirectory) ? FilterStatus.Ignored : FilterStatus.Found;
                }
                else
                {
                    return regex.IsMatch(tempDirectory) ? FilterStatus.Found : FilterStatus.Ignored;
                }
            }
            else
            {
                return FilterStatus.ParentIgnored;
            }
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
                var filterStatus = this.GetFilterStatus(subDirectory, this.Filter, parentFilterStatus);
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
                                Status = GetFilterStatus(file, Filter, parentFilterStatus)
                            });
                }
            }
        }
    }
}