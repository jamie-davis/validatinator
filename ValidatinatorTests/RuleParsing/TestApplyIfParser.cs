using System.Linq;
using FluentAssertions;
using TestConsole.OutputFormatting;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator.RuleParsing;
using Xunit;

namespace ValidatinatorTests.RuleParsing
{
    public class TestApplyIfParser
    {
        private static readonly string[] TestStrings = 
        {
            "FirstError",
            "cleaninput",
            "match(a.b)",
            "match(a.b),match(b.c),firstError",
            "match(a.b), match(b.c), firstError",
            "match(a.b)match(b.c)firstError",
            "match(a)",
            "match(a",
            "match(",
            "match",
            "wrong",
            "a.b"
        };

        [Fact]
        public void ParserExtractsRuleComponents()
        {
            //Arrange
            var buffer = new OutputBuffer()
            {
                BufferWidth = 133
            };
            var output = new Output(buffer);

            //Act
            var results = TestStrings
                .Select(s => new { String = s, Result = ApplyIfParser.Parse(s)})
                .ToList();

            //Assert
            var report = results.AsReport(rep => rep
                .AddColumn(r => r.String, cc => cc.Heading("Input"))
                .AddColumn(r => r.Result.Valid, cc => { })
                .AddChild(r => new [] { new {r.Result?.Error} }, erep => erep.AddColumn(e => e.Error, cc => {}))
                .AddChild(r => new [] { new {Result = r.Result?.Root.Describe()} }, drep => drep.AddColumn(e => e.Result ?? "NULL", cc => cc.Heading("Result")))
                );

            output.FormatTable(report);
            Approvals.Verify(output.Report);
        }

        [Fact]
        public void ParserExtractsMatches()
        {
            //Arrange
            var expected = new[]
            {
                "a.b",
                "b.c",
                "c.d"
            };

            var rule = string.Join(",", expected.Select(m => $"match({m})"));

            //Act
            var results = ApplyIfParser.Parse(rule);

            //Assert
            var matches = results.Matches.Select(m => $"{m.Entity}.{m.Field}");
            matches.ToArray().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void RequireCleanInputDefaultsToFalse()
        {
            //Arrange
            var rule = "match(a.b)";

            //Act
            var results = ApplyIfParser.Parse(rule);

            //Assert
            results.RequireCleanInput.Should().BeFalse();
        }

        [Fact]
        public void ParserExtractsRequireCleanInput()
        {
            //Arrange
            var rule = "match(a.b),cleaninput";

            //Act
            var results = ApplyIfParser.Parse(rule);

            //Assert
            results.RequireCleanInput.Should().BeTrue();
        }


        [Fact]
        public void RequireFirstErrorDefaultsToFalse()
        {
            //Arrange
            var rule = "match(a.b)";

            //Act
            var results = ApplyIfParser.Parse(rule);

            //Assert
            results.RequireFirstError.Should().BeFalse();
        }

        [Fact]
        public void ParserExtractsRequireFirstError()
        {
            //Arrange
            var rule = "match(a.b),firsterror";

            //Act
            var results = ApplyIfParser.Parse(rule);

            //Assert
            results.RequireFirstError.Should().BeTrue();
        }

    }
}
