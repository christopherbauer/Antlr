namespace Antlr.ViewModels.Designer
{
    using System.Collections.Generic;
    using Core;

    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        public DesignMainWindowViewModel() : base(new StatusReader(new AntRegexGenerator()))
        {
            ProjectUri = @"C:\svn\Antler";
            Filter = @"**/docs/**";
            HideChildren = true;
            FilterRemoves = true;
            LastFilterResult = new List<FilterResultViewModel>
                                   {
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\docs",
                                               RelativePath = @".\docs",
                                               Level = 1,
                                               Status = FilterStatus.Ignored
                                           },
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\docs\Test\",
                                               RelativePath = @"\Test",
                                               Level = 2,
                                               Status = FilterStatus.ParentIgnored
                                           },
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\docs\Test\Test File.txt",
                                               RelativePath = @"\Test File.txt",
                                               Level = 3,
                                               Status = FilterStatus.ParentIgnored
                                           },
                
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\src\",
                                               RelativePath = @".\src",
                                               Level = 1,
                                               Status = FilterStatus.Found
                                           },
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\src\File.cs",
                                               RelativePath = @"\File.cs",
                                               Level = 2,
                                               Status = FilterStatus.Found
                                           },
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\test\",
                                               RelativePath = @".\test",
                                               Level = 1,
                                               Status = FilterStatus.Found
                                           },
                
                                       new FilterResultViewModel
                                           {
                                               FullPath = @"C:\svn\Antler\test\FileUnitTests.cs",
                                               RelativePath = @"\FileUnitTests.cs",
                                               Level = 2,
                                               Status = FilterStatus.Found
                                           },
                
                                   };
        }
    }
}