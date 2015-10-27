using System.Collections.Generic;
using System.IO;

namespace Antlr.Core
{
    public class DirectoryCrawler
    {
        private readonly StatusReader _statusReader;

        public DirectoryCrawler(StatusReader statusReader)
        {
            _statusReader = statusReader;
        }

        public void DepthFirstSearch(List<FilterResult> accumulator, string directory, int level, FilterStatus parentFilterStatus, string filter, string projectUri, bool filterRemoves, bool hideChildren = false)
        {
            if (hideChildren
                && (parentFilterStatus == FilterStatus.Ignored || parentFilterStatus == FilterStatus.ParentIgnored))
            {
                return;
            }
            var subDirectories = Directory.EnumerateDirectories(directory);
            var thisLevel = level + 1;
            foreach (var subDirectory in subDirectories)
            {
                var filterStatus = _statusReader.GetFilterStatus(subDirectory, filter, parentFilterStatus, projectUri, filterRemoves);
                accumulator.Add(
                    new FilterResult
                    {
                        FullPath = subDirectory,
                        RelativePath = subDirectory.Remove(0, directory.Length),
                        Level = thisLevel,
                        Status = filterStatus
                    });

                DepthFirstSearch(accumulator, subDirectory, thisLevel, filterStatus, filter, projectUri, filterRemoves, hideChildren);

                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    accumulator.Add(
                        new FilterResult
                        {
                            FullPath = file,
                            RelativePath = file.Remove(0, directory.Length),
                            Level = thisLevel,
                            Status = _statusReader.GetFilterStatus(file, filter, parentFilterStatus, projectUri, filterRemoves)
                        });
                }
            }
        }
    }
}