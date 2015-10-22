namespace Antlr.Core
{
    using System.Windows.Media;

    public class FilterColorHelper
    {
        public Color GetFilterColor(FilterStatus status)
        {
            Color theColor;
            switch (status)
            {
                case FilterStatus.Found:
                    theColor = Color.FromArgb(255, 0, 255, 0);
                    break;
                case FilterStatus.Ignored:
                    theColor = Color.FromArgb(255, 255, 0, 0);
                    break;
                default:
                    theColor = Color.FromArgb(255, 255, 255, 0);
                    break;
            }
            return theColor;
        }
    }
}