using System.Linq;
using TestConsole.OutputFormatting;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator.RuleParsing;
using Xunit;

namespace ValidatinatorTests.RuleParsing
{
    public class TestEntityReferenceParser
    {
        private static readonly string[] TestStrings =
        {
            "FirstError",
            "cleaninput",
            "a.b",
            "a",
            "match(a.b)"
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
                .Select(s => new {String = s, Result = EntityReferenceParser.Parse(s)})
                .ToList();

            //Assert
            var report = results.AsReport(rep => rep
                .AddColumn(r => r.String, cc => cc.Heading("Input"))
                .AddColumn(r => r.Result.Valid, cc => { })
                .AddChild(r => new[] {new {r.Result?.Error}}, erep => erep.AddColumn(e => e.Error, cc => { }))
                .AddChild(r => new[] {new {Result = r.Result?.EntityReference?.Describe()}},
                    drep => drep.AddColumn(e => e.Result ?? "NULL", cc => cc.Heading("Result")))
            );

            output.FormatTable(report);
            Approvals.Verify(output.Report);
        }
    }
}