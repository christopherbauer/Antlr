using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;

namespace Antlr.Core.Tests
{
    public class StatusReaderUnitTests
    {
        [TestFixture]
        public class when_get_filter_status
        {
            [Test]
            [TestCase(FilterStatus.ParentIgnored)]
            [TestCase(FilterStatus.Ignored)]
            public void then_should_return_parent_ignored_given_parent_filter_status_is_ignored_enum_values(FilterStatus parentFilterStatus)
            {
                // Arrange
                var reader = new StatusReader(new Mock<IAntRegexGenerator>().Object);

                // Act
                var status = reader.GetFilterStatus("C:\\Temp", "*", parentFilterStatus, "C:\\Temp");

                // Assert
                Assert.That(status, Is.EqualTo(FilterStatus.ParentIgnored));
            }

            [Test]
            public void then_should_return_found_given_parent_filter_status_is_found_and_regex_matches()
            {
                // Arrange
                var antRegexGenerator = new Mock<IAntRegexGenerator>();
                antRegexGenerator.Setup(generator => generator.GetRegexForFilter("*")).Returns(new Regex("\\S"));
                var reader = new StatusReader(antRegexGenerator.Object);

                // Act
                var status = reader.GetFilterStatus("C:\\Temp\\MyProject", "*", FilterStatus.Found, "C:\\Temp");

                // Assert
                Assert.That(status, Is.EqualTo(FilterStatus.Found));
            }
            [Test]
            public void then_should_return_ignored_given_parent_filter_status_is_found_and_regex_does_not_matches()
            {
                // Arrange
                var antRegexGenerator = new Mock<IAntRegexGenerator>();
                antRegexGenerator.Setup(generator => generator.GetRegexForFilter("*")).Returns(new Regex("\\s"));
                var reader = new StatusReader(antRegexGenerator.Object);

                // Act
                var status = reader.GetFilterStatus("C:\\Temp\\MyProject", "*", FilterStatus.Found, "C:\\Temp");

                // Assert
                Assert.That(status, Is.EqualTo(FilterStatus.Ignored));
            }

        } 
    }
}