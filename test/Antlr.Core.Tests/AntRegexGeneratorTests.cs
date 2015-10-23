namespace Antlr.Core.Tests
{
    using NUnit.Framework;

    public class AntRegexGeneratorTests
    {
        [TestFixture]
        public class when_get_regex_for_filter
        {

            [Test]
            [TestCase("A")]
            [TestCase("a")]
            [TestCase("1")]
            [TestCase("/")]
            [TestCase("\\")]
            [TestCase(".")]
            public void then_should_be_true_given_single_character_and_filter_equals_one_star(string character)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("*");

                // Assert
                Assert.That(regex.IsMatch(character), Is.True);
            }
            [Test]
            [TestCase("test/")]
            [TestCase("test/UnitTest.cs")]
            [TestCase("test/SubDirectory")]
            [TestCase("test/SubDirectory/SubDirectoryUnitTest.cs")]
            //[TestCase("test.cs")] language regarding this is confusing: "Matches all files that have a test element in their path, including test as a filename.". Does this mean test.cs should be found? Not sure.
            public void then_return_true_given_directory_has_test_in_path_or_file(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("**/test/**");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }

        }
        [TestFixture]
        public class when_testing_from_ant_examples
        {
            [Test]
            [TestCase("org/apache/CVS/Entries")]
            [TestCase("org/apache/jakarta/tools/ant/CVS/Entries")]
            public void then_be_true_given_org_frontslash_apache_frontslash_two_star_frontslash_cvs_frontslash_one_star(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/**/CVS/*");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }

            [Test]
            [TestCase("org/apache/CVS/foo/bar/Entries")]
            public void then_be_false_given_org_frontslash_apache_frontslash_two_star_frontslash_cvs_frontslash_one_star(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/**/CVS/*");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.False);
            }
            [Test]
            [TestCase("CVS/Repository")]
            [TestCase("org/apache/CVS/Entries")]
            [TestCase("org/apache/jakarta/tools/ant/CVS/Entries")]
            public void then_be_true_given_two_star_frontslash_cvs_frontslash_one_star(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("**/CVS/*");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }
            [Test]
            [TestCase("org/apache/CVS/foo/bar/Entries")]
            public void then_be_false_given_two_star_frontslash_cvs_frontslash_one_star(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("**/CVS/*");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.False);
            }

            [Test]
            [TestCase("org/apache/jakarta/tools/ant/docs/index.html")]
            [TestCase("org/apache/jakarta/test.xml")]
            public void then_should_be_true_given_filter_equal_to_two_stars(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta/**");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }
            [Test]
            public void then_should_be_false_given_filter_equal_to_two_stars()
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta/**");

                // Assert
                Assert.That(regex.IsMatch("org/apache/CVS/foo/bar/Entries"), Is.False);
            }

            [Test]
            [TestCase("org/apache/jakarta/tools/ant/docs/index.html")]
            [TestCase("org/apache/jakarta/test.xml")]
            public void then_should_be_true_given_filter_ends_with_frontslash(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta//");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }
            [Test]
            public void then_should_be_false_given_filter_ends_with_frontslash()
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta//");

                // Assert
                Assert.That(regex.IsMatch("org/apache/CVS/foo/bar/Entries"), Is.False);
            }

            [Test]
            [TestCase("org/apache/jakarta/tools/ant/docs/index.html")]
            [TestCase("org/apache/jakarta/test.xml")]
            public void then_should_be_true_given_filter_ends_with_backslash(string directory)
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta/\\");

                // Assert
                Assert.That(regex.IsMatch(directory), Is.True);
            }
            [Test]
            public void then_should_be_false_given_filter_ends_with_backslash()
            {
                // Arrange
                var generator = new AntRegexGenerator();

                // Act
                var regex = generator.GetRegexForFilter("org/apache/jakarta/\\");

                // Assert
                Assert.That(regex.IsMatch("org/apache/CVS/foo/bar/Entries"), Is.False);
            }

        }
    }
}