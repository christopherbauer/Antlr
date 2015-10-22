using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Antlr
{
    public class MainWindowViewModel
    {
        public string ProjectUri { get; set; }
        public string AntLikeFilter { get; set; }
        public IEnumerable<FilterResultViewModel> LastFilterResult { get; set; }
        public ICommand FilterResultCommand { get; set; }

        public void RefreshFilterResults()
        {
            
        }
    }

    public class DesignMainWindowViewModel : MainWindowViewModel
    {
        public DesignMainWindowViewModel()
        {
            ProjectUri = @"C:\svn\Antler";
            AntLikeFilter = @"**/docs/**";
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

    public class FilterResultViewModel
    {
        public int Level { get; set; }
        public FilterStatus Status { get; set; }
        public string FullPath { get; set; }
        public string RelativePath { get; set; }

        public decimal GetIndent {
            get
            {
                return Level * 25; 
            }
        }
    }

    public enum FilterStatus
    {
        Found,
        Ignored,
        ParentIgnored
    }

    public class MarginLeftValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var left = System.Convert.ToInt32(value);
            return new Thickness(left, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class StatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (FilterStatus)value;
            if (status == FilterStatus.Found)
            {
                return new SolidColorBrush(Color.FromArgb(255,0,255,0));
            }
            else if (status == FilterStatus.Ignored)
            {
                return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
            else
            {
                return new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));                
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}