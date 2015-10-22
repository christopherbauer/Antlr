namespace Antlr.Core.Tests
{
    using System.Windows.Media;

    using NUnit.Framework;

    public class FilterColorHelperUnitTests
    {
        [TestFixture]
        public class when_get_filter_color
        {

            [Test]
            public void then_should_return_green_given_found()
            {
                // Arrange
                var helper = new FilterColorHelper();

                // Act
                var color = helper.GetFilterColor(FilterStatus.Found);

                // Assert
                Assert.That(color, Is.EqualTo(Color.FromArgb(255, 0, 255, 0)));
            }
            [Test]
            public void then_should_return_red_given_ignored()
            {
                // Arrange
                var helper = new FilterColorHelper();

                // Act
                var color = helper.GetFilterColor(FilterStatus.Ignored);

                // Assert
                Assert.That(color, Is.EqualTo(Color.FromArgb(255, 255, 0, 0)));
            }
            [Test]
            public void then_should_return_yellow_given_parent_ignored()
            {
                // Arrange
                var helper = new FilterColorHelper();

                // Act
                var color = helper.GetFilterColor(FilterStatus.ParentIgnored);

                // Assert
                Assert.That(color, Is.EqualTo(Color.FromArgb(255, 255, 255, 0)));
            }

        }
    }
}