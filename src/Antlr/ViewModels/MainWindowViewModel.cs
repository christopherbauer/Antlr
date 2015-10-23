namespace Antlr.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;

    using Antlr.Core;

    public class MainWindowViewModel: ViewModelBase
    {
        private IEnumerable<FilterResultViewModel> lastFilterResult;
        private string projectUri;
        private string antLikeFilter;

        private bool recursive;

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

        public string AntLikeFilter
        {
            get
            {
                return this.antLikeFilter;
            }
            set
            {
                SetValue(ref this.antLikeFilter, value);
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

        public bool Recursive
        {
            get
            {
                return this.recursive;
            }
            set
            {
                SetValue(ref this.recursive, value);
            }
        }

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
                    filterResultViewModels.Add(
                        new FilterResultViewModel
                        {
                            FullPath = directory,
                            RelativePath = "." + directory.Remove(0, projectUri.Length),
                            Level = level,
                            Status = FilterStatus.Found
                        });
                    
                    DepthFirstSearch(directory, filterResultViewModels, level);
                    
                }
            }
            else
            {
                return;
            }
            this.LastFilterResult = filterResultViewModels;
        }

        private void DepthFirstSearch(string directory, List<FilterResultViewModel> filterResultViewModels, int level)
        {
            var subDirectories = Directory.EnumerateDirectories(directory);
            var thisLevel = level + 1;
            foreach (var subDirectory in subDirectories)
            {
                filterResultViewModels.Add(
                    new FilterResultViewModel
                    {
                        FullPath = subDirectory,
                        RelativePath = subDirectory.Remove(0, directory.Length),
                        Level = thisLevel,
                        Status = FilterStatus.Found
                    });

                DepthFirstSearch(subDirectory, filterResultViewModels, thisLevel);
                
                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    filterResultViewModels.Add(new FilterResultViewModel
                    {
                        FullPath = file,
                        RelativePath = file.Remove(0, directory.Length),
                        Level = thisLevel,
                        Status = FilterStatus.Found
                    });
                }
            }
            
        }
    }
}